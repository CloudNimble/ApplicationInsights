using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights.Blazor.Webassembly.Controls
{

    /// <summary>
    /// An extension of the Blazor <see cref="ErrorBoundary" /> control that automatically sends exceptions to 
    /// Azure Application Insights.
    /// </summary>
    public class ApplicationInsightsErrorBoundary : ErrorBoundary
    {

        #region Public Parameters

        /// <summary>
        /// The instance of the <see cref="TelemetryClient" /> to use to send exceptions to Azure Application Insights.
        /// </summary>
        [Inject]
        public TelemetryClient TelemetryClient { get; set; }

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
            await TelemetryClient.TrackExceptionAsync(exception, 
                new Dictionary<string, string>
                {
                    { "Runtime", "DotNet" },
                    { "ExceptionType", "Unhandled" }
                }, null);
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
