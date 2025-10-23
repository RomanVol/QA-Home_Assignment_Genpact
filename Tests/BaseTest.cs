using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using AventStack.ExtentReports;
using WikipediaPlaywrightTests.Utils;
using WikipediaPlaywrightTests.Config;

namespace WikipediaPlaywrightTests.Tests
{
    public class BaseTest : PageTest
    {
        protected ExtentTest? Test;
        protected static ExtentReports? Extent;
        
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Extent = ExtentReportManager.GetInstance();
        }
        
        [SetUp]
        public virtual async Task Setup()
        {
            // Create test in report
            var testName = TestContext.CurrentContext.Test.Name;
            Test = Extent?.CreateTest(testName);
            
            // Configure browser context timeout if context is available
            if (Context != null)
            {
                Context.SetDefaultTimeout(TestConfiguration.DefaultTimeout);
            }
            await Task.CompletedTask;
        }
        
        [TearDown]
        public async Task TearDown()
        {
            var outcome = TestContext.CurrentContext.Result.Outcome.Status;
            var testName = TestContext.CurrentContext.Test.Name;
            
            try
            {
                // Take screenshot on failure
                if (outcome == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    var screenshotPath = Path.Combine(TestConfiguration.ScreenshotPath, 
                        $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                    
                    await Page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath });
                    Test?.AddScreenCaptureFromPath(screenshotPath);
                    Test?.Fail(TestContext.CurrentContext.Result.Message);
                }
                else if (outcome == NUnit.Framework.Interfaces.TestStatus.Passed)
                {
                    Test?.Pass("Test passed successfully");
                }
                else if (outcome == NUnit.Framework.Interfaces.TestStatus.Skipped)
                {
                    Test?.Skip("Test was skipped");
                }
            }
            catch (Exception ex)
            {
                Test?.Warning($"Error during teardown: {ex.Message}");
            }
        }
        
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExtentReportManager.Flush();
        }
        
        protected void LogInfo(string message)
        {
            Test?.Info(message);
            TestContext.WriteLine(message);
        }
        
        protected void LogPass(string message)
        {
            Test?.Pass(message);
            TestContext.WriteLine($"✓ {message}");
        }
        
        protected void LogFail(string message)
        {
            Test?.Fail(message);
            TestContext.WriteLine($"✗ {message}");
        }
    }
}
