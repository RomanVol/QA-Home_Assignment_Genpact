# 📁 Complete Project Structure

```
QA- Home_Assignment_Genpact/
│
├── 📄 WikipediaPlaywrightTests.sln          # Visual Studio Solution
├── 📄 WikipediaPlaywrightTests.csproj       # Project file with NuGet packages
├── 📄 global.json                           # .NET SDK version specification
├── 📄 .gitignore                           # Git ignore rules
│
├── 📚 Documentation/
│   ├── 📄 README.md                        # Main documentation (10,000+ words)
│   ├── 📄 GETTING_STARTED.md               # Step-by-step setup guide
│   ├── 📄 QUICKSTART.md                    # Quick reference guide
│   ├── 📄 ARCHITECTURE.md                  # Architecture documentation
│   ├── 📄 FLEXIBILITY_GUIDE.md             # NEW: Flexibility & SOLID principles guide
│   ├── 📄 INSTALLATION.md                  # .NET SDK installation guide
│   ├── 📄 SUMMARY.md                       # Project summary
│   ├── 📄 PROJECT_STATUS.md                # Completion status
│   ├── 📄 PROJECT_STRUCTURE.md             # This file
│   ├── 📄 INDEX.md                         # Documentation index
│   ├── 📄 START_HERE.md                    # Getting started pointer
│   ├── 📄 VISUAL_SUMMARY.md                # Visual project summary
│   └── 📄 COMPLETE_SUMMARY.md              # Complete project overview
│
├── ⚙️ Config/
│   └── 📄 TestConfiguration.cs             # Centralized configuration
│       ├── BaseUrl, MediaWikiApiUrl
│       ├── DefaultPageTitle (Playwright_(software))
│       ├── WikipediaPlaywrightUrl (backward compatible)
│       ├── GetWikipediaPageUrl(pageTitle) - NEW: Dynamic URL generation
│       ├── Browser settings (headless, timeout, type)
│       ├── Screenshot path
│       └── Report path
│
├── 📦 PageObjects/ (Page Object Model)
│   ├── 📄 BasePage.cs                      # Base page with common operations
│   │   ├── NavigateTo()
│   │   ├── GetPageTitle()
│   │   ├── WaitForLoad()
│   │   ├── TakeScreenshot()
│   │   ├── GetThemeClass()                  # Task 3: Get current theme
│   │   ├── IsNightTheme()                   # Task 3: Check if night theme
│   │   ├── IsDayTheme()                     # Task 3: Check if day theme
│   │   ├── OpenAppearanceMenu()             # Task 3: Open appearance menu
│   │   ├── SwitchToDayTheme()              # Task 3: Switch to day theme
│   │   ├── SwitchToNightTheme()            # Task 3: Switch to night theme
│   │   ├── ToggleTheme()                    # Task 3: Toggle theme
│   │   └── IsAppearanceMenuAccessible()    # Task 3: Verify menu access
│   │
│   └── 📄 WikipediaPlaywrightPage.cs       # Wikipedia-specific page
│       ├── GetDebuggingFeaturesText()       # Task 1: Extract section via UI
│       └── GetMicrosoftDevToolsTechnologies() # Task 2: Get tech names
│
├── 🔌 Services/ (API Integration)
│   └── 📄 MediaWikiApiService.cs           # MediaWiki Parse API (SOLID refactored)
│       ├── GetSectionText()                 # Main: Extract section via API
│       ├── FindSectionNumberByTitle()       # Find section by title
│       ├── FetchSectionHtml()               # Fetch HTML from API
│       ├── ExtractTextFromHtml()            # Coordinate text extraction
│       ├── ExtractParagraphText()           # Extract paragraph text
│       ├── ExtractListItemsText()           # Extract list items
│       ├── CleanAndDecodeText()             # Clean and decode text
│       └── GetAllSections()                 # Get all page sections
│
├── 🛠️ Utils/ (Utility Classes)
│   ├── 📄 TextNormalizer.cs                # Text processing utilities
│   │   ├── Normalize()                      # Task 1: Normalize text
│   │   ├── CountUniqueWords()              # Task 1: Count words
│   │   └── GetUniqueWords()                # Task 1: Get word set
│   │
│   └── 📄 ExtentReportManager.cs           # HTML report generation
│       ├── GetInstance()                    # Singleton pattern
│       └── Flush()                          # Generate report
│
├── 🧪 Tests/
│   ├── 📄 BaseTest.cs                      # Base test class
│   │   ├── Setup() - Test initialization
│   │   ├── TearDown() - Cleanup & reporting
│   │   ├── LogInfo(), LogPass(), LogFail()
│   │   └── Screenshot on failure
│   │
│   ├── 📄 Task1_DebuggingFeaturesTests.cs  # Task 1: Extract & Compare
│   │   ├── ✅ Task1_CompareDebuggingFeaturesSectionFromUIAndAPI
│   │   │   └── Extracts via UI, API, normalizes, counts, asserts
│   │   ├── ✅ VerifyDebuggingFeaturesSectionExtractionViaUI
│   │   │   └── Validates UI extraction independently
│   │   └── ✅ VerifyDebuggingFeaturesSectionExtractionViaAPI
│   │       └── Validates API extraction independently
│   │
│   ├── 📄 Task2_MicrosoftDevToolsTests.cs  # Task 2: Validate Links
│   │   ├── ✅ Task2_ValidateAllTechnologyNamesAreLinks
│   │   │   └── Validates all tech names are clickable
│   │   └── ✅ Task2_ValidateIndividualTechnologyIsLink
│   │       └── Parameterized test for individual validation
│   │
│   ├── 📄 Task3_ColorThemeTests.cs         # Task 3: Theme Change
│   │   ├── ✅ Task3_ChangeColorToDarkAndValidate
│   │   │   └── Changes to Dark, validates change
│   │   └── ✅ VerifyColorBetaSectionIsAccessible
│   │       └── Validates UI accessibility
│   │
│   └── 📄 ExampleTests_DifferentWikipediaPages.cs  # NEW: Example tests
│       ├── ✅ Example_TestPythonPage
│       ├── ✅ Example_TestSeleniumPage
│       ├── ✅ Example_ExtractSectionFromDifferentPage
│       └── ✅ Example_TestMultipleProgrammingLanguages (TestCase)
│
├── 🤖 .github/
│   └── workflows/
│       └── 📄 playwright.yml               # GitHub Actions CI/CD
│           ├── Setup .NET
│           ├── Restore dependencies
│           ├── Build project
│           ├── Install Playwright browsers
│           ├── Run tests
│           └── Upload artifacts (reports, screenshots)
│
├── 🔧 Setup Scripts/
│   ├── 📄 setup.sh                         # Unix/macOS setup script
│   │   ├── Check .NET installation
│   │   ├── Restore NuGet packages
│   │   ├── Build project
│   │   └── Install Playwright browsers
│   │
│   └── 📄 setup.ps1                        # Windows PowerShell setup
│       └── Same steps as setup.sh
│
├── 📊 TestResults/ (Generated during test execution)
│   └── ExtentTestReport.html               # ExtentReports HTML report
│
└── 📸 Screenshots/ (Generated on test failure)
    ├── Task1_TestName_timestamp.png
    ├── Task2_TestName_timestamp.png
    └── Task3_TestName_timestamp.png
```

## 📈 File Statistics

### Source Code Files
- **C# Files**: 12
  - Config: 1
  - PageObjects: 2
  - Services: 1
  - Utils: 2
  - Tests: 5
  - Base Classes: 1

### Configuration Files
- **Project Files**: 3
  - `.sln`, `.csproj`, `global.json`
- **CI/CD**: 1
  - `playwright.yml`
- **Git**: 1
  - `.gitignore`

### Documentation Files
- **Markdown Docs**: 12
  - Main docs: 11
  - CI/CD docs: 1 (in workflow comments)

### Setup Scripts
- **Scripts**: 2
  - Unix/Mac: 1
  - Windows: 1

### Total Files: 30 files

## 📊 Code Metrics

### Lines of Code (Estimated)
- **Configuration**: ~80 lines (improved flexibility)
- **PageObjects**: ~450 lines (BasePage expanded)
- **Services**: ~200 lines (SOLID refactored)
- **Utils**: ~200 lines (cleaned up)
- **Tests**: ~900 lines (added examples)
- **Documentation**: ~6,000+ lines (added FLEXIBILITY_GUIDE.md)
- **Total Code**: ~1,800+ lines
- **Total with Docs**: ~8,000+ lines

### Test Metrics
- **Total Tests**: 11
- **Test Classes**: 4
- **Categories**: 4 (UI, API, Integration, Example)
- **Test Methods**: 11

## 🎯 File Purposes

### Configuration Layer
| File | Purpose | Key Content |
|------|---------|-------------|
| `TestConfiguration.cs` | Central config | URLs, timeouts, paths |
| `global.json` | .NET version | SDK version specification |
| `.csproj` | Project definition | NuGet packages, settings |

### Page Object Layer (POM)
| File | Purpose | Key Content |
|------|---------|-------------|
| `BasePage.cs` | Common page ops | Navigate, screenshot, wait |
| `WikipediaPlaywrightPage.cs` | Wikipedia page | All locators and actions |

### Service Layer
| File | Purpose | Key Content |
|------|---------|-------------|
| `MediaWikiApiService.cs` | API integration | 7 focused methods (SOLID) |

### Utility Layer
| File | Purpose | Key Content |
|------|---------|-------------|
| `TextNormalizer.cs` | Text processing | Normalize, count words |
| `ExtentReportManager.cs` | HTML reports | Report generation |

### Test Layer
| File | Tests | Purpose |
|------|-------|---------|
| `BaseTest.cs` | 0 | Base class with setup/teardown |
| `Task1_DebuggingFeaturesTests.cs` | 3 | Extract & compare sections |
| `Task2_MicrosoftDevToolsTests.cs` | 2 | Validate technology links |
| `Task3_ColorThemeTests.cs` | 2 | Change theme to Dark/Night |
| `ExampleTests_DifferentWikipediaPages.cs` | 4 | Example tests for flexibility |

## 🔍 Important Files for Review

### Must Review (Core Implementation)
1. ✅ `README.md` - Start here!
2. ✅ `Tests/Task1_DebuggingFeaturesTests.cs` - Task 1 implementation
3. ✅ `Tests/Task2_MicrosoftDevToolsTests.cs` - Task 2 implementation
4. ✅ `Tests/Task3_ColorThemeTests.cs` - Task 3 implementation
5. ✅ `PageObjects/WikipediaPlaywrightPage.cs` - POM implementation
6. ✅ `Services/MediaWikiApiService.cs` - API integration

### Should Review (Architecture)
7. ✅ `ARCHITECTURE.md` - Design details
8. ✅ `FLEXIBILITY_GUIDE.md` - NEW: SOLID & flexibility patterns
9. ✅ `Config/TestConfiguration.cs` - Configuration
10. ✅ `Utils/TextNormalizer.cs` - Text processing
11. ✅ `Tests/BaseTest.cs` - Test base class
12. ✅ `PageObjects/BasePage.cs` - Base page with theme methods

### Nice to Review (Supporting)
13. ✅ `GETTING_STARTED.md` - Setup guide
14. ✅ `Tests/ExampleTests_DifferentWikipediaPages.cs` - Example tests
15. ✅ `Utils/ExtentReportManager.cs` - Reporting
16. ✅ `.github/workflows/playwright.yml` - CI/CD
17. ✅ `PROJECT_STATUS.md` - Completion status

## 📦 Dependencies (NuGet Packages)

```xml
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.10.0" />
<PackageReference Include="Microsoft.Playwright" Version="1.47.0" />
<PackageReference Include="Microsoft.Playwright.NUnit" Version="1.47.0" />
<PackageReference Include="NUnit" Version="4.1.0" />
<PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
<PackageReference Include="coverlet.collector" Version="6.0.2" />
<PackageReference Include="ExtentReports" Version="5.0.2" />
<PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
<PackageReference Include="FluentAssertions" Version="6.12.0" />
```

## 🎯 Navigation Guide

### For Quick Setup
1. Read `GETTING_STARTED.md`
2. Install .NET SDK
3. Run `./setup.sh`
4. Run `dotnet test`

### For Understanding Architecture
1. Read `ARCHITECTURE.md`
2. Review `Config/TestConfiguration.cs`
3. Explore `PageObjects/` layer
4. Study `Tests/BaseTest.cs`

### For Running Tests
1. Check `QUICKSTART.md`
2. Review `Tests/` directory
3. Run specific tasks
4. Check `test-results/TestReport.html`

### For Code Review
1. Start with `README.md`
2. Review test files in `Tests/`
3. Check POM in `PageObjects/`
4. Examine utilities in `Utils/`

---

## 🆕 Recent Updates (October 23, 2025)

### SOLID Refactoring
- ✅ MediaWikiApiService split into 7 focused methods
- ✅ Single Responsibility Principle applied
- ✅ Better testability and maintainability

### Flexibility Improvements
- ✅ Dynamic URL generation with `GetWikipediaPageUrl()`
- ✅ Support for testing any Wikipedia page
- ✅ Example tests for different pages (Python, Selenium, JavaScript, etc.)
- ✅ Backward compatibility maintained

### Code Cleanup
- ✅ Removed unused utilities (TestDataHelper, BrowserHelper)
- ✅ Moved theme functionality to BasePage
- ✅ Cleaned up WikipediaPlaywrightPage
- ✅ Removed unused locators and methods

### Documentation
- ✅ Added FLEXIBILITY_GUIDE.md
- ✅ Updated all documentation files
- ✅ Added usage examples

---

**Last Updated**: October 23, 2025  
**Total Files**: 30  
**Total Tests**: 11  
**Status**: ✅ Complete & Ready for Submission  
**Architecture**: ✅ SOLID Principles Applied  
**Flexibility**: ✅ Supports Multiple Wikipedia Pages
