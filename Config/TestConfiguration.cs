namespace WikipediaPlaywrightTests.Config
{
    public static class TestConfiguration
    {
        public static string BaseUrl => "https://en.wikipedia.org";
        public static string WikipediaPlaywrightUrl => $"{BaseUrl}/wiki/Playwright_(software)";
        public static string MediaWikiApiUrl => $"{BaseUrl}/w/api.php";
        
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
