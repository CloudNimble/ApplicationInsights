[![GitHub Workflow Status (main)](https://img.shields.io/github/actions/workflow/status/microsoft/ApplicationInsights-JS/ci.yml?branch=main)](https://github.com/microsoft/ApplicationInsights-JS/tree/main)
[![Build Status](https://dev.azure.com/mseng/AppInsights/_apis/build/status%2FAppInsights%20-%20DevTools%2F1DS%20JavaScript%20SDK%20web%20SKU%20(main%3B%20master)?branchName=main)](https://dev.azure.com/mseng/AppInsights/_build/latest?definitionId=8184&branchName=main)

# CloudNimble.ApplicationInsights.Blazor.WebAssembly
A native Blazor WebAssembly Client for working with Azure Application Insights.

## Purpose
This package makes registering the proper services with Dependency Injection easier.
Since Blazor WebAssembly is a client-side technology executed inside a single browser
instance, the services should be registered as Singletons and not Scoped.

### Current Feature Support
- [x] Exception Reporting
- [x] Unhandled Exception Reporting
- [x] Logging
- [x] Telemetry
- [x] Metrics
- [ ] Pageview Performance
- [ ] Offline Mode


