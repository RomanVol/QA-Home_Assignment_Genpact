using NUnit.Framework;
using FluentAssertions;
using WikipediaPlaywrightTests.PageObjects;
using WikipediaPlaywrightTests.Config;

namespace WikipediaPlaywrightTests.Tests
{
    [TestFixture]
    public class Task2_MicrosoftDevToolsTests : BaseTest
    {
        private WikipediaPlaywrightPage? _wikipediaPage;
        
        [SetUp]
        public override async Task Setup()
        {
            await base.Setup();
            _wikipediaPage = new WikipediaPlaywrightPage(Page);
        }
        
        [Test]
        [Category("UI")]
        [Description("Validate that all technology names under Microsoft development tools section are text links")]
        public async Task Task2_ValidateAllTechnologyNamesAreLinks()
        {
            // Arrange
            LogInfo("Starting Task 2: Validate Microsoft development tools technology links");
            
            // Act
            LogInfo($"Navigating to: {TestConfiguration.WikipediaPlaywrightUrl}");
            await _wikipediaPage!.NavigateTo(TestConfiguration.WikipediaPlaywrightUrl);
            await _wikipediaPage.WaitForLoad();
            
            LogInfo("Extracting technology names from Microsoft development tools section");
            var technologies = await _wikipediaPage.GetMicrosoftDevToolsTechnologies();
            
            LogInfo($"Found {technologies.Count} technologies");
            
            // Assert
            technologies.Should().NotBeEmpty("Microsoft development tools section should contain technologies");
            
            var nonLinkTechnologies = new List<string>();
            
            foreach (var (name, isLink) in technologies)
            {
                if (isLink)
                {
                    LogPass($"✓ '{name}' is a link");
                }
                else
                {
                    LogFail($"✗ '{name}' is NOT a link");
                    nonLinkTechnologies.Add(name);
                }
            }
            
            // Final assertion
            if (nonLinkTechnologies.Any())
            {
                var failureMessage = $"The following technology names are NOT links: {string.Join(", ", nonLinkTechnologies)}";
                LogFail(failureMessage);
                Assert.Fail(failureMessage);
            }
            else
            {
                LogPass($"✓ All {technologies.Count} technology names are links!");
            }
        }
        
        [Test]
        [Category("UI")]
        [Description("Validate each technology name individually (multiple test approach)")]
        [TestCaseSource(nameof(GetTechnologies))]
        public async Task Task2_ValidateIndividualTechnologyIsLink(string technologyName)
        {
            // Arrange
            LogInfo($"Validating that '{technologyName}' is a link");
            
            // This test would need to be run after extracting technologies
            // For now, we'll extract them in each test (can be optimized with a fixture)
            await _wikipediaPage!.NavigateTo(TestConfiguration.WikipediaPlaywrightUrl);
            await _wikipediaPage.WaitForLoad();
            
            var technologies = await _wikipediaPage.GetMicrosoftDevToolsTechnologies();
            var technology = technologies.FirstOrDefault(t => t.Name == technologyName);
            
            // Assert
            technology.Should().NotBe(default, $"Technology '{technologyName}' should exist");
            technology.IsLink.Should().BeTrue($"Technology '{technologyName}' should be a link");
            
            LogPass($"✓ '{technologyName}' is a link");
        }
        
        private static IEnumerable<string> GetTechnologies()
        {
            // This would ideally be populated from a data source
            // For now, returning common expected technologies
            // In a real scenario, this could be extracted once and cached
            return new[]
            {
                "Visual Studio Code",
                "Visual Studio",
                "Azure DevOps"
            };
        }
    }
}
