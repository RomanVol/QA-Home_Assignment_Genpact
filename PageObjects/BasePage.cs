using Microsoft.Playwright;

namespace WikipediaPlaywrightTests.PageObjects
{
    public class BasePage
    {
        protected readonly IPage Page;
        
        // Common Wikipedia locators for appearance/theme
        protected const string HtmlElement = "html";
        protected const string AppearanceButton = "//button[contains(@class, 'vector-appearance')]";
        protected const string DayThemeToggle = "[for='skin-client-pref-skin-theme-value-day']";
        protected const string NightThemeToggle = "[for='skin-client-pref-skin-theme-value-night']";
        protected const string DarkModeLabel = "//label[contains(., 'Dark')]";
        
        public BasePage(IPage page)
        {
            Page = page;
        }
        
        public async Task NavigateTo(string url)
        {
            await Page.GotoAsync(url);
        }
        
        public async Task<string> GetPageTitle()
        {
            return await Page.TitleAsync();
        }
        
        public async Task WaitForLoad()
        {
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }
        
        public async Task<byte[]> TakeScreenshot(string name)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var filename = $"{name}_{timestamp}.png";
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots", filename);
            
            await Page.ScreenshotAsync(new PageScreenshotOptions { Path = path });
            return await File.ReadAllBytesAsync(path);
        }
        
        // Wikipedia Theme/Appearance Methods
        
        /// <summary>
        /// Gets the current theme class from HTML element
        /// </summary>
        public async Task<string> GetThemeClass()
        {
            var htmlElement = Page.Locator(HtmlElement);
            return await htmlElement.GetAttributeAsync("class") ?? string.Empty;
        }
        
        /// <summary>
        /// Checks if the current theme is Night/Dark
        /// </summary>
        public async Task<bool> IsNightTheme()
        {
            var classList = await GetThemeClass();
            return classList.Contains("skin-theme-clientpref-night");
        }
        
        /// <summary>
        /// Checks if the current theme is Day/Light
        /// </summary>
        public async Task<bool> IsDayTheme()
        {
            var classList = await GetThemeClass();
            return classList.Contains("skin-theme-clientpref-day");
        }
        
        /// <summary>
        /// Opens the Appearance menu (Color beta)
        /// </summary>
        public async Task OpenAppearanceMenu()
        {
            var appearanceButton = Page.Locator(AppearanceButton);
            if (await appearanceButton.IsVisibleAsync())
            {
                await appearanceButton.ClickAsync();
                await Page.WaitForTimeoutAsync(1000); // Wait for menu to appear
            }
        }
        
        /// <summary>
        /// Switches to Day theme
        /// </summary>
        public async Task SwitchToDayTheme()
        {
            var dayToggle = Page.Locator(DayThemeToggle);
            await dayToggle.WaitForAsync();
            await dayToggle.ClickAsync();
            await Page.WaitForTimeoutAsync(2000); // Wait for theme to apply
        }
        
        /// <summary>
        /// Switches to Night theme
        /// </summary>
        public async Task SwitchToNightTheme()
        {
            var nightToggle = Page.Locator(NightThemeToggle);
            await nightToggle.WaitForAsync();
            await nightToggle.ClickAsync();
            await Page.WaitForTimeoutAsync(2000); // Wait for theme to apply
        }
        
        /// <summary>
        /// Toggles between Day and Night themes
        /// </summary>
        public async Task<string> ToggleTheme()
        {
            var isNight = await IsNightTheme();
            
            if (isNight)
            {
                await SwitchToDayTheme();
                return "day";
            }
            else
            {
                await SwitchToNightTheme();
                return "night";
            }
        }
        
        /// <summary>
        /// Verifies if the Appearance menu is accessible
        /// </summary>
        public async Task<bool> IsAppearanceMenuAccessible()
        {
            var appearanceButton = Page.Locator(AppearanceButton);
            return await appearanceButton.IsVisibleAsync();
        }
    }
}
