[![GitHub Workflow Status (main)](https://img.shields.io/github/actions/workflow/status/microsoft/ApplicationInsights-JS/ci.yml?branch=main)](https://github.com/microsoft/ApplicationInsights-JS/tree/main)
[![Build Status](https://dev.azure.com/mseng/AppInsights/_apis/build/status%2FAppInsights%20-%20DevTools%2F1DS%20JavaScript%20SDK%20web%20SKU%20(main%3B%20master)?branchName=main)](https://dev.azure.com/mseng/AppInsights/_build/latest?definitionId=8184&branchName=main)

# CloudNimble.ApplicationInsights
A modern .NET Client for working with Azure Application Insights.

## Purpose
Microsoft has two options for working with Application Insights in .NET:
- The legacy ApplicationInsights SDK, which is outdated and not designed for modern .NET
- OpenTelementry, which is fairly well-designed but lacks features of the legacy SDK... not to mention that the data points it stores will someday cost extra.

For those people that want the complete support of the legacy SDK but with a clean, efficient design, this library is for you.