using CloudNimble.ApplicationInsights.Blazor.TelemetryPipeline;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights.Blazor.Controls
{

    /// <summary>
    /// An extension of the Blazor <see cref="ErrorBoundary" /> control that automatically sends exceptions to 
    /// Azure Application Insights.
    /// </summary>
    /// <remarks>
    /// This control will automatically call <see cref="BrowserTelemetryInterop.InitializeAsync" /> in the
    /// <see cref="OnAfterRenderAsync(bool)" /> method.
    /// </remarks>
    public class ApplicationInsightsErrorBoundary : ErrorBoundary
    {

        #region Public Parameters

        /// <summary>
        /// The <see cref="TelemetryClient" /> instance to use to send exceptions to Azure Application Insights.
        /// </summary>
        [Inject]
        public TelemetryClient TelemetryClient { get; set; }

        /// <summary>
        /// The <see cref="BlazorTelemetryOptions" /> instance that holds the developer's configuration for Azure Application
        /// Insights.
        /// </summary>
        [Inject]
        public BlazorTelemetryOptions TelemetryOptions { get; set; }

        /// <summary>
        /// The <see cref="TelemetryClientInterop" /> instance that handles dealing with JSInterop.
        /// </summary>
        [Inject]
        public TelemetryClientInterop TelemetryClientInterop { get; set; }

        /// <summary>
        /// Specifies whether or not to display the built-in error UI when an exception occurs.
        /// </summary>
        [Parameter]
        public bool ShowExceptionUI { get; set; } = false;

        #endregion

        #region Base Class Overrides

        /// <summary>
        /// Overrides the default implementation to send incoming exceptions to Azure Application Insights.
        /// </summary>
        /// <param name="exception">The <see cref="Exception" /> to send to Azure Application Insights.</param>
        /// <returns></returns>
        protected override async Task OnErrorAsync(Exception exception)
        {
            if (!TelemetryOptions.TrackUnhandledExceptions) return;

            await TelemetryClient.TrackExceptionAsync(exception,
                TelemetryOptions.DefaultUnhandledExceptionSeverityLevel,
                new Dictionary<string, string>
                {
                    { "blazor.runtime", "DotNet" },
                    { "blazor.exceptionType", "Unhandled" }
                },
                null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await TelemetryClientInterop.InitializeAsync();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        /// <inheritdoc />
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));
            if (CurrentException is not null && ShowExceptionUI)
            {
                if (ErrorContent is not null)
                {
                     builder.AddContent(1, ErrorContent(CurrentException));
                }
                else
                {
                    // The default error UI doesn't include any content, because:
                    // [1] We don't know whether or not you'd be happy to show the stack trace. It depends both on
                    //     whether DetailedErrors is enabled and whether you're in production, because even on WebAssembly
                    //     you likely don't want to put technical data like that in the UI for end users. A reasonable way
                    //     to toggle this is via something like "#if DEBUG" but that can only be done in user code.
                    // [2] We can't have any other human-readable content by default, because it would need to be valid
                    //     for all languages.
                    // Instead, the default project template provides locale-specific default content via CSS. This provides
                    // a quick form of customization even without having to subclass this component.
                    builder.OpenElement(2, "div");
                    builder.AddAttribute(3, "class", "blazor-error-boundary");
                    builder.CloseElement();
                }
            }
            else
            {
                builder.AddContent(0, ChildContent);
            }
        }

        #endregion

    }

}
