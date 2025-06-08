using Microsoft.Playwright;

namespace TestFramework_NET.Test_Playwright.Extensions
{
    internal static class ApiExtension
    {
        //=> https://playwright.dev/dotnet/docs/mock
        //=> https://playwright.dev/dotnet/docs/network
        //=> https://playwright.dev/dotnet/docs/api-network

        /// <summary>
        /// Intercepts a network request and modifies the response body.
        /// </summary>
        internal static async Task ModifyResponse(this IPage page, string urlPattern, string oldString, string newString)
        {
            await page.RouteAsync(urlPattern, async route =>
            {
                var originalResponse = await route.FetchAsync();
                var originalBody = await originalResponse.TextAsync();
                var modifiedBody = originalBody.Replace(oldString, newString);

                await route.FulfillAsync(new()
                {
                    Status = originalResponse.Status,
                    Headers = originalResponse.Headers,
                    //ContentType = originalResponse.Headers.TryGetValue("content-type", out var ct) ? ct : "application/json",
                    Body = modifiedBody
                });
            });
        }

        /// <summary>
        /// Waits for a network response containing a specific URL part and returns the response body.
        /// </summary>
        internal static async Task<string?> GetResponseBody(this IPage page, string urlPart, TimeSpan timeout)
        {
            var tcs = new TaskCompletionSource<string>();
            EventHandler<IResponse> handler = async (_, response) =>
            {
                if (response.Url.Contains(urlPart))
                {
                    try
                    {
                        var body = await response.TextAsync();
                        tcs.TrySetResult(body);
                    }
                    catch
                    {
                        tcs.TrySetResult(""); // np. jeśli body nie da się sparsować
                    }
                }
            };

            page.Response += handler;

            using var cts = new CancellationTokenSource(timeout);
            cts.Token.Register(() => tcs.TrySetCanceled(), useSynchronizationContext: false);

            try
            {
                return await tcs.Task;
            }
            catch (TaskCanceledException)
            {
                return null;
            }
            finally
            {
                page.Response -= handler;
            }
        }
    }
}
