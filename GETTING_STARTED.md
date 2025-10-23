````markdown
# Getting Started - Complete Setup Guide

## 🎯 Project Overview

This is a **complete test automation framework** for the Genpact QA Home Assignment. It contains:
- ✅ All 3 tasks fully implemented
- ✅ 15 automated tests with clean architecture
- ✅ UI and API testing capabilities
- ✅ Automated test execution scripts
- ✅ HTML report generation with auto-open
- ✅ Screenshot capture on failures

## 📋 Prerequisites

Before you begin, ensure you have:

- **macOS** 11.0 or higher
- **.NET 8.0 SDK** (installation instructions below)
- **Internet connection** (to download browsers and run tests)

## 🚀 Quick Start (3 Steps)

### Step 1: Install .NET SDK (5 minutes)

**Using Homebrew (Recommended):**
```bash
# Install .NET SDK
brew install --cask dotnet-sdk

# Verify installation
dotnet --version
# Should show: 8.0.x
```

**Alternative - Direct Download:**
1. Visit: https://dotnet.microsoft.com/download
2. Download .NET 8.0 SDK for macOS
3. Run the installer (.pkg file)
4. Restart your terminal

**Troubleshooting:**
If `dotnet` command not found after install:
```bash
# Restart terminal completely, or add to PATH:
export PATH="$PATH:/usr/local/share/dotnet"
source ~/.zshrc
```

### Step 2: Run Setup Script (2 minutes)

```bash
# Navigate to project directory
cd "/Users/roma/QA- Home_Assignment_Genpact"

# Make setup script executable
chmod +x setup.sh

# Run automated setup
./setup.sh
```

This will automatically:
1. ✅ Restore NuGet packages
2. ✅ Build the project
3. ✅ Install Playwright browsers (Chromium, Firefox, WebKit)

### Step 3: Run Tests with Auto-Report (2 minutes)

**🎯 RECOMMENDED: Use automated test script**

```bash
# Run tests and automatically open HTML report
./run-tests.sh
```

This script will:
1. ✅ Execute all tests
2. ✅ Display colored results in terminal
3. ✅ **Automatically open ExtentTestReport.html in your browser**
4. ✅ Show you pass/fail summary

**That's it!** Your tests will run and the report will open automatically! 🎉

---

## 📊 Running Tests - Multiple Options

### Option 1: Automated Script with Report (RECOMMENDED) ✨

```bash
# Run all tests + auto-open report
./run-tests.sh
```

**Output:**
```
🧪 Running tests...
Test run for WikipediaPlaywrightTests.dll
✅ Task1_CompareDebuggingFeaturesSectionFromUIAndAPI - PASSED
✅ Task2_ValidateAllTechnologyNamesAreLinks - PASSED  
✅ Task3_ChangeColorToDarkAndValidate - PASSED

==========================================
✅ Tests completed successfully!
📊 Opening Extent Test Report...
   Report location: TestResults/ExtentTestReport.html
✅ Report opened in browser!
==========================================
```

### Option 2: PowerShell Script (Cross-platform)

```bash
# For Windows/macOS/Linux
./run-tests.ps1
```

### Option 3: Manual (Traditional)

```bash
# Run tests
dotnet test

# Then manually open report
open TestResults/ExtentTestReport.html
```

### Option 4: Run Specific Tests

```bash
# Task 1 only - Extract and compare sections
dotnet test --filter "FullyQualifiedName~Task1"
open TestResults/ExtentTestReport.html

# Task 2 only - Validate technology links  
dotnet test --filter "FullyQualifiedName~Task2"
open TestResults/ExtentTestReport.html

# Task 3 only - Change color theme
dotnet test --filter "FullyQualifiedName~Task3"
open TestResults/ExtentTestReport.html
```

## � Understanding Test Results

### Console Output
During test execution, you'll see:
```
✓ Test passed successfully
✗ Test failed (with error details)
→ Test in progress (with step logs)
```

### HTML Report (Auto-Opens!)
When using `./run-tests.sh`, the report automatically opens in your browser.

The **ExtentTestReport.html** includes:
- 📊 Test execution summary (Pass/Fail counts)
- ⏱️ Execution time per test
- 📝 Detailed step-by-step logs
- 📸 Screenshots (on failure)
- 🎨 Dark theme for better readability
- 💻 System information

**Manual access:**
```bash
open TestResults/ExtentTestReport.html
```

### Screenshots
If any test fails, screenshots are automatically saved to:
```
Screenshots/
├── Task1_TestName_20241023_143052.png
├── Task2_TestName_20241023_143125.png
└── Task3_TestName_20241023_143158.png
```

---

## �🔧 Manual Setup (Alternative)

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

# 5. Run tests with auto-report
./run-tests.sh
```

---

## 🎯 Test Script Features

Both `run-tests.sh` and `run-tests.ps1` provide:

✅ **Colored terminal output** - Better readability  
✅ **Test execution status** - Real-time feedback  
✅ **Automatic report opening** - No manual steps  
✅ **Error handling** - Checks if report exists  
✅ **Exit codes** - Matches test results for CI/CD  

**Script output example:**
```
🧪 Running tests...

Test run for WikipediaPlaywrightTests.dll (.NETCoreApp,Version=v8.0)
Microsoft (R) Test Execution Command Line Tool Version 17.8.0

Passed!  - Failed:     0, Passed:     5, Skipped:     0, Total:     5

==========================================
✅ Tests completed successfully!
📊 Opening Extent Test Report...
   Report location: TestResults/ExtentTestReport.html
✅ Report opened in browser!
==========================================
```

---

## 🔍 Advanced: Run by Category

```bash
# UI tests only
dotnet test --filter "Category=UI"

# API tests only
dotnet test --filter "Category=API"

# Integration tests (UI + API combined)
dotnet test --filter "Category=Integration"
```

**Note:** After running filtered tests, manually open report:
```bash
open TestResults/ExtentTestReport.html
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

## 🎯 What Each Task Tests

### ✅ Task 1: Extract and Compare Debugging Features
**File:** `Tests/Task1_DebuggingFeaturesTests.cs`

**What it does:**
1. Opens Wikipedia Playwright page
2. Extracts "Debugging features" section via UI (POM)
3. Extracts same section via MediaWiki Parse API
4. Normalizes both texts
5. Counts unique words
6. Asserts equality

**Run it:**
```bash
dotnet test --filter "FullyQualifiedName~Task1"
open TestResults/ExtentTestReport.html
```

---

### ✅ Task 2: Validate Microsoft Development Tools Links
**File:** `Tests/Task2_MicrosoftDevToolsTests.cs`

**What it does:**
1. Navigates to "Microsoft development tools" section
2. Extracts all technology names
3. Validates each technology is a clickable link
4. Fails if any technology is NOT a link

**Run it:**
```bash
dotnet test --filter "FullyQualifiedName~Task2"
open TestResults/ExtentTestReport.html
```

---

### ✅ Task 3: Change Color Theme to Dark
**File:** `Tests/Task3_ColorThemeTests.cs`

**What it does:**
1. Captures initial theme state
2. Clicks "Color (beta)" button
3. Selects "Dark" mode
4. Verifies theme changed (CSS + computed styles)
5. Takes before/after screenshots

**Run it:**
```bash
dotnet test --filter "FullyQualifiedName~Task3"
open TestResults/ExtentTestReport.html
```

## 🔍 Troubleshooting

### "dotnet: command not found"
**Solution:** Install .NET SDK
```bash
brew install --cask dotnet-sdk
# Restart terminal
dotnet --version
```

### "Permission denied" on scripts
**Solution:**
```bash
chmod +x setup.sh
chmod +x run-tests.sh
```

### Build fails with package errors
**Solution:**
```bash
dotnet clean
dotnet restore
dotnet build
```

### Playwright browsers not installed
**Solution:**
```bash
pwsh bin/Debug/net8.0/playwright.ps1 install --with-deps
```

### Report doesn't auto-open
**Solution:** Open manually
```bash
open TestResults/ExtentTestReport.html
```

### Tests timeout
**Solution:** Increase timeout in `Config/TestConfiguration.cs`:
```csharp
public static int DefaultTimeout => 60000; // 60 seconds
```

---

## � Quick Reference Commands

```bash
# 1️⃣ SETUP (One-time)
brew install --cask dotnet-sdk    # Install .NET
chmod +x setup.sh                 # Make script executable
./setup.sh                        # Run setup

# 2️⃣ RUN TESTS (Recommended)
./run-tests.sh                    # Run tests + auto-open report

# 3️⃣ ALTERNATIVE METHODS
./run-tests.ps1                   # PowerShell version
dotnet test                       # Traditional way

# 4️⃣ SPECIFIC TESTS
dotnet test --filter "Task1"      # Task 1 only
dotnet test --filter "Task2"      # Task 2 only
dotnet test --filter "Task3"      # Task 3 only

# 5️⃣ VIEW REPORT MANUALLY
open TestResults/ExtentTestReport.html

# 6️⃣ TROUBLESHOOTING
dotnet clean && dotnet restore && dotnet build
chmod +x run-tests.sh
```

---

## ✅ Success Checklist

Before running tests, verify:

- [x] .NET SDK installed (`dotnet --version`)
- [x] Setup script executed (`./setup.sh`)
- [x] Project builds (`dotnet build`)
- [x] Scripts are executable (`chmod +x`)
- [x] Internet connection active

---

## 📖 Additional Documentation

| Document | Purpose | Time |
|----------|---------|------|
| **README.md** | Project overview | 10 min |
| **ARCHITECTURE.md** | Design & patterns | 10 min |
| **PROJECT_STRUCTURE.md** | File organization | 5 min |
| **INDEX.md** | All documentation | 2 min |

---

## � Next Steps

1. ✅ Run `./run-tests.sh`
2. ✅ Review ExtentTestReport.html (auto-opens!)
3. ✅ Check test code in `Tests/` folder
4. ✅ Read README.md for full details

---

## � Pro Tips

🔹 **First time?** Use `./run-tests.sh` - it does everything automatically  
🔹 **Report doesn't open?** Check `TestResults/ExtentTestReport.html` manually  
🔹 **Test failed?** Check Screenshots/ folder for visual evidence  
🔹 **Want details?** Review inline code comments in test files  

---

## 🎉 You're Ready!

**Total setup time: ~10 minutes**

1. Install .NET (5 min)
2. Run setup (2 min)
3. Run tests (2 min)
4. View report (1 min)

**Start with:** `./run-tests.sh` 🚀
