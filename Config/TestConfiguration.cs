namespace WikipediaPlaywrightTests.Config
{
    public static class TestConfiguration
    {
        public static string BaseUrl => "https://en.wikipedia.org";
        
        // Default page for Playwright tests
        public static string DefaultPageTitle => "Playwright_(software)";
        
        // Backward compatibility - kept for existing tests
        public static string WikipediaPlaywrightUrl => GetWikipediaPageUrl(DefaultPageTitle);
        
        public static string MediaWikiApiUrl => $"{BaseUrl}/w/api.php";
        
        /// <summary>
        /// Generates a Wikipedia page URL for any given page title
        /// </summary>
        /// <param name="pageTitle">The Wikipedia page title (e.g., "Playwright_(software)", "Python_(programming_language)")</param>
        /// <returns>Full Wikipedia URL</returns>
        public static string GetWikipediaPageUrl(string pageTitle)
        {
            return $"{BaseUrl}/wiki/{pageTitle}";
        }
        
        // Browser settings
        public static bool Headless => false;
        public static int DefaultTimeout => 30000; // 30 seconds
        public static string BrowserType => "chromium"; // chromium, firefox, webkit
        
        // Screenshot settings
        public static string ScreenshotPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
        
        // Report settings - Save to TestResults folder in project root
        public static string ReportPath
        {
            get
            {
                // Navigate up from bin/Debug/net8.0 to project root, then to TestResults
                var projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
                return projectRoot != null 
                    ? Path.Combine(projectRoot, "TestResults") 
                    : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestResults");
            }
        }
        
        static TestConfiguration()
        {
            // Ensure directories exist
            Directory.CreateDirectory(ScreenshotPath);
            Directory.CreateDirectory(ReportPath);
        }
    }
}
