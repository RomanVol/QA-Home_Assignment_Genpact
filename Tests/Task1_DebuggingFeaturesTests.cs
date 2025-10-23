using NUnit.Framework;
using FluentAssertions;
using WikipediaPlaywrightTests.PageObjects;
using WikipediaPlaywrightTests.Services;
using WikipediaPlaywrightTests.Utils;
using WikipediaPlaywrightTests.Config;

namespace WikipediaPlaywrightTests.Tests
{
    [TestFixture]
    public class Task1_DebuggingFeaturesTests : BaseTest
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
        [Category("Integration")]
        [Description("Extract Debugging features section via UI and API, normalize texts, and compare unique word counts")]
        public async Task Task1_CompareDebuggingFeaturesSectionFromUIAndAPI()
        {
            // Arrange
            LogInfo("Starting Task 1: Extract and compare Debugging features section");
            
            // Act - Step 1: Extract via UI (POM approach)
            LogInfo($"Navigating to: {TestConfiguration.WikipediaPlaywrightUrl}");
            await _wikipediaPage!.NavigateTo(TestConfiguration.WikipediaPlaywrightUrl);
            await _wikipediaPage.WaitForLoad();
            
            LogInfo("Extracting Debugging features section via UI");
            var uiText = await _wikipediaPage.GetDebuggingFeaturesText();
            LogInfo($"UI Text extracted (first 100 chars): {uiText.Substring(0, Math.Min(100, uiText.Length))}...");
            
            // Print complete UI text
            Console.WriteLine("========== COMPLETE UI TEXT ==========");
            Console.WriteLine(uiText);
            Console.WriteLine("========== END UI TEXT ==========");
            Console.WriteLine();
            
            // Act - Step 2: Extract via API (MediaWiki Parse API)
            LogInfo("Extracting Debugging features section via MediaWiki API");
            var apiText = await _apiService!.GetSectionText(TestConfiguration.DefaultPageTitle, "Debugging features");
            LogInfo($"API Text extracted (first 100 chars): {apiText.Substring(0, Math.Min(100, apiText.Length))}...");
            
            // Print complete API text
            Console.WriteLine("========== COMPLETE API TEXT ==========");
            Console.WriteLine(apiText);
            Console.WriteLine("========== END API TEXT ==========");
            Console.WriteLine();
            
            // Act - Step 3: Normalize both texts
            LogInfo("Normalizing both texts");
            var normalizedUiText = TextNormalizer.Normalize(uiText);
            var normalizedApiText = TextNormalizer.Normalize(apiText);
            
            LogInfo($"Normalized UI text (first 100 chars): {normalizedUiText.Substring(0, Math.Min(100, normalizedUiText.Length))}...");
            LogInfo($"Normalized API text (first 100 chars): {normalizedApiText.Substring(0, Math.Min(100, normalizedApiText.Length))}...");
            
            // Act - Step 4: Count unique words
            LogInfo("Counting unique words in both texts");
            var uiUniqueWordCount = TextNormalizer.CountUniqueWords(normalizedUiText);
            var apiUniqueWordCount = TextNormalizer.CountUniqueWords(normalizedApiText);
            
            LogInfo($"UI unique word count: {uiUniqueWordCount}");
            LogInfo($"API unique word count: {apiUniqueWordCount}");
            
            // Get unique words for detailed comparison
            var uiWords = TextNormalizer.GetUniqueWords(normalizedUiText);
            var apiWords = TextNormalizer.GetUniqueWords(normalizedApiText);
            
            var onlyInUi = uiWords.Except(apiWords).ToList();
            var onlyInApi = apiWords.Except(uiWords).ToList();
            
            if (onlyInUi.Count > 0)
            {
                LogInfo($"Words only in UI ({onlyInUi.Count}): {string.Join(", ", onlyInUi)}");
            }
            
            if (onlyInApi.Count > 0)
            {
                LogInfo($"Words only in API ({onlyInApi.Count}): {string.Join(", ", onlyInApi)}");
            }
            
            // Assert - Step 5: Both texts should have the same unique words
            LogInfo("Asserting that UI and API extracted the same words");
            
            onlyInUi.Should().BeEmpty($"UI should not have any unique words that API doesn't have. Words only in UI: {string.Join(", ", onlyInUi)}");
            onlyInApi.Should().BeEmpty($"API should not have any unique words that UI doesn't have. Words only in API: {string.Join(", ", onlyInApi)}");
            
            uiUniqueWordCount.Should().Be(apiUniqueWordCount, 
                $"UI and API should extract the same content with equal unique word counts");
            
            LogPass($"✓ Exact match achieved! UI and API both have {uiUniqueWordCount} unique words");
        }
        
        [Test]
        [Category("UI")]
        [Description("Verify that Debugging features section can be extracted via UI")]
        public async Task VerifyDebuggingFeaturesSectionExtractionViaUI()
        {
            // Arrange
            LogInfo("Testing UI extraction of Debugging features section");
            
            // Act
            await _wikipediaPage!.NavigateTo(TestConfiguration.WikipediaPlaywrightUrl);
            await _wikipediaPage.WaitForLoad();
            
            var text = await _wikipediaPage.GetDebuggingFeaturesText();
            
            // Print the raw text to console
            Console.WriteLine("=== RAW UI TEXT ===");
            Console.WriteLine(text);
            Console.WriteLine("=== END RAW TEXT ===");
            Console.WriteLine();
            
            var normalizedText = TextNormalizer.Normalize(text);
            
            // Print normalized text to console
            Console.WriteLine("=== NORMALIZED TEXT ===");
            Console.WriteLine(normalizedText);
            Console.WriteLine("=== END NORMALIZED TEXT ===");
            Console.WriteLine();
            
            var uniqueWords = TextNormalizer.CountUniqueWords(normalizedText);
            
            Console.WriteLine($"Total unique words: {uniqueWords}");
            
            // Assert
            text.Should().NotBeNullOrWhiteSpace("Debugging features section should contain text");
            uniqueWords.Should().BeGreaterThan(0, "Should have at least one unique word");
            
            LogPass($"✓ Successfully extracted Debugging features via UI with {uniqueWords} unique words");
        }
        
        [Test]
        [Category("API")]
        [Description("Verify that Debugging features section can be extracted via MediaWiki API")]
        public async Task VerifyDebuggingFeaturesSectionExtractionViaAPI()
        {
            // Arrange
            LogInfo("Testing API extraction of Debugging features section");
            
            // Act
            var text = await _apiService!.GetSectionText(TestConfiguration.DefaultPageTitle, "Debugging features");
            
            // Print the raw text to console
            Console.WriteLine("=== RAW API TEXT ===");
            Console.WriteLine(text);
            Console.WriteLine("=== END RAW TEXT ===");
            Console.WriteLine();
            
            var normalizedText = TextNormalizer.Normalize(text);
            
            // Print normalized text to console
            Console.WriteLine("=== NORMALIZED TEXT ===");
            Console.WriteLine(normalizedText);
            Console.WriteLine("=== END NORMALIZED TEXT ===");
            Console.WriteLine();
            
            var uniqueWords = TextNormalizer.CountUniqueWords(normalizedText);
            
            Console.WriteLine($"Total unique words: {uniqueWords}");
            
            // Assert
            text.Should().NotBeNullOrWhiteSpace("Debugging features section should contain text");
            uniqueWords.Should().BeGreaterThan(0, "Should have at least one unique word");
            
            LogPass($"✓ Successfully extracted Debugging features via API with {uniqueWords} unique words");
        }
    }
}
