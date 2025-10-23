# Running Tests with Automated Report Opening

This project includes scripts to automatically open the Extent Test Report after running tests.

## 🚀 Quick Start

### Option 1: Using Shell Script (macOS/Linux) - **RECOMMENDED for macOS**

```bash
./run-tests.sh
```

This will:
1. Run all tests with `dotnet test`
2. Display test results in the terminal
3. Automatically open `TestResults/ExtentTestReport.html` in your default browser

### Option 2: Using PowerShell Script (Windows/macOS/Linux)

```powershell
./run-tests.ps1
```

### Option 3: Manual (Traditional Way)

```bash
# Run tests
dotnet test

# Then manually open the report
open TestResults/ExtentTestReport.html  # macOS
start TestResults/ExtentTestReport.html  # Windows
xdg-open TestResults/ExtentTestReport.html  # Linux
```

## 📋 Script Features

Both scripts provide:
- ✅ Colored output for better readability
- ✅ Test execution status (success/failure)
- ✅ Automatic report opening in default browser
- ✅ Error handling (checks if report exists)
- ✅ Exit codes matching test results

## 🎯 Running Specific Tests

You can still use standard `dotnet test` filters with the scripts:

### Run only Task1, Task2, Task3

**Shell Script:**
```bash
# Edit run-tests.sh and change the dotnet test line to:
dotnet test --filter "FullyQualifiedName~Task1_DebuggingFeaturesTests|FullyQualifiedName~Task2_MicrosoftDevToolsTests|FullyQualifiedName~Task3_ColorThemeTests"
```

**Or use dotnet test directly:**
```bash
dotnet test --filter "FullyQualifiedName~Task1_DebuggingFeaturesTests|FullyQualifiedName~Task2_MicrosoftDevToolsTests|FullyQualifiedName~Task3_ColorThemeTests"

# Then open report manually
open TestResults/ExtentTestReport.html
```

### Run by Category

```bash
dotnet test --filter "Category=UI"
dotnet test --filter "Category=API"
dotnet test --filter "Category=Integration"
```

## 📊 Report Location

The Extent Test Report is generated at:
```
TestResults/ExtentTestReport.html
```

This report includes:
- Test execution summary
- Pass/Fail status with ✓/✗ indicators
- Detailed logs for each test
- Screenshots (for failed tests)
- Execution timeline
- Dark theme for better readability

## 🛠️ Troubleshooting

### Script not executable (macOS/Linux)
```bash
chmod +x run-tests.sh
```

### Report not opening automatically
- **macOS:** Ensure `open` command is available (it's built-in)
- **Windows:** Ensure PowerShell execution policy allows scripts:
  ```powershell
  Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
  ```
- **Linux:** Ensure `xdg-open` is installed

### Report not generated
- Ensure tests have run completely
- Check that `ExtentReportManager.cs` is properly configured
- Verify `TestResults` folder exists and has write permissions

## 📝 Script Customization

You can customize the scripts to:
- Add specific test filters
- Change report path
- Add pre/post test actions
- Send notifications
- Upload reports to cloud storage

## 🎨 Example Output

```
🧪 Running tests...

Test run for WikipediaPlaywrightTests.dll
...
Tests completed successfully!

==========================================
✅ Tests completed successfully!
📊 Opening Extent Test Report...
   Report location: TestResults/ExtentTestReport.html
✅ Report opened in browser!
==========================================
```

## 🔗 Additional Resources

- [NUnit Documentation](https://docs.nunit.org/)
- [Playwright for .NET](https://playwright.dev/dotnet/)
- [ExtentReports Documentation](https://www.extentreports.com/)
