# ApplicationInsights
A clean, simple Azure Application Insights client for .NET 6.0 and later.

## Purpose
This client was created specifically to give Blazor developers a first-class experience instrumenting their
applications with Azure Application Insights, whether using C# or JSInterop. 

The challenge is that the Application Insights SDK is simply not compatible with Blazor's Mono + Enscripten
runtime. So we decided to make a clean, modern, simple .NET 8.0 client that any .NET developer can use.

## Why ApplicationInsights and not OpenTelemetry?
Microsoft's own OpenTelemetry documentation says there is no ETA for a JavaScript SDK. This is a problem for Blazor
developers, and the common workaround has been to use the Application Insights JavaScript SDK in Blazor
apps to report telemetry data. 

We believe this is sub-optimal and that Blazor developers deserve a first-class experience.