# Wikipedia Playwright Automation Framework

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Playwright](https://img.shields.io/badge/Playwright-1.47-45ba4b?logo=playwright)](https://playwright.dev/)
[![NUnit](https://img.shields.io/badge/NUnit-4.1-22B14C?logo=nunit)](https://nunit.org/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## 📋 Overview
This is a comprehensive test automation framework built with **C# and Playwright** for the **Genpact QA Home Assignment**. It demonstrates clean architecture, modern testing practices, and professional-grade automation.

## 🎯 Target
**URL:** https://en.wikipedia.org/wiki/Playwright_(software)  
**Focus:** Debugging features section

## 🏗️ Project Structure
```
WikipediaPlaywrightTests/
├── Config/                      # Configuration & Settings
│   └── TestConfiguration.cs     # Centralized configuration
├── PageObjects/                 # Page Object Model (POM)
│   ├── BasePage.cs             # Base page with common operations
│   └── WikipediaPlaywrightPage.cs # Wikipedia-specific page
├── Services/                    # API Services
│   └── MediaWikiApiService.cs  # MediaWiki Parse API integration
├── Utils/                       # Utility Classes
│   ├── TextNormalizer.cs       # Text processing utilities
│   ├── ExtentReportManager.cs  # HTML report generation
│   ├── TestDataHelper.cs       # Test data management
│   └── BrowserHelper.cs        # Browser operation helpers
├── Tests/                       # Test Cases
│   ├── BaseTest.cs             # Base test with setup/teardown
│   ├── Task1_DebuggingFeaturesTests.cs    # Task 1 tests
│   ├── Task2_MicrosoftDevToolsTests.cs    # Task 2 tests
│   ├── Task3_ColorThemeTests.cs           # Task 3 tests
│   ├── CompleteTestSuite.cs               # Full test suite
│   └── UnitTests/              # Unit tests
│       └── TextNormalizerTests.cs
├── .github/workflows/           # CI/CD
│   └── playwright.yml          # GitHub Actions workflow
├── test-results/                # Test results & reports
└── Screenshots/                 # Test failure screenshots
```

## ✅ Test Cases

### Task 1: Extract and Compare Debugging Features Section
**Objective:** Compare content extracted via UI and API

**Steps:**
1. ✅ Extract "Debugging features" section via UI using POM approach
2. ✅ Extract "Debugging features" section via MediaWiki Parse API
3. ✅ Normalize both texts (remove special chars, standardize format)
4. ✅ Count unique words in each text
5. ✅ Assert that both counts are equal

**Key Features:**
- Dual extraction methods (UI + API)
- Text normalization with regex
- Unique word counting
- Detailed comparison reporting
- Tolerance-based assertion

### Task 2: Validate Microsoft Development Tools Technology Links
**Objective:** Ensure all technology names are clickable links

**Steps:**
1. ✅ Navigate to Microsoft development tools section
2. ✅ Extract all technology names
3. ✅ Validate each technology name is a link
4. ✅ Fail test if any technology is not a link

**Implementation Options:**
- Single test validating all technologies (primary)
- Multiple tests, one per technology (alternative)

**Key Features:**
- Comprehensive link validation
- Clear failure reporting
- Detailed logging per technology

### Task 3: Theme Color Change
**Objective:** Change theme to Dark and verify the change

**Steps:**
1. ✅ Navigate to Wikipedia Playwright page
2. ✅ Capture initial theme state
3. ✅ Click "Color (beta)" section
4. ✅ Select "Dark" mode
5. ✅ Verify theme changed (CSS classes + computed styles)
6. ✅ Take before/after screenshots

**Key Features:**
- Multiple verification methods (CSS classes, computed styles)
- Screenshot evidence
- State comparison
- Robust theme detection

## 🚀 Quick Start

### Prerequisites
- ✅ .NET 8.0 SDK or higher
- ✅ PowerShell (for Playwright browser installation)
- ✅ Git (optional, for version control)

### Automated Setup

**macOS/Linux:**
```bash
chmod +x setup.sh
./setup.sh
```

**Windows:**
```powershell
.\setup.ps1
```

### Manual Setup
```bash
# 1. Restore NuGet packages
dotnet restore

# 2. Build the project
dotnet build

# 3. Install Playwright browsers
pwsh bin/Debug/net8.0/playwright.ps1 install
```

## 🧪 Running Tests

### Run All Tests
```bash
dotnet test
```

### Run Specific Task
```bash
# Task 1 - Extract and compare sections
dotnet test --filter "FullyQualifiedName~Task1"

# Task 2 - Validate technology links  
dotnet test --filter "FullyQualifiedName~Task2"

# Task 3 - Change color theme
dotnet test --filter "FullyQualifiedName~Task3"
```

### Run by Category
```bash
# UI tests only
dotnet test --filter "Category=UI"

# API tests only
dotnet test --filter "Category=API"

# Integration tests
dotnet test --filter "Category=Integration"

# Unit tests
dotnet test --filter "Category=Unit"
```

### Run with Detailed Output
```bash
dotnet test --verbosity detailed
```

## 📊 Reports & Results

### HTML Report (ExtentReports)
Beautiful, interactive HTML report with:
- ✅ Test execution summary
- ✅ Pass/Fail status with percentages
- ✅ Execution time per test
- ✅ Screenshots on failure
- ✅ Detailed step-by-step logs
- ✅ System information

**View Report:**
```bash
# macOS
open test-results/TestReport.html

# Linux
xdg-open test-results/TestReport.html

# Windows
start test-results/TestReport.html
```

### Console Output
Real-time test execution feedback with:
- ✓ Green checkmarks for passed assertions
- ✗ Red X marks for failed assertions
- Detailed logging of each step
- Timing information

### Screenshots
Automatic screenshot capture on test failure:
```
Screenshots/
├── Task1_CompareDebugging_20241022_143052.png
├── Task2_ValidateTechnology_20241022_143125.png
└── Task3_ChangeColor_20241022_143158.png
```

## 🏛️ Architecture & Design Patterns

### Clean Architecture Principles
- ✅ **Separation of Concerns**: Clear layers (Config, Services, PageObjects, Tests)
- ✅ **Single Responsibility**: Each class has one clear purpose
- ✅ **DRY (Don't Repeat Yourself)**: Common functionality in base classes
- ✅ **SOLID Principles**: Loosely coupled, highly cohesive design

### Design Patterns
1. **Page Object Model (POM)**: Encapsulates page structure and behavior
2. **Singleton Pattern**: Single report manager instance
3. **Factory Pattern**: Test creation through NUnit framework
4. **Strategy Pattern**: Multiple extraction strategies (UI + API)

### Best Practices
- ✅ Async/Await for all I/O operations
- ✅ Proper exception handling with context
- ✅ Resource cleanup in teardown
- ✅ Centralized configuration
- ✅ Comprehensive logging
- ✅ Clear assertion messages
- ✅ Test independence (no dependencies between tests)
- ✅ Idempotent tests (can run multiple times)

## 🛠️ Technology Stack

| Technology | Version | Purpose |
|------------|---------|---------|
| .NET | 8.0 | Runtime framework |
| C# | 12.0 | Programming language |
| Playwright | 1.47.0 | Browser automation |
| NUnit | 4.1.0 | Test framework |
| ExtentReports | 5.0.2 | HTML reporting |
| FluentAssertions | 6.12.0 | Fluent assertion library |
| Newtonsoft.Json | 13.0.3 | JSON parsing |

## 📚 Documentation

- **[QUICKSTART.md](QUICKSTART.md)** - Quick setup and execution guide
- **[ARCHITECTURE.md](ARCHITECTURE.md)** - Detailed architecture documentation
- **Inline Code Comments** - Comprehensive documentation in code

## 🔧 Configuration

All configuration is centralized in `Config/TestConfiguration.cs`:

```csharp
public static class TestConfiguration
{
    public static string BaseUrl => "https://en.wikipedia.org";
    public static bool Headless => false;  // Set to true for CI/CD
    public static int DefaultTimeout => 30000;  // 30 seconds
    public static string BrowserType => "chromium";  // chromium, firefox, webkit
}
```

## 🐛 Troubleshooting

### Playwright browsers not installed
```bash
pwsh bin/Debug/net8.0/playwright.ps1 install --with-deps
```

### Build errors
```bash
dotnet clean
dotnet restore
dotnet build
```

### Tests timing out
Increase timeout in `TestConfiguration.cs`:
```csharp
public static int DefaultTimeout => 60000; // 60 seconds
```

### Wikipedia page structure changed
Update locators in `WikipediaPlaywrightPage.cs`

## 🚀 CI/CD Integration

GitHub Actions workflow included (`.github/workflows/playwright.yml`):
- ✅ Automated test execution on push/PR
- ✅ Multi-platform support (Ubuntu, Windows, macOS)
- ✅ Artifact upload (reports, screenshots)
- ✅ Test result publishing

## 📈 Future Enhancements

- [ ] Parallel test execution
- [ ] Data-driven tests with external data sources
- [ ] Cross-browser testing (Firefox, WebKit)
- [ ] Performance metrics and assertions
- [ ] Docker containerization
- [ ] Allure reporting integration
- [ ] API contract testing
- [ ] Visual regression testing

## 📝 Assignment Completion Checklist

- ✅ Task 1: Extract & compare Debugging features (UI + API)
- ✅ Task 2: Validate Microsoft development tools links
- ✅ Task 3: Change color theme to Dark
- ✅ Clean architecture implementation
- ✅ POM approach for UI tests
- ✅ MediaWiki Parse API integration
- ✅ Text normalization
- ✅ Unique word counting
- ✅ HTML report generation (ExtentReports)
- ✅ Screenshot on failure
- ✅ Comprehensive logging
- ✅ Unit tests for utilities
- ✅ CI/CD workflow (GitHub Actions)
- ✅ Complete documentation

## 📧 Contact

For questions or clarification about this implementation, please refer to:
- Inline code comments
- Test execution logs
- HTML report (test-results/TestReport.html)
- Architecture documentation (ARCHITECTURE.md)

## 📄 License

This project is created for the Genpact QA Home Assignment.

---

**Created with ❤️ for Genpact QA Assignment**
