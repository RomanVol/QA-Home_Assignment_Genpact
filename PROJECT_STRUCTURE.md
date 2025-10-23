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
│   ├── 📄 INSTALLATION.md                  # .NET SDK installation guide
│   ├── 📄 SUMMARY.md                       # Project summary
│   ├── 📄 PROJECT_STATUS.md                # Completion status
│   └── 📄 PROJECT_STRUCTURE.md             # This file
│
├── ⚙️ Config/
│   └── 📄 TestConfiguration.cs             # Centralized configuration
│       ├── BaseUrl, WikipediaPlaywrightUrl
│       ├── MediaWikiApiUrl
│       ├── Browser settings (headless, timeout, type)
│       ├── Screenshot path
│       └── Report path
│
├── 📦 PageObjects/ (Page Object Model)
│   ├── 📄 BasePage.cs                      # Base page with common operations
│   │   ├── NavigateTo()
│   │   ├── GetPageTitle()
│   │   ├── WaitForLoad()
│   │   └── TakeScreenshot()
│   │
│   └── 📄 WikipediaPlaywrightPage.cs       # Wikipedia-specific page
│       ├── GetDebuggingFeaturesText()       # Task 1: Extract section via UI
│       ├── GetMicrosoftDevToolsTechnologies() # Task 2: Get tech names
│       ├── ChangeColorToDark()              # Task 3: Change theme
│       ├── IsDarkModeActive()               # Task 3: Verify theme
│       └── GetCurrentTheme()                # Task 3: Get theme state
│
├── 🔌 Services/ (API Integration)
│   └── 📄 MediaWikiApiService.cs           # MediaWiki Parse API
│       ├── GetSectionText()                 # Task 1: Extract section via API
│       └── GetAllSections()                 # Get all page sections
│
├── 🛠️ Utils/ (Utility Classes)
│   ├── 📄 TextNormalizer.cs                # Text processing utilities
│   │   ├── Normalize()                      # Task 1: Normalize text
│   │   ├── CountUniqueWords()              # Task 1: Count words
│   │   └── GetUniqueWords()                # Task 1: Get word set
│   │
│   ├── 📄 ExtentReportManager.cs           # HTML report generation
│   │   ├── GetInstance()                    # Singleton pattern
│   │   └── Flush()                          # Generate report
│   │
│   ├── 📄 TestDataHelper.cs                # Test data management
│   │   ├── SaveTextToFile()
│   │   ├── GenerateComparisonReport()
│   │   └── CalculateSimilarity()
│   │
│   └── 📄 BrowserHelper.cs                 # Browser operation helpers
│       ├── WaitForCondition()
│       ├── ScrollIntoView()
│       ├── GetComputedStyle()
│       ├── ElementExists()
│       └── WaitForNetworkIdle()
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
│   ├── 📄 CompleteTestSuite.cs             # Full Test Suite
│   │   ├── ✅ RunTask1() - Executes Task 1
│   │   ├── ✅ RunTask2() - Executes Task 2
│   │   └── ✅ RunTask3() - Executes Task 3
│   │
│   └── 📁 UnitTests/
│       └── 📄 TextNormalizerTests.cs       # Unit tests for utilities
│           ├── ✅ Normalize_ShouldNormalizeTextCorrectly (5 cases)
│           ├── ✅ CountUniqueWords_ShouldCountCorrectly
│           ├── ✅ GetUniqueWords_ShouldReturnUniqueWords
│           ├── ✅ Normalize_EmptyString_ShouldReturnEmpty
│           └── ✅ CountUniqueWords_EmptyString_ShouldReturnZero
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
├── 📊 test-results/ (Generated during test execution)
│   ├── TestReport.html                     # ExtentReports HTML report
│   └── TestResults.json                    # Test results metadata
│
└── 📸 Screenshots/ (Generated on test failure)
    ├── Task1_TestName_timestamp.png
    ├── Task2_TestName_timestamp.png
    └── Task3_TestName_timestamp.png
```

## 📈 File Statistics

### Source Code Files
- **C# Files**: 17
  - Config: 1
  - PageObjects: 2
  - Services: 1
  - Utils: 4
  - Tests: 6
  - Unit Tests: 1
  - Base Classes: 2

### Configuration Files
- **Project Files**: 3
  - `.sln`, `.csproj`, `global.json`
- **CI/CD**: 1
  - `playwright.yml`
- **Git**: 1
  - `.gitignore`

### Documentation Files
- **Markdown Docs**: 8
  - Main docs: 7
  - CI/CD docs: 1 (in workflow comments)

### Setup Scripts
- **Scripts**: 2
  - Unix/Mac: 1
  - Windows: 1

### Total Files: 32 files

## 📊 Code Metrics

### Lines of Code (Estimated)
- **Configuration**: ~50 lines
- **PageObjects**: ~300 lines
- **Services**: ~150 lines
- **Utils**: ~400 lines
- **Tests**: ~800 lines
- **Documentation**: ~5,000+ lines
- **Total Code**: ~2,000+ lines
- **Total with Docs**: ~7,000+ lines

### Test Metrics
- **Total Tests**: 15
- **Test Classes**: 5
- **Categories**: 4 (UI, API, Integration, Unit)
- **Test Methods**: 15

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
| `MediaWikiApiService.cs` | API integration | Parse API methods |

### Utility Layer
| File | Purpose | Key Content |
|------|---------|-------------|
| `TextNormalizer.cs` | Text processing | Normalize, count words |
| `ExtentReportManager.cs` | HTML reports | Report generation |
| `TestDataHelper.cs` | Data comparison | Compare, analyze texts |
| `BrowserHelper.cs` | Browser helpers | Wait, scroll, check |

### Test Layer
| File | Tests | Purpose |
|------|-------|---------|
| `BaseTest.cs` | 0 | Base class with setup/teardown |
| `Task1_DebuggingFeaturesTests.cs` | 3 | Extract & compare sections |
| `Task2_MicrosoftDevToolsTests.cs` | 2 | Validate technology links |
| `Task3_ColorThemeTests.cs` | 2 | Change theme to Dark |
| `CompleteTestSuite.cs` | 3 | Run all tasks together |
| `TextNormalizerTests.cs` | 5 | Unit tests for utilities |

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
8. ✅ `Config/TestConfiguration.cs` - Configuration
9. ✅ `Utils/TextNormalizer.cs` - Text processing
10. ✅ `Tests/BaseTest.cs` - Test base class

### Nice to Review (Supporting)
11. ✅ `GETTING_STARTED.md` - Setup guide
12. ✅ `Utils/ExtentReportManager.cs` - Reporting
13. ✅ `.github/workflows/playwright.yml` - CI/CD
14. ✅ `PROJECT_STATUS.md` - Completion status

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

**Last Updated**: October 22, 2025  
**Total Files**: 32  
**Total Tests**: 15  
**Status**: ✅ Complete & Ready for Submission
