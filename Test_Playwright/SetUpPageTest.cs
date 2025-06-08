using Microsoft.Playwright;
using TestFramework_NET.Common;
using TestFramework_NET.Test_Playwright.Helpers;

namespace TestFramework_NET.Test_Playwright
{
    [TestFixture]
    public class SetUpPageTest
    {
        #region variables
        //=> More settings to check: https://playwright.dev/dotnet/docs/api/class-browsertype
        //=> Hot to set params: https://playwright.dev/dotnet/docs/test-runners
        protected IPlaywright Playwright { get; private set; }
        protected IBrowser Browser { get; private set; }
        protected IBrowserContext Context { get; private set; }
        protected IPage Page { get; private set; } = null!;
        #endregion

        [SetUp]
        public async Task SetUp()
        {
            Playwright = await CreatePlaywrightAsync();
            Browser = await RunBrowserAsync(Playwright);
            Context = await CreateContext(Browser);
            Page = await Context.NewPageAsync();
            QLogger.PrintStartWithTcName();
            await Context.StartTracingAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            if (IsTestFail())
            {
                if (bool.Parse(TestContext.Parameters.Get("Screenshot") ?? "true"))
                {
                    var ssPath = await Page.DoScreenshotAsync();
                    QLogger.Print("---");
                    QLogger.Print($"Screenshot path: {ssPath}");

                    var tracePath = await Context.StopTracingAsync();
                    QLogger.Print($"Trace path: {tracePath}");
                }
            }

            await Browser.CloseAsync();
            Playwright.Dispose();

            if (IsTestFail())
            {
                var videoPath = await Page.StopRecordVideoAsync();
                QLogger.Print($"Video record path: {videoPath}");
            }
            else
                await Page.DeleteRecordedVideoAsync();
            QLogger.PrintEnd();
        }

        protected static bool IsTestFail()
            => TestContext.CurrentContext.Result.Outcome.Status
                == NUnit.Framework.Interfaces.TestStatus.Failed;

        private static async Task<IPlaywright> CreatePlaywrightAsync()
            => await Microsoft.Playwright.Playwright.CreateAsync()
                ?? throw new ArgumentException("Playwright is null");

        private static async Task<IBrowser> RunBrowserAsync(IPlaywright playwright)
        {
            var headless = TestContext.Parameters.Get("Headless") ?? "false";
            var _browserType = TestContext.Parameters.Get("BrowserName") ?? "chromium";
            var browserOptions = new BrowserTypeLaunchOptions
            {
                //=> https://playwright.dev/dotnet/docs/api/class-browser#browser-new-context
                Timeout = 10000,
                Headless = bool.Parse(headless),
                //SlowMo = 1000,
                //Devtools = true //=> Only for Chromium
            };
            var browser = _browserType switch
            {
                "chromium" => await playwright.Chromium.LaunchAsync(browserOptions),
                "firefox" => await playwright.Firefox.LaunchAsync(browserOptions),
                "webkit" => await playwright.Webkit.LaunchAsync(browserOptions),
                _ => throw new ArgumentException("Invalid browser type"),
            };

            return browser;
        }

        private static async Task<IBrowserContext> CreateContext(IBrowser browser)
        {
            //=> To oversize params in test
            //=> https://playwright.dev/dotnet/docs/test-runners#customizing-browsercontext-options
            var context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new() { Width = 1280, Height = 720 },
                TimezoneId = "Europe/Berlin",
                Locale = "en-US",
                UserAgent = "Aqq Nunit Agent",
                ColorScheme = ColorScheme.Dark,
                //=> Settings for recording
                RecordVideoDir = TestContext.Parameters.Get("DirResults"),
                //===
                // StorageStatePath = "session.json",
                // HttpCredentials = new HttpCredentials { Username = "admin", Password = "admin" },
                // ExtraHTTPHeaders = new Dictionary<string, string> { ["Authorization"] = "Bearer token" },
            });
            context.SetDefaultTimeout(10000);

            return context;
        }
    }
}
