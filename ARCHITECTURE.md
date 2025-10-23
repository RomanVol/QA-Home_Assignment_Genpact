# Genpact QA Assignment - Implementation Guide

## Architecture Overview

This project follows a clean architecture pattern with clear separation of concerns:

### Layer Structure

```
├── Config/                 # Configuration and settings
│   └── TestConfiguration.cs
├── Utils/                  # Utility classes
│   ├── TextNormalizer.cs
│   └── ExtentReportManager.cs
├── Services/               # API services
│   └── MediaWikiApiService.cs
├── PageObjects/            # Page Object Model
│   ├── BasePage.cs
│   └── WikipediaPlaywrightPage.cs
└── Tests/                  # Test cases
    ├── BaseTest.cs
    ├── Task1_DebuggingFeaturesTests.cs
    ├── Task2_MicrosoftDevToolsTests.cs
    └── Task3_ColorThemeTests.cs
```

## Design Patterns Used

### 1. Page Object Model (POM)
- `BasePage`: Contains common page operations
- `WikipediaPlaywrightPage`: Specific page with locators and actions
- Encapsulates page structure and behavior
- Improves test maintenance and readability

### 2. Singleton Pattern
- `ExtentReportManager`: Single instance for report generation
- `TestConfiguration`: Centralized configuration

### 3. Factory Pattern
- Test creation through NUnit framework
- Browser context creation through Playwright

### 4. Builder Pattern
- Test setup and teardown with consistent configuration

## Key Features

### Clean Code Principles
- **Single Responsibility**: Each class has one clear purpose
- **DRY (Don't Repeat Yourself)**: Common functionality in base classes
- **SOLID Principles**: Loosely coupled, highly cohesive design
- **Meaningful Names**: Clear, descriptive naming conventions

### Test Structure
- **Arrange-Act-Assert**: Clear test structure
- **Independent Tests**: Each test can run independently
- **Descriptive Logging**: Detailed logs for debugging
- **Screenshot on Failure**: Automatic screenshot capture

### Reporting
- **ExtentReports**: Beautiful HTML reports
- **NUnit Reports**: Standard test framework reports
- **Console Logs**: Real-time test execution feedback

## Task Implementation Details

### Task 1: Extract and Compare
**Approach:**
1. UI Extraction: Uses POM to navigate and extract section text
2. API Extraction: Uses MediaWiki Parse API to get same content
3. Text Normalization: Removes HTML artifacts, standardizes format
4. Comparison: Counts unique words and asserts equality

**Key Classes:**
- `WikipediaPlaywrightPage.GetDebuggingFeaturesTextWithUI()`
- `MediaWikiApiService.GetSectionText()`
- `TextNormalizer.Normalize()` and `CountUniqueWords()`

### Task 2: Validate Technology Links
**Approach:**
1. Navigate to Microsoft development tools section
2. Extract all technology names
3. Check if each has an associated link
4. Fail test if any technology is not a link

**Implementation Options:**
- Single test validating all technologies (implemented)
- Multiple tests, one per technology (also provided)

**Key Methods:**
- `WikipediaPlaywrightPage.GetMicrosoftDevToolsTechnologies()`

### Task 3: Theme Color Change
**Approach:**
1. Capture initial theme state
2. Click appearance/color button
3. Select Dark mode
4. Verify theme changed by checking CSS classes and computed styles
5. Screenshots for visual verification

**Key Methods:**
- `WikipediaPlaywrightPage.ChangeColorToDark()`
- `WikipediaPlaywrightPage.IsDarkModeActive()`

## Test Execution

### Run All Tests
```bash
dotnet test
```

### Run Specific Category
```bash
dotnet test --filter "Category=UI"
dotnet test --filter "Category=API"
dotnet test --filter "Category=Integration"
```

### Run Specific Test
```bash
dotnet test --filter "FullyQualifiedName~Task1_CompareDebuggingFeaturesSectionFromUIAndAPI"
```

### Run with Verbose Output
```bash
dotnet test --verbosity detailed
```

## HTML Report

After test execution, open:
```
test-results/TestReport.html
```

The report includes:
- Test execution summary
- Pass/Fail status
- Execution time
- Screenshots (on failure)
- Detailed logs

## Best Practices Implemented

1. **Async/Await**: All I/O operations are asynchronous
2. **Exception Handling**: Proper error messages and context
3. **Resource Management**: Proper cleanup in teardown
4. **Configuration Management**: Centralized settings
5. **Logging**: Comprehensive logging at all levels
6. **Assertions**: Clear, descriptive assertion messages
7. **Test Independence**: No test dependencies
8. **Idempotency**: Tests can be run multiple times

## Extensibility

### Adding New Tests
1. Create new test class inheriting from `BaseTest`
2. Use `LogInfo`, `LogPass`, `LogFail` for reporting
3. Follow Arrange-Act-Assert pattern

### Adding New Pages
1. Create new page class inheriting from `BasePage`
2. Define locators as constants
3. Implement page-specific methods

### Adding New API Services
1. Create service class with HttpClient
2. Implement API methods with error handling
3. Return strongly-typed results

## Troubleshooting

### Tests Failing Due to Page Changes
- Update locators in `WikipediaPlaywrightPage.cs`
- Wikipedia structure may change over time

### API Tests Failing
- Check MediaWiki API is accessible
- Verify page and section names are correct

### Theme Change Not Detected
- Wikipedia may have updated their theme system
- Check browser console for errors
- Update theme detection logic in `IsDarkModeActive()`

## Future Enhancements

1. **Parallel Test Execution**: Enable NUnit parallelization
2. **Data-Driven Tests**: Externalize test data to JSON/CSV
3. **Cross-Browser Testing**: Test on Firefox, WebKit
4. **Performance Metrics**: Add response time assertions
5. **CI/CD Integration**: GitHub Actions workflow (already provided)
6. **Docker Support**: Containerize test execution
7. **Allure Reporting**: Alternative reporting framework

## Contact & Support

For questions about implementation:
- Check inline code comments
- Review test execution logs
- Examine HTML report for detailed results
