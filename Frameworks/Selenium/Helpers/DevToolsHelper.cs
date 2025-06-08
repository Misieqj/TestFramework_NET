using System.Diagnostics;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V134.Network;

namespace TestFramework_NET.Frameworks.Selenium.Helpers
{
    public class DevToolsHelper
    {
        private readonly NetworkAdapter _network;
        private readonly List<(string url, string body)> _responses = [];

        public DevToolsHelper(ChromeDriver driver)
        {
            var session = ((IDevTools)driver).GetDevToolsSession();
            var domains = new OpenQA.Selenium.DevTools.V134.DevToolsSessionDomains(session);
            _network = domains.Network;

            _network.Enable(new EnableCommandSettings()).Wait();
            _network.ResponseReceived += async (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Response.Url))
                {
                    try
                    {
                        var body = await _network.GetResponseBody(new GetResponseBodyCommandSettings
                        {
                            RequestId = e.RequestId
                        });

                        lock (_responses)
                        {
                            _responses.Add((e.Response.Url, body.Body));
                        }
                    }
                    catch
                    {
                        // np. dla assetów binarnych
                    }
                }
            };
        }

        /// <summary>
        /// Method to get response body.
        /// </summary>
        /// <example>string? body = devHelper.WaitForApiResponse("/api/dane", TimeSpan.FromSeconds(5));</example>
        public string? WaitForApiResponse(string urlPart, TimeSpan timeout)
        {
            var sw = Stopwatch.StartNew();

            while (sw.Elapsed < timeout)
            {
                lock (_responses)
                {
                    var match = _responses.FirstOrDefault(r => r.url.Contains(urlPart));
                    if (!string.IsNullOrEmpty(match.url))
                    {
                        return match.body;
                    }
                }
                Thread.Sleep(200);
            }

            return null;
        }
    }
}
