# Quick Start Guide

## Prerequisites
- .NET 8.0 SDK or higher
- PowerShell (for Playwright browser installation)
- Git (for version control)

## Setup (Quick)

### macOS/Linux
```bash
chmod +x setup.sh
./setup.sh
```

### Windows
```powershell
.\setup.ps1
```

### Manual Setup
```bash
# Restore packages
dotnet restore

# Build project
dotnet build

# Install Playwright browsers
pwsh bin/Debug/net8.0/playwright.ps1 install
```

## Running Tests

### Run all tests
```bash
dotnet test
```

### Run specific task
```bash
# Task 1 - Extract and compare sections
dotnet test --filter "FullyQualifiedName~Task1"

# Task 2 - Validate technology links
dotnet test --filter "FullyQualifiedName~Task2"

# Task 3 - Change color theme
dotnet test --filter "FullyQualifiedName~Task3"
```

### Run by category
```bash
dotnet test --filter "Category=UI"
dotnet test --filter "Category=API"
dotnet test --filter "Category=Integration"
```

## View Results

### HTML Report
After running tests, open the HTML report:
```bash
# macOS
open test-results/TestReport.html

# Linux
xdg-open test-results/TestReport.html

# Windows
start test-results/TestReport.html
```

### Console Output
Test results are printed to console in real-time with:
- ✓ for passed assertions
- ✗ for failed assertions
- Detailed logging of each step

### Screenshots
On test failure, screenshots are automatically saved to:
```
Screenshots/
```

## Project Structure
```
├── Config/                     # Configuration files
├── PageObjects/                # Page Object Model classes
├── Services/                   # API service classes
├── Utils/                      # Utility classes
├── Tests/                      # Test cases
│   ├── Task1_DebuggingFeaturesTests.cs
│   ├── Task2_MicrosoftDevToolsTests.cs
│   └── Task3_ColorThemeTests.cs
├── test-results/               # Test results and reports
└── Screenshots/                # Test failure screenshots
```

## Common Issues

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
Increase timeout in `Config/TestConfiguration.cs`:
```csharp
public static int DefaultTimeout => 60000; // 60 seconds
```

## Next Steps

1. Review `ARCHITECTURE.md` for detailed design information
2. Check test results in HTML report
3. Examine code for implementation details
4. Run tests individually to understand each task

## Support

For issues:
1. Check test execution logs
2. Review screenshots in `Screenshots/` folder
3. Examine HTML report for detailed information
4. Check inline code comments for implementation details
