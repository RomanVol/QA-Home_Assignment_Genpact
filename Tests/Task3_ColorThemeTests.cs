using NUnit.Framework;
using FluentAssertions;
using WikipediaPlaywrightTests.PageObjects;
using WikipediaPlaywrightTests.Config;

namespace WikipediaPlaywrightTests.Tests
{
    [TestFixture]
    public class Task3_ColorThemeTests : BaseTest
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
        [Description("Change color theme to Dark and validate the change")]
        public async Task Task3_ChangeColorToDarkAndValidate()
        {
            // Arrange
            LogInfo("Starting Task 3: Change color theme to Dark/Day toggle");
            
            // Act - Step 1: Navigate to page
            LogInfo($"Navigating to: {TestConfiguration.WikipediaPlaywrightUrl}");
            await _wikipediaPage!.NavigateTo(TestConfiguration.WikipediaPlaywrightUrl);
            await _wikipediaPage.WaitForLoad();
            
            // Act - Step 2: Get initial theme state
            LogInfo("Getting initial theme state");
            var initialClassList = await _wikipediaPage.GetThemeClass();
            
            LogInfo($"Initial HTML classes: {initialClassList}");
            
            // Determine initial theme
            bool isInitiallyNight = await _wikipediaPage.IsNightTheme();
            bool isInitiallyDay = await _wikipediaPage.IsDayTheme();
            
            LogInfo($"Is initially night: {isInitiallyNight}");
            LogInfo($"Is initially day: {isInitiallyDay}");
            
            string initialTheme = isInitiallyNight ? "night" : (isInitiallyDay ? "day" : "unknown");
            LogInfo($"Initial theme: {initialTheme}");
            
            // Take screenshot before
            await _wikipediaPage.TakeScreenshot($"before_theme_change_{initialTheme}");
            
            // Act - Step 3: Toggle theme based on current state
            if (isInitiallyNight)
            {
                // If currently night, switch to day
                LogInfo("Current theme is NIGHT, switching to DAY");
                await _wikipediaPage.SwitchToDayTheme();
                
                // Verify theme changed to day
                var finalClassList = await _wikipediaPage.GetThemeClass();
                LogInfo($"Final HTML classes: {finalClassList}");
                
                var isNowDay = await _wikipediaPage.IsDayTheme();
                LogInfo($"Is now day: {isNowDay}");
                
                // Take screenshot after
                await _wikipediaPage.TakeScreenshot("after_theme_change_to_day");
                
                // Assert
                isNowDay.Should().BeTrue("Theme should be 'skin-theme-clientpref-day' after clicking day toggle");
                LogPass("✓ Theme successfully changed from NIGHT to DAY");
            }
            else if (isInitiallyDay)
            {
                // If currently day, switch to night
                LogInfo("Current theme is DAY, switching to NIGHT");
                await _wikipediaPage.SwitchToNightTheme();
                
                // Verify theme changed to night
                var finalClassList = await _wikipediaPage.GetThemeClass();
                LogInfo($"Final HTML classes: {finalClassList}");
                
                var isNowNight = await _wikipediaPage.IsNightTheme();
                LogInfo($"Is now night: {isNowNight}");
                
                // Take screenshot after
                await _wikipediaPage.TakeScreenshot("after_theme_change_to_night");
                
                // Assert
                isNowNight.Should().BeTrue("Theme should be 'skin-theme-clientpref-night' after clicking night toggle");
                LogPass("✓ Theme successfully changed from DAY to NIGHT");
            }
            else
            {
                LogFail("✗ Initial theme is neither day nor night - cannot proceed with test");
                Assert.Fail("HTML element does not have 'skin-theme-clientpref-day' or 'skin-theme-clientpref-night' class");
            }
        }
        
        [Test]
        [Category("UI")]
        [Description("Verify Color (beta) section is accessible")]
        public async Task VerifyColorBetaSectionIsAccessible()
        {
            // Arrange
            LogInfo("Verifying Color (beta) section accessibility");
            
            // Act
            await _wikipediaPage!.NavigateTo(TestConfiguration.WikipediaPlaywrightUrl);
            await _wikipediaPage.WaitForLoad();
            
            // Check if appearance menu is accessible
            var isAccessible = await _wikipediaPage.IsAppearanceMenuAccessible();
            
            // Assert
            isAccessible.Should().BeTrue("Color (beta) button should be visible");
            
            if (isAccessible)
            {
                await _wikipediaPage.OpenAppearanceMenu();
                
                var darkOption = Page.Locator("//label[contains(., 'Dark')]");
                var isDarkOptionVisible = await darkOption.IsVisibleAsync();
                
                isDarkOptionVisible.Should().BeTrue("Dark mode option should be visible after clicking color button");
                LogPass("✓ Color (beta) section is accessible and Dark option is available");
            }
        }
    }
}
