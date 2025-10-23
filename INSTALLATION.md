# Installation Guide - .NET SDK

## ⚠️ .NET SDK Not Found

The .NET SDK is required to build and run this project. Follow the steps below to install it.

## Installation Steps

### Option 1: Using Homebrew (Recommended for macOS)

```bash
# Install Homebrew if not already installed
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"

# Install .NET SDK
brew install --cask dotnet-sdk

# Verify installation
dotnet --version
```

### Option 2: Direct Download from Microsoft

1. Visit: https://dotnet.microsoft.com/download
2. Download .NET 8.0 SDK for macOS
3. Run the installer package (.pkg file)
4. Follow the installation wizard
5. Restart your terminal

### Option 3: Using Official Installer Script

```bash
# Download and run the installation script
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 8.0

# Add to PATH (add these lines to ~/.zshrc)
export DOTNET_ROOT=$HOME/.dotnet
export PATH=$PATH:$DOTNET_ROOT:$DOTNET_ROOT/tools

# Reload shell configuration
source ~/.zshrc

# Verify installation
dotnet --version
```

## After Installation

Once .NET is installed, run the setup:

```bash
# Make setup script executable
chmod +x setup.sh

# Run setup
./setup.sh
```

Or manually:

```bash
# Restore packages
dotnet restore

# Build project
dotnet build

# Install Playwright browsers
pwsh bin/Debug/net8.0/playwright.ps1 install

# Run tests
dotnet test
```

## Verify Installation

Check if .NET is properly installed:

```bash
# Check .NET version
dotnet --version

# Should show: 8.0.x or higher

# List installed SDKs
dotnet --list-sdks

# List installed runtimes
dotnet --list-runtimes
```

## Troubleshooting

### Command not found: dotnet

If you get "command not found" after installation:

1. **Restart your terminal** completely (close and reopen)
2. **Check PATH**: Run `echo $PATH` and verify .NET is included
3. **Manual PATH setup**: Add to `~/.zshrc`:
   ```bash
   export PATH="$PATH:/usr/local/share/dotnet"
   ```
4. **Reload shell**: `source ~/.zshrc`

### Permission Denied

If you get permission errors:

```bash
sudo chown -R $(whoami) /usr/local/share/dotnet
```

### Multiple .NET Versions

To check and manage multiple versions:

```bash
# List all SDKs
dotnet --list-sdks

# Use specific version in global.json (already configured)
cat global.json
```

## System Requirements

- **OS**: macOS 11.0 (Big Sur) or higher
- **Architecture**: x64 or ARM64 (Apple Silicon)
- **RAM**: Minimum 4 GB
- **Disk Space**: ~1 GB for SDK + browsers

## Next Steps

After successful .NET installation:

1. ✅ Run `dotnet --version` to verify
2. ✅ Navigate to project directory
3. ✅ Run `./setup.sh` or manual setup commands
4. ✅ Run tests with `dotnet test`
5. ✅ View report at `test-results/TestReport.html`

## Alternative: Use Docker

If you prefer not to install .NET locally, you can use Docker:

```bash
# Pull .NET SDK image
docker pull mcr.microsoft.com/dotnet/sdk:8.0

# Run tests in container
docker run --rm -v $(pwd):/app -w /app mcr.microsoft.com/dotnet/sdk:8.0 bash -c "dotnet restore && dotnet build && dotnet test"
```

## Support

For .NET installation issues, refer to:
- Official Documentation: https://learn.microsoft.com/en-us/dotnet/core/install/macos
- .NET Downloads: https://dotnet.microsoft.com/download
- GitHub Issues: https://github.com/dotnet/core/issues
