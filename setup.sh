#!/bin/bash

# Setup script for Genpact QA Assignment

echo "=========================================="
echo "Genpact QA Assignment - Setup Script"
echo "=========================================="
echo ""

# Check if .NET is installed
if ! command -v dotnet &> /dev/null
then
    echo "❌ .NET is not installed. Please install .NET 8.0 or higher."
    echo "Visit: https://dotnet.microsoft.com/download"
    exit 1
fi

echo "✓ .NET is installed"
dotnet --version

# Restore NuGet packages
echo ""
echo "📦 Restoring NuGet packages..."
dotnet restore

if [ $? -ne 0 ]; then
    echo "❌ Failed to restore packages"
    exit 1
fi

echo "✓ Packages restored successfully"

# Build the project
echo ""
echo "🔨 Building the project..."
dotnet build

if [ $? -ne 0 ]; then
    echo "❌ Build failed"
    exit 1
fi

echo "✓ Build successful"

# Install Playwright browsers
echo ""
echo "🎭 Installing Playwright browsers..."
pwsh bin/Debug/net8.0/playwright.ps1 install

if [ $? -ne 0 ]; then
    echo "⚠️  Warning: Playwright browser installation may have failed"
    echo "Try running manually: pwsh bin/Debug/net8.0/playwright.ps1 install"
fi

echo ""
echo "=========================================="
echo "✓ Setup completed successfully!"
echo "=========================================="
echo ""
echo "To run tests:"
echo "  dotnet test"
echo ""
echo "To run specific test categories:"
echo "  dotnet test --filter Category=UI"
echo "  dotnet test --filter Category=API"
echo "  dotnet test --filter Category=Integration"
echo ""
echo "To view HTML report after tests:"
echo "  open test-results/TestReport.html"
echo ""
