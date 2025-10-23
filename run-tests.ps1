# PowerShell script to run tests and automatically open the Extent Test Report
# Usage: .\run-tests.ps1

Write-Host "🧪 Running tests..." -ForegroundColor Cyan
Write-Host ""

# Run dotnet test and capture exit code
dotnet test
$TestExitCode = $LASTEXITCODE

Write-Host ""
Write-Host "==========================================" -ForegroundColor Gray

if ($TestExitCode -eq 0) {
    Write-Host "✅ Tests completed successfully!" -ForegroundColor Green
} else {
    Write-Host "⚠️  Tests completed with errors (Exit code: $TestExitCode)" -ForegroundColor Yellow
}

# Path to the Extent Report
$ReportPath = "TestResults/ExtentTestReport.html"

# Check if report exists
if (Test-Path $ReportPath) {
    Write-Host "📊 Opening Extent Test Report..." -ForegroundColor Cyan
    Write-Host "   Report location: $ReportPath" -ForegroundColor Gray
    
    # Open the HTML report in default browser
    if ($IsMacOS) {
        open $ReportPath
    } elseif ($IsWindows) {
        Start-Process $ReportPath
    } elseif ($IsLinux) {
        xdg-open $ReportPath
    } else {
        # Fallback for older PowerShell versions (Windows)
        Start-Process $ReportPath
    }
    
    Write-Host "✅ Report opened in browser!" -ForegroundColor Green
} else {
    Write-Host "❌ Report not found at: $ReportPath" -ForegroundColor Red
    Write-Host "   Please ensure tests have run and generated the report." -ForegroundColor Yellow
}

Write-Host "==========================================" -ForegroundColor Gray
Write-Host ""

# Exit with the same code as the test run
exit $TestExitCode
