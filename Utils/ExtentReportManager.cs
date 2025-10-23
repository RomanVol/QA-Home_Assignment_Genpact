using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using WikipediaPlaywrightTests.Config;

namespace WikipediaPlaywrightTests.Utils
{
    public class ExtentReportManager
    {
        private static ExtentReports? _extent;
        private static readonly object _lock = new object();
        
        public static ExtentReports GetInstance()
        {
            if (_extent == null)
            {
                lock (_lock)
                {
                    if (_extent == null)
                    {
                        var reportPath = Path.Combine(TestConfiguration.ReportPath, "ExtentTestReport.html");
                        var htmlReporter = new ExtentSparkReporter(reportPath);
                        
                        htmlReporter.Config.DocumentTitle = "Wikipedia Playwright Tests";
                        htmlReporter.Config.ReportName = "Genpact QA Assignment";
                        htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;
                        
                        _extent = new ExtentReports();
                        _extent.AttachReporter(htmlReporter);
                        
                        _extent.AddSystemInfo("Framework", "Playwright with C#");
                        _extent.AddSystemInfo("Environment", "Wikipedia");
                        _extent.AddSystemInfo("Tester", "Genpact QA Assignment");
                    }
                }
            }
            return _extent;
        }
        
        public static void Flush()
        {
            _extent?.Flush();
        }
    }
}
