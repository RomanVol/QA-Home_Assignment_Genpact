# Getting Started - Complete Guide

## 🎯 Project Overview

This is a **complete test automation framework** for the Genpact QA Home Assignment. It contains:
- ✅ 15 automated tests covering all 3 tasks
- ✅ Clean architecture with POM pattern
- ✅ UI and API testing capabilities
- ✅ HTML report generation
- ✅ Screenshot capture on failures
- ✅ Comprehensive documentation

## 📋 Prerequisites Checklist

Before you begin, ensure you have:

- [ ] **macOS** 11.0 or higher (your current OS)
- [ ] **.NET 8.0 SDK** - [Installation Guide](INSTALLATION.md)
- [ ] **PowerShell** (for Playwright) - Usually pre-installed on macOS
- [ ] **Internet connection** - To download browsers and run tests
- [ ] **Git** (optional) - For version control

## 🚀 Step-by-Step Setup

### Step 1: Install .NET SDK

**.NET is currently NOT installed on your system.** You need to install it first.

**Quick Install (Recommended):**
```bash
# Using Homebrew
brew install --cask dotnet-sdk

# Verify installation
dotnet --version
# Should show: 8.0.x
```

**Detailed instructions:** See [INSTALLATION.md](INSTALLATION.md)

### Step 2: Verify .NET Installation

```bash
# Check .NET version
dotnet --version

# List installed SDKs
dotnet --list-sdks

# You should see .NET 8.0.x in the list
```

### Step 3: Run Automated Setup

```bash
# Navigate to project directory
cd "/Users/roma/QA- Home_Assignment_Genpact"

# Make setup script executable
chmod +x setup.sh

# Run setup
./setup.sh
```

This will:
1. ✅ Restore NuGet packages
2. ✅ Build the project
3. ✅ Install Playwright browsers (Chromium, Firefox, WebKit)

### Step 4: Run Tests

```bash
# Run all tests
dotnet test

# Tests will execute and generate reports
```

### Step 5: View Results

```bash
# Open HTML report in browser
open test-results/TestReport.html

# View screenshots (if any failures)
open Screenshots/
```

## 🔧 Manual Setup (Alternative)

If automated setup doesn't work, follow these manual steps:

```bash
# 1. Navigate to project directory
cd "/Users/roma/QA- Home_Assignment_Genpact"

# 2. Restore NuGet packages
dotnet restore

# 3. Build the project
dotnet build

# 4. Install Playwright browsers
pwsh bin/Debug/net8.0/playwright.ps1 install

# 5. Run tests
dotnet test
```

## 🧪 Running Tests

### Run All Tests (Recommended for first time)
```bash
dotnet test
```

### Run Specific Task
```bash
# Task 1: Extract and compare sections
dotnet test --filter "FullyQualifiedName~Task1"

# Task 2: Validate technology links
dotnet test --filter "FullyQualifiedName~Task2"

# Task 3: Change color theme
dotnet test --filter "FullyQualifiedName~Task3"
```

### Run by Category
```bash
# UI tests only
dotnet test --filter "Category=UI"

# API tests only
dotnet test --filter "Category=API"

# Integration tests (UI + API combined)
dotnet test --filter "Category=Integration"

# Unit tests
dotnet test --filter "Category=Unit"
```

### Run with Detailed Output
```bash
dotnet test --verbosity detailed
```

## 📊 Understanding Results

### Console Output
During test execution, you'll see:
```
✓ Test passed successfully
✗ Test failed (with error details)
→ Test in progress (with step logs)
```

### HTML Report
After tests complete:
```bash
open test-results/TestReport.html
```

The report shows:
- 📊 Test execution summary (Pass/Fail counts)
- ⏱️ Execution time per test
- 📝 Detailed step-by-step logs
- 📸 Screenshots (on failure)
- 💻 System information

### Screenshots
If any test fails, screenshots are saved to:
```
Screenshots/
├── Task1_TestName_20241022_143052.png
├── Task2_TestName_20241022_143125.png
└── Task3_TestName_20241022_143158.png
```

## 📁 Project Structure Quick Reference

```
WikipediaPlaywrightTests/
├── Config/                          # Configuration
│   └── TestConfiguration.cs         # Settings (URLs, timeouts, etc.)
│
├── PageObjects/                     # Page Object Model
│   ├── BasePage.cs                  # Common page operations
│   └── WikipediaPlaywrightPage.cs   # Wikipedia-specific page
│
├── Services/                        # API Services
│   └── MediaWikiApiService.cs       # MediaWiki Parse API
│
├── Utils/                           # Utilities
│   ├── TextNormalizer.cs            # Text processing
│   ├── ExtentReportManager.cs       # Report generation
│   ├── TestDataHelper.cs            # Data comparison
│   └── BrowserHelper.cs             # Browser helpers
│
├── Tests/                           # Test Cases
│   ├── BaseTest.cs                  # Base test class
│   ├── Task1_DebuggingFeaturesTests.cs    # Task 1 tests
│   ├── Task2_MicrosoftDevToolsTests.cs    # Task 2 tests
│   ├── Task3_ColorThemeTests.cs           # Task 3 tests
│   └── UnitTests/                         # Unit tests
│
├── test-results/                    # Generated reports
├── Screenshots/                     # Failure screenshots
│
└── Documentation
    ├── README.md                    # Main documentation
    ├── QUICKSTART.md                # Quick start guide
    ├── ARCHITECTURE.md              # Architecture details
    ├── INSTALLATION.md              # .NET installation
    ├── SUMMARY.md                   # Project summary
    └── GETTING_STARTED.md           # This file
```

## 🎯 What Each Task Does

### Task 1: Extract and Compare Debugging Features
**File:** `Tests/Task1_DebuggingFeaturesTests.cs`

**What it does:**
1. Opens Wikipedia Playwright page
2. Extracts "Debugging features" section using UI (POM)
3. Extracts same section using MediaWiki Parse API
4. Normalizes both texts (removes special chars, lowercase)
5. Counts unique words in each
6. Asserts that counts are equal

**Why it's important:** Tests both UI and API extraction methods and validates data consistency.

### Task 2: Validate Microsoft Development Tools Links
**File:** `Tests/Task2_MicrosoftDevToolsTests.cs`

**What it does:**
1. Navigates to "Microsoft development tools" section
2. Extracts all technology names (e.g., "Visual Studio Code")
3. Checks if each technology name is a clickable link
4. Fails the test if any technology is NOT a link

**Why it's important:** Ensures critical navigation elements are functional.

### Task 3: Change Color Theme to Dark
**File:** `Tests/Task3_ColorThemeTests.cs`

**What it does:**
1. Captures initial theme state
2. Clicks "Color (beta)" button
3. Selects "Dark" mode
4. Verifies theme changed using:
   - CSS class checks
   - Computed style validation
5. Takes before/after screenshots

**Why it's important:** Tests dynamic UI changes and validates visual modifications.

## 🔍 Troubleshooting

### Issue: "dotnet: command not found"
**Solution:** Install .NET SDK - See [INSTALLATION.md](INSTALLATION.md)

### Issue: Build fails with package errors
**Solution:**
```bash
dotnet clean
dotnet restore
dotnet build
```

### Issue: Playwright browsers not installed
**Solution:**
```bash
pwsh bin/Debug/net8.0/playwright.ps1 install --with-deps
```

### Issue: Tests timeout
**Solution:** Increase timeout in `Config/TestConfiguration.cs`:
```csharp
public static int DefaultTimeout => 60000; // 60 seconds
```

### Issue: Cannot open HTML report
**Solution:** 
```bash
# Find the report
ls -la test-results/

# Open manually
open test-results/TestReport.html
```

### Issue: Permission denied on setup.sh
**Solution:**
```bash
chmod +x setup.sh
./setup.sh
```

## 📚 Additional Resources

- **README.md** - Comprehensive project overview
- **ARCHITECTURE.md** - Detailed architecture and design patterns
- **QUICKSTART.md** - Quick reference guide
- **INSTALLATION.md** - .NET SDK installation guide
- **SUMMARY.md** - Project summary and achievements

## ✅ Verification Checklist

Before running tests, verify:

- [ ] .NET SDK is installed (`dotnet --version`)
- [ ] Project builds successfully (`dotnet build`)
- [ ] Playwright browsers are installed
- [ ] Internet connection is active
- [ ] No other browser sessions interfering

## 🎓 Learning Path

**For Reviewers:**
1. Read README.md (5 min)
2. Review ARCHITECTURE.md (10 min)
3. Run tests (`dotnet test`) (2 min)
4. Check HTML report (5 min)
5. Examine code files (20 min)

**For Developers:**
1. Complete setup (10 min)
2. Run tests and explore (15 min)
3. Read code with inline comments (30 min)
4. Modify and extend (ongoing)

## 🚀 Quick Commands Reference

```bash
# Setup
chmod +x setup.sh && ./setup.sh

# Build
dotnet build

# Run all tests
dotnet test

# Run specific task
dotnet test --filter "FullyQualifiedName~Task1"

# Run with verbose output
dotnet test --verbosity detailed

# View report
open test-results/TestReport.html

# Clean and rebuild
dotnet clean && dotnet restore && dotnet build
```

## 💡 Tips for Success

1. **First time?** Run all tests to see the full suite in action
2. **Checking specific task?** Use filter to run individual tests
3. **Test failed?** Check screenshots and HTML report for details
4. **Modifying code?** Follow the patterns in existing files
5. **Need help?** Check inline code comments

## 🎉 Next Steps

Once setup is complete:

1. ✅ Run the full test suite
2. ✅ Review the HTML report
3. ✅ Examine the code structure
4. ✅ Read the architecture documentation
5. ✅ Explore individual test files

## 📧 Support

For questions about:
- **Installation**: See INSTALLATION.md
- **Architecture**: See ARCHITECTURE.md
- **Quick reference**: See QUICKSTART.md
- **Project overview**: See README.md
- **Implementation details**: Check inline code comments

---

**Ready to begin!** Start with installing .NET SDK, then run `./setup.sh` 🚀
