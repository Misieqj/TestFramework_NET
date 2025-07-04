﻿using FluentAssertions;
using Microsoft.Playwright.NUnit;
using TestFramework_NET.Common;
using TestFramework_NET.Common.Helpers;
using TestFramework_NET.Common.Models;
using TestFramework_NET.Data;
using TestFramework_NET.Data.Models.Api;
using TestFramework_NET.Test_Playwright.Extensions;
using TestFramework_NET.Test_Playwright.Pages;
using TestFramework_NET.Test_Playwright.Pages.Components;

namespace TestFramework_NET.Test_Playwright.Tests
{
    internal class BookStoreTests : PageTest
    {
        private const string SettingsFilePath = "settings.json";


        [SetUp]
        public async Task Setup()
        {
            SettingsModel settings = JsonHelper.ObjectFromFile<SettingsModel>(SettingsFilePath);
            QLogger.PrintStartWithTcName();
            await Page.GotoAsync(settings.BaseUrl);
        }

        [TearDown]
        public void TearDowns()
        {
            QLogger.PrintEnd();
        }

        [Test]
        public async Task LoadBooksList()
        {
            // Arrange
            const string firstTitle = "Git Pocket Guide";
            const string apiBooksList = "BookStore/v1/Books";

            // Act
            MenuComponent menu = new(Page);
            await menu.ClickMenuPositionAsync(MenuData.BookStoreApplication);
            // API
            string bookListResp = await Page.GetResponseBody(apiBooksList, TimeSpan.FromSeconds(10)) ?? string.Empty;
            BooksModel booksList = JsonHelper.ObjectFromJson<BooksModel>(bookListResp);
            // Page
            BookStorePage bookStorePage = new(Page);
            string bookStoreText = await bookStorePage.GetTableRowTextAsync();

            // Assert
            Assert.Multiple(() =>
            {
                bookStoreText.Should().Contain(firstTitle);
                booksList?.Book?.First().Title.Should().Be(firstTitle);
            });
        }

        [Test]
        public async Task ChangeApiResponse()
        {
            // Arrange
            const string firstTitle = "Git Pocket Guide";
            const string modifiedTitle = "Aqq";
            const string apiBooksList = "**/Books";
            await Page.ModifyResponse(apiBooksList, firstTitle, modifiedTitle);

            // Act
            MenuComponent menu = new(Page);
            await menu.ClickMenuPositionAsync(MenuData.BookStoreApplication);
            await menu.ClickSubmenuPositionAsync(MenuData.BookStoreApplication_BookStore);
            BookStorePage bookStorePage = new(Page);
            string bookStoreText = await bookStorePage.GetTableRowTextAsync();

            // Assert
            bookStoreText.Should().Contain(modifiedTitle);
        }

        [Test]
        public async Task BlockApiResponse()
        {
            // Arrange
            const string firstTitle = "Git Pocket Guide";
            const string apiBooksListURL = "**/Books";
            await Page.RouteAsync(apiBooksListURL, route => route.AbortAsync());

            // Act
            MenuComponent menu = new(Page);
            await menu.ClickMenuPositionAsync(MenuData.BookStoreApplication);
            BookStorePage bookStorePage = new(Page);
            string bookStoreText = await bookStorePage.GetTableRowTextAsync();

            // Assert
            bookStoreText.Should().Contain(firstTitle);
        }
    }
}
