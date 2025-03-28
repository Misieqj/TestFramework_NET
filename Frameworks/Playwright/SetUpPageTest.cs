using Microsoft.Playwright;
using TestFramework_NET.Frameworks.Playwright.Extensions;

namespace TestFramework_NET.Frameworks.Playwright
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
        }

        [TearDown]
        public async Task TearDown()
        {
            if (IsTestFail())
            {
                await Page.DoScreenshotAsync();
                await Page.StopRecordVideoAsync();
            }
            await Browser!.CloseAsync();
            Playwright?.Dispose();
        }

        protected static bool IsTestFail()
            => TestContext.CurrentContext.Result.Outcome.Status
                == NUnit.Framework.Interfaces.TestStatus.Failed;

        private static async Task<IPlaywright> CreatePlaywrightAsync()
            => await Microsoft.Playwright.Playwright.CreateAsync()
                ?? throw new ArgumentException("Playwright is null");

        private static async Task<IBrowser> RunBrowserAsync(IPlaywright playwright)
        {
            var _browserType = TestContext.Parameters.Get("BrowserName") ?? "chromium";
            var browserOptions = new BrowserTypeLaunchOptions
            {
                //=> https://playwright.dev/dotnet/docs/api/class-browser#browser-new-context
                Timeout = 10000
                //SlowMo = 1000,
                //Devtools = true
                //Headless = false //=> set in runsettings.
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
                //=> good to set unique name to later easier find sth in logs
                UserAgent = "Aqq Nunit Agent",
                //=> Settings for recording
                // RecordVideoDir = "videos/",
                //=> Additional useful settings
                ColorScheme = ColorScheme.Dark,
                TimezoneId = "Europe/Berlin",
                Locale = "en-US"
                // StorageStatePath = "session.json",
                // HttpCredentials = new HttpCredentials { Username = "admin", Password = "admin" },
                // ExtraHTTPHeaders = new Dictionary<string, string> { ["Authorization"] = "Bearer token" },
            });
            context.SetDefaultTimeout(10000);

            return context;
        }
    }
}
