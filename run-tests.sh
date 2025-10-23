#!/bin/bash

# Script to run tests and automatically open the Extent Test Report
# Usage: ./run-tests.sh

echo "🧪 Running tests..."
echo ""

# Run dotnet test
dotnet test

# Check if the test run was successful (captures exit code)
TEST_EXIT_CODE=$?

echo ""
echo "=========================================="

if [ $TEST_EXIT_CODE -eq 0 ]; then
    echo "✅ Tests completed successfully!"
else
    echo "⚠️  Tests completed with errors (Exit code: $TEST_EXIT_CODE)"
fi

# Path to the Extent Report
REPORT_PATH="TestResults/ExtentTestReport.html"

# Check if report exists
if [ -f "$REPORT_PATH" ]; then
    echo "📊 Opening Extent Test Report..."
    echo "   Report location: $REPORT_PATH"
    
    # Open the HTML report in default browser (macOS)
    open "$REPORT_PATH"
    
    echo "✅ Report opened in browser!"
else
    echo "❌ Report not found at: $REPORT_PATH"
    echo "   Please ensure tests have run and generated the report."
fi

echo "=========================================="
echo ""

# Exit with the same code as the test run
exit $TEST_EXIT_CODE
