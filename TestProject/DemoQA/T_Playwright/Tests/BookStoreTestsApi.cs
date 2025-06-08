using System.Net;
using FluentAssertions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TestFramework_NET.Common;
using TestFramework_NET.Common.Helpers;
using TestFramework_NET.Common.Models;
using TestFramework_NET.TestProject.DemoQA.Data.Models.Api;

namespace TestFramework_NET.TestProject.DemoQA.T_Playwright.Tests
{
    public class BookStoreTestsApi : PlaywrightTest
    {
        private const string _settingsFilePath = $"TestProject\\DemoQA\\settings.json";
        private readonly string _baseUrl = JsonHelper.ObjectFromFile<SettingsModel>(_settingsFilePath).BaseUrl;
        private IAPIRequestContext RequestContext;

        [SetUp]
        public async Task Setup()
        {
            QLogger.PrintStartWithTcName();
            var requestOptions = new APIRequestNewContextOptions
            {
                BaseURL = _baseUrl
            };
            RequestContext = await Playwright.APIRequest.NewContextAsync(requestOptions);
        }

        [TearDown]
        public async Task TearDowns()
        {
            await RequestContext.DisposeAsync();
            QLogger.PrintEnd();
        }

        [Test]
        public async Task ApiAccountCheck()
        {
            // Arrange
            RegisterViewModel accountData = new()
            {
                UserName = "Aqq_TestUser",
                Password = "ZAQ!2wsx"
            };
            var accountRequestBody = JsonHelper.ObjectToDictionary(accountData);

            // Act
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

            IAPIResponse tokenReq = await RequestContext.PostAsync("/Account/v1/GenerateToken", new() { DataObject = accountRequestBody });
            string aaa = await tokenReq.TextAsync();
            TokenViewModel token = JsonHelper.ObjectFromJson<TokenViewModel>(await tokenReq.TextAsync());
            QLogger.Print($"Result => {token.Result}");

            // Assert
            token.Status.Should().Be("Success");
        }
    }
}
