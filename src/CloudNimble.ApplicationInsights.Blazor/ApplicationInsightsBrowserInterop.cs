using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using KristofferStrube.Blazor.Window;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights.Blazor
{

    /// <summary>
    /// 
    /// </summary>
    public class ApplicationInsightsBrowserInterop : IAsyncDisposable
    {

        #region Private Members

        private readonly DotNetObjectReference<ApplicationInsightsBrowserInterop> _dotNetReference;
        private EventListener<ErrorEvent>? _errorEventListener;
        private readonly IJSRuntime _jsRuntime;
        private readonly TelemetryClient _telemetryClient;
        private readonly IWindowService _windowService;

        #endregion

        #region Public Properties

        /// <summary>
        /// A reference to the Browser's Window object.
        /// </summary>
        public Window Window { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the <see cref="ApplicationInsightsBrowserInterop" /> class.
        /// </summary>
        /// <param name="telemetryClient"></param>
        /// <param name="jsRuntime">The <see cref="IJSRuntime" /> instance to use for JS Interop.</param>
        /// <param name="windowService">The <see cref="WindowService" /> used to get a reference to the Browser Window object.</param>
        [DynamicDependency(nameof(TrackEventAsync))]
        public ApplicationInsightsBrowserInterop(TelemetryClient telemetryClient, IJSRuntime jsRuntime, IWindowService windowService)
        {
            _dotNetReference = DotNetObjectReference.Create(this);
            _telemetryClient = telemetryClient;
            _jsRuntime = jsRuntime;
            _windowService = windowService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Properly configures BlazorInsights Blazor for use with JavaScript.
        /// </summary>
        public async Task InitializeAsync()
        {
            // RWM: Register a handler for unhandled JS exceptions.
            Window = await _windowService.GetWindowAsync();
            _errorEventListener = await EventListener<ErrorEvent>.CreateAsync(_jsRuntime, OnUnhandledJsException);
            await Window.AddOnErrorEventListenerAsync(_errorEventListener);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="properties"></param>
        /// <param name="metrics"></param>
        /// <returns></returns>
        [JSInvokable]
        public async Task TrackEventAsync(WebIDLException exception, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            properties ??= new Dictionary<string, string>();
            properties.TryAdd("ExceptionType", "Manual");
            properties.TryAdd("Runtime", "JavaScript");
            await _telemetryClient.TrackExceptionAsync(exception, properties, metrics);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async ValueTask DisposeAsync()
        {
            if (_errorEventListener is not null && Window is not null)
            {
                await Window.RemoveOnErrorEventListenerAsync(_errorEventListener);
                await _errorEventListener.DisposeAsync();
            }
            if (Window is not null)
            {
                await Window.DisposeAsync();
            }
            _dotNetReference?.Dispose();
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Processes unhandled exceptions from JavaScript and reports them to Azure Application Insights.
        /// </summary>
        /// <param name="errorEvent">The <see cref="ErrorEvent" /> passed up from the DOM.</param>
        internal async Task OnUnhandledJsException(ErrorEvent errorEvent)
        {
            var exception = await errorEvent.GetErrorAsExceptionAsync();
            if (exception is null) return;
            await _telemetryClient.TrackExceptionAsync(exception,
                new Dictionary<string, string>
                {
                    { "Runtime", "JavaScript" },
                    { "ExceptionType", "Unhandled" }
                }, null);
        }

        #endregion

    }

}
