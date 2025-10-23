using NUnit.Framework;
using FluentAssertions;
using WikipediaPlaywrightTests.PageObjects;
using WikipediaPlaywrightTests.Services;
using WikipediaPlaywrightTests.Config;

namespace WikipediaPlaywrightTests.Tests
{
    /// <summary>
    /// Example tests demonstrating how to test different Wikipedia pages
    /// using the flexible configuration
    /// </summary>
    [TestFixture]
    [Explicit] // Mark as explicit so it doesn't run with other tests by default
    public class ExampleTests_DifferentWikipediaPages : BaseTest
    {
        private WikipediaPlaywrightPage? _wikipediaPage;
        private MediaWikiApiService? _apiService;
        
        [SetUp]
        public override async Task Setup()
        {
            await base.Setup();
            _wikipediaPage = new WikipediaPlaywrightPage(Page);
            _apiService = new MediaWikiApiService();
        }
        
        [Test]
        [Category("Example")]
        [Description("Example: Test Python programming language Wikipedia page")]
        public async Task Example_TestPythonPage()
        {
            // Arrange
            var pageTitle = "Python_(programming_language)";
            var pageUrl = TestConfiguration.GetWikipediaPageUrl(pageTitle);
            
            LogInfo($"Testing Wikipedia page: {pageTitle}");
            
            // Act
            await _wikipediaPage!.NavigateTo(pageUrl);
            await _wikipediaPage.WaitForLoad();
            
            // You can now interact with the Python page
            var pageTitle_displayed = await Page.TitleAsync();
            
            // Assert
            pageTitle_displayed.Should().Contain("Python", "Page should be about Python");
            
            LogPass($"✓ Successfully navigated to {pageTitle}");
        }
        
        [Test]
        [Category("Example")]
        [Description("Example: Test Selenium WebDriver Wikipedia page")]
        public async Task Example_TestSeleniumPage()
        {
            // Arrange
            var pageTitle = "Selenium_(software)";
            var pageUrl = TestConfiguration.GetWikipediaPageUrl(pageTitle);
            
            LogInfo($"Testing Wikipedia page: {pageTitle}");
            
            // Act
            await _wikipediaPage!.NavigateTo(pageUrl);
            await _wikipediaPage.WaitForLoad();
            
            var pageTitle_displayed = await Page.TitleAsync();
            
            // Assert
            pageTitle_displayed.Should().Contain("Selenium", "Page should be about Selenium");
            
            LogPass($"✓ Successfully navigated to {pageTitle}");
        }
        
        [Test]
        [Category("Example")]
        [Description("Example: Extract section from a different Wikipedia page via API")]
        public async Task Example_ExtractSectionFromDifferentPage()
        {
            // Arrange
            var pageTitle = "JavaScript";
            var sectionTitle = "History";
            
            LogInfo($"Extracting '{sectionTitle}' section from {pageTitle} page via API");
            
            // Act
            var sectionText = await _apiService!.GetSectionText(pageTitle, sectionTitle);
            
            // Assert
            sectionText.Should().NotBeNullOrWhiteSpace("Section should contain text");
            sectionText.ToLower().Should().Contain("javascript", 
                "History section should mention JavaScript");
            
            LogInfo($"Extracted text length: {sectionText.Length} characters");
            LogPass($"✓ Successfully extracted '{sectionTitle}' section from {pageTitle}");
        }
        
        [Test]
        [Category("Example")]
        [Description("Example: Test multiple pages in a data-driven approach")]
        [TestCase("C_Sharp_(programming_language)", "C#")]
        [TestCase("Java_(programming_language)", "Java")]
        [TestCase("TypeScript", "TypeScript")]
        public async Task Example_TestMultipleProgrammingLanguages(string pageTitle, string expectedKeyword)
        {
            // Arrange
            var pageUrl = TestConfiguration.GetWikipediaPageUrl(pageTitle);
            
            LogInfo($"Testing Wikipedia page: {pageTitle}");
            
            // Act
            await _wikipediaPage!.NavigateTo(pageUrl);
            await _wikipediaPage.WaitForLoad();
            
            var displayedTitle = await Page.TitleAsync();
            
            // Assert
            displayedTitle.Should().Contain(expectedKeyword, $"Page should be about {expectedKeyword}");
            
            LogPass($"✓ Successfully verified {pageTitle}");
        }
    }
}
