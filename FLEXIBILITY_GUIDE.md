# Flexibility Guide: Testing Different Wikipedia Pages

This guide explains how the framework has been designed to easily test different Wikipedia pages beyond just the Playwright page.

## Overview

The framework is now flexible and follows SOLID principles, allowing you to:
- Test any Wikipedia page
- Extract any section from any Wikipedia page
- Reuse the same Page Object Model for different pages
- Maintain backward compatibility with existing tests

## Configuration Changes

### `TestConfiguration.cs`

The configuration now supports dynamic Wikipedia page URLs:

```csharp
// Default page for existing tests
public static string DefaultPageTitle => "Playwright_(software)";

// Backward compatibility - existing code still works
public static string WikipediaPlaywrightUrl => GetWikipediaPageUrl(DefaultPageTitle);

// NEW: Flexible method to get any Wikipedia page URL
public static string GetWikipediaPageUrl(string pageTitle)
{
    return $"{BaseUrl}/wiki/{pageTitle}";
}
```

### `MediaWikiApiService.cs`

The service has been refactored following the **Single Responsibility Principle**:

**Before:** One monolithic method (130+ lines)

**After:** Seven focused methods:
1. `GetSectionText()` - Main orchestrator
2. `FindSectionNumberByTitle()` - Finds section by title
3. `FetchSectionHtml()` - Fetches HTML from API
4. `ExtractTextFromHtml()` - Coordinates extraction
5. `ExtractParagraphText()` - Extracts paragraph text
6. `ExtractListItemsText()` - Extracts list items
7. `CleanAndDecodeText()` - Cleans and decodes text

## Usage Examples

### Example 1: Test a Different Wikipedia Page

```csharp
[Test]
public async Task TestPythonWikipediaPage()
{
    // Get URL for Python programming language page
    var pythonUrl = TestConfiguration.GetWikipediaPageUrl("Python_(programming_language)");
    
    await _wikipediaPage.NavigateTo(pythonUrl);
    await _wikipediaPage.WaitForLoad();
    
    // Your test logic here
}
```

### Example 2: Extract Section from Different Page via API

```csharp
[Test]
public async Task ExtractHistorySectionFromJavaScriptPage()
{
    var apiService = new MediaWikiApiService();
    
    // Extract "History" section from JavaScript Wikipedia page
    var historyText = await apiService.GetSectionText("JavaScript", "History");
    
    // Assert or process the text
    historyText.Should().NotBeNullOrWhiteSpace();
}
```

### Example 3: Data-Driven Tests with Multiple Pages

```csharp
[TestCase("C_Sharp_(programming_language)", "C#")]
[TestCase("Java_(programming_language)", "Java")]
[TestCase("TypeScript", "TypeScript")]
public async Task TestMultipleProgrammingLanguages(string pageTitle, string expectedKeyword)
{
    var pageUrl = TestConfiguration.GetWikipediaPageUrl(pageTitle);
    
    await _wikipediaPage.NavigateTo(pageUrl);
    await _wikipediaPage.WaitForLoad();
    
    var displayedTitle = await Page.TitleAsync();
    displayedTitle.Should().Contain(expectedKeyword);
}
```

### Example 4: Complete Test File

See `Tests/ExampleTests_DifferentWikipediaPages.cs` for comprehensive examples including:
- Testing Python, Selenium, JavaScript, C#, Java, and TypeScript Wikipedia pages
- Extracting sections from different pages
- Data-driven tests with TestCase attributes

## Benefits of This Approach

✅ **SOLID Principles**
- Single Responsibility: Each method has one clear purpose
- Open/Closed: Open for extension (new pages), closed for modification (existing tests work)

✅ **Flexibility**
- Test any Wikipedia page without code changes
- Extract any section from any page
- Easy to add new test scenarios

✅ **Maintainability**
- Smaller, focused methods are easier to understand and debug
- Each method can be unit tested independently
- Changes to one part don't affect others

✅ **Backward Compatibility**
- All existing tests continue to work without modification
- `WikipediaPlaywrightUrl` still available for legacy code

✅ **Reusability**
- Methods can be reused across different test scenarios
- Same Page Object Model works for all Wikipedia pages

## Running Example Tests

The example tests are marked with `[Explicit]` attribute, so they won't run with your main test suite. To run them:

```bash
# Run a specific example test
dotnet test --filter "FullyQualifiedName~Example_TestPythonPage"

# Run all example tests
dotnet test --filter "Category=Example"
```

## Next Steps

1. **Add More Pages**: Simply use `GetWikipediaPageUrl("Your_Page_Title")`
2. **Extract More Sections**: Call `GetSectionText(pageTitle, sectionTitle)` with any section
3. **Create Custom Page Objects**: Extend `BasePage` for page-specific functionality
4. **Add More Test Scenarios**: Follow the examples in `ExampleTests_DifferentWikipediaPages.cs`

## Migration Guide

If you have existing tests, no changes are needed! But if you want to use the new flexibility:

**Before:**
```csharp
await _wikipediaPage.NavigateTo("https://en.wikipedia.org/wiki/Python_(programming_language)");
```

**After:**
```csharp
var url = TestConfiguration.GetWikipediaPageUrl("Python_(programming_language)");
await _wikipediaPage.NavigateTo(url);
```

This makes your tests more maintainable and easier to update if the base URL changes.
