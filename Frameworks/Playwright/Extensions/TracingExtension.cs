using Microsoft.Playwright;
using TestFramework_NET.Common;

namespace TestFramework_NET.Frameworks.Playwright.Extensions
{
    internal static class TracingExtension
    {
        // => https://playwright.dev/dotnet/docs/trace-viewer

        internal static async Task StartTracingAsync(this IBrowserContext context)
        {
            await context.Tracing.StartAsync(new()
            {
                Title = TestContext.CurrentContext.Test.ClassName + "." + TestContext.CurrentContext.Test.Name,
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }

        internal static async Task StopTracingAsync(this IBrowserContext context)
        {
            var path = Path.Combine(
                    Environment.CurrentDirectory,
                    "playwright-traces",
                    $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip");

            await context.Tracing.StopAsync(new() { Path = path });
            QLogger.PrintAttachmentInfo(path, "Trace path");
        }
    }
}
