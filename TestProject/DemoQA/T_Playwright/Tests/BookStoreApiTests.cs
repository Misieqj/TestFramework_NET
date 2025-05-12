using System.Net;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TestFramework_NET.Common;
using TestFramework_NET.Common.Helpers;
using TestFramework_NET.Common.Models;
using TestFramework_NET.TestProject.DemoQA.Data.Models.Api;

namespace TestFramework_NET.TestProject.DemoQA.T_Playwright.Tests
{
    public class BookStoreApiTests : PlaywrightTest
    {
        private readonly string _settingsFilePath = $"TestProject\\DemoQA\\settings.json";
        private IAPIRequestContext RequestContext;

        [SetUp]
        public async Task Setup()
        {
            QLogger.PrintStartWithTcName();
            var baseUrl = JsonHelper.ObjectFromFile<SettingsModel>(_settingsFilePath).BaseUrl;
            var requestOptions = new APIRequestNewContextOptions
            {
                BaseURL = baseUrl
            };
            RequestContext = await Playwright.APIRequest.NewContextAsync(requestOptions);
        }

        [TearDown]
        public void TearDowns()
        {
            QLogger.PrintEnd();
        }

        [Test]
        public async Task ApiAccountCheck()
        {
            RegisterViewModel accountData = new()
            {
                UserName = "Aqq_TestUser",
                Password = "ZAQ!2wsx"
            };
            var accountRequestBody = JsonHelper.ObjectToDictionary(accountData);

            // Create user if not exist or get info that exist
            QLogger.PrintHeader($"Create User");
            IAPIResponse createUserReq = await RequestContext.PostAsync("/Account/v1/User", new() { DataObject = accountRequestBody });
            //CreateUserModel createUserModel = new();
            if (createUserReq.Status == (int)HttpStatusCode.Created)
                QLogger.Print("User created.");
            else
            {
                MessageModel createUserRespBody = JsonHelper.ObjectFromJson<MessageModel>(await createUserReq.TextAsync());
                QLogger.Print($"Error code => {createUserRespBody.Code}");
                QLogger.Print($"Error msg  => {createUserRespBody.Message}");
            }

            // Authorize user
            QLogger.PrintHeader($"Auth");
            var authMsgReq = await RequestContext.PostAsync("/Account/v1/Authorized", new() { DataObject = accountRequestBody });
            QLogger.Print($"Authorization status: {authMsgReq.Status}");

            // Generate token for the user
            QLogger.PrintHeader($"Generate a token");
            var tokenReq = await RequestContext.PostAsync("/Account/v1/GenerateToken", new() { DataObject = accountRequestBody });
            TokenViewModel token = JsonHelper.ObjectFromJson<TokenViewModel>(await tokenReq.TextAsync());
            QLogger.Print($"Result => {token.Result}");
        }
    }
}
