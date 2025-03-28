//using RestSharp;
//using TestFramework_NET.Common;

namespace TestFramework_NET.Frameworks.ApiRestSharp
{
    /*
    internal class ApiService(string baseUrl)
    {
        private RestClient Client { get; set; } = new(baseUrl);
        private RestResponse Response { get; set; } = new RestResponse();

        public void AddAuthToken(string token)
        {
            Client.AddDefaultHeader("Authorization", $"Bearer {token}");
            QLog.Print($"Token added");
        }

        public async Task<RestResponse> SendRequestAsync(string uri, Method type = Method.Get, string? body = null)
        {
            var request = new RestRequest(uri, type);
            if (body != null)
            {
                request.AddJsonBody(body);
            }
            Response = await Client.ExecuteAsync(request);

            return Response;
        }

        public bool ResponseIsOk()
            => Response.IsSuccessful;
        //	IsSuccessStatusCode true	    bool
        //	ResponseStatus	    Completed	RestSharp.ResponseStatus
        //	StatusCode	        OK	        System.Net.HttpStatusCode => HttpStatusCode.OK
        //  StatusDescription	"OK"	    string

        public string ResponseStatusDescription()
            => Response.StatusDescription ?? string.Empty;

        public string ResponseError()
            => Response.ErrorMessage ?? string.Empty;
    }
    */
}
