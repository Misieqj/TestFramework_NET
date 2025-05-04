using Microsoft.Playwright;

namespace TestFramework_NET.Frameworks.Playwright.Helpers
{
    internal static class TracingHelper
    {
        //=> https://playwright.dev/dotnet/docs/trace-viewer

        internal static async Task StartTracingAsync(this IBrowserContext context)
        {
            await context.Tracing.StartAsync(new()
            {
                Title = TestContext.CurrentContext.Test.Name,
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }

        internal static async Task<string> StopTracingAsync(this IBrowserContext context)
        {
            var path = Path.Combine(TestContext.Parameters.Get("DirResults") ?? string.Empty,
                    $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:ddHHmmss}.zip");
            await context.Tracing.StopAsync(new() { Path = path });

            return path;
        }
    }
}
