# Setup script for Genpact QA Assignment (Windows)

Write-Host "==========================================" -ForegroundColor Cyan
Write-Host "Genpact QA Assignment - Setup Script" -ForegroundColor Cyan
Write-Host "==========================================" -ForegroundColor Cyan
Write-Host ""

# Check if .NET is installed
try {
    $dotnetVersion = dotnet --version
    Write-Host "✓ .NET is installed: $dotnetVersion" -ForegroundColor Green
}
catch {
    Write-Host "❌ .NET is not installed. Please install .NET 8.0 or higher." -ForegroundColor Red
    Write-Host "Visit: https://dotnet.microsoft.com/download" -ForegroundColor Yellow
    exit 1
}

# Restore NuGet packages
Write-Host ""
Write-Host "📦 Restoring NuGet packages..." -ForegroundColor Yellow
dotnet restore

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Failed to restore packages" -ForegroundColor Red
    exit 1
}

Write-Host "✓ Packages restored successfully" -ForegroundColor Green

# Build the project
Write-Host ""
Write-Host "🔨 Building the project..." -ForegroundColor Yellow
dotnet build

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Build failed" -ForegroundColor Red
    exit 1
}

Write-Host "✓ Build successful" -ForegroundColor Green

# Install Playwright browsers
Write-Host ""
Write-Host "🎭 Installing Playwright browsers..." -ForegroundColor Yellow
pwsh bin/Debug/net8.0/playwright.ps1 install

if ($LASTEXITCODE -ne 0) {
    Write-Host "⚠️  Warning: Playwright browser installation may have failed" -ForegroundColor Yellow
    Write-Host "Try running manually: pwsh bin/Debug/net8.0/playwright.ps1 install" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "==========================================" -ForegroundColor Cyan
Write-Host "✓ Setup completed successfully!" -ForegroundColor Green
Write-Host "==========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "To run tests:" -ForegroundColor Yellow
Write-Host "  dotnet test" -ForegroundColor White
Write-Host ""
Write-Host "To run specific test categories:" -ForegroundColor Yellow
Write-Host "  dotnet test --filter Category=UI" -ForegroundColor White
Write-Host "  dotnet test --filter Category=API" -ForegroundColor White
Write-Host "  dotnet test --filter Category=Integration" -ForegroundColor White
Write-Host ""
Write-Host "To view HTML report after tests:" -ForegroundColor Yellow
Write-Host "  start test-results/TestReport.html" -ForegroundColor White
Write-Host ""
