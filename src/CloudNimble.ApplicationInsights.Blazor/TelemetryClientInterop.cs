using CloudNimble.ApplicationInsights.Blazor.TelemetryPipeline;
using CloudNimble.ApplicationInsights.Models;
using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using KristofferStrube.Blazor.Window;
using Microsoft.AspNetCore.Components;
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
    public class TelemetryClientInterop : IAsyncDisposable
    {

        #region Private Members

        private readonly DotNetObjectReference<TelemetryClientInterop> _dotNetReference;
        private EventListener<ErrorEvent>? _errorEventListener;
        private readonly IJSRuntime _jsRuntime;
        private readonly BrowserTelemetryInterop _metricsInterop;
        private readonly NavigationManager _navigationManager;
        private readonly TelemetryClient _telemetryClient = null;
        private readonly BlazorTelemetryOptions _telemetryOptions;
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
        /// Creates a new instance of the <see cref="TelemetryClientInterop" /> class.
        /// </summary>
        /// <param name="metricsInterop"></param>
        /// <param name="telemetryClient"></param>
        /// <param name="telemetryOptions"></param>
        /// <param name="jsRuntime">The <see cref="IJSRuntime" /> instance to use for JS Interop.</param>
        /// <param name="navigationManager"></param>
        /// <param name="windowService">The <see cref="WindowService" /> used to get a reference to the Browser Window object.</param>
        [DynamicDependency(nameof(TrackExceptionAsync))]
        public TelemetryClientInterop(BrowserTelemetryInterop metricsInterop, TelemetryClient telemetryClient, BlazorTelemetryOptions telemetryOptions, IJSRuntime jsRuntime, NavigationManager navigationManager,
            IWindowService windowService)
        {
            _dotNetReference = DotNetObjectReference.Create(this);
            _metricsInterop = metricsInterop;
            _telemetryClient = telemetryClient;
            _telemetryOptions = telemetryOptions;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _windowService = windowService;

            //// TODO: RWM: Get the disposable here and dispose of it.
            //_navigationManager.RegisterLocationChangingHandler(context =>
            //{
            //    _telemetryClient.StartTrackPage(context.TargetLocation);
            //    return ValueTask.CompletedTask;
            //});
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Properly configures BlazorInsights Blazor for use with JavaScript.
        /// </summary>
        public async Task InitializeAsync()
        {
            if (_metricsInterop.BrowserSpecs is not null) return;

            //RWM: Do this first because we can't initialize something that isn't loaded yet.
            await _metricsInterop.InitializeAsync();

            // RWM: Register the .NET reference with JS so that JS code can also manually create Bookmarks and report Exceptions.
            await _jsRuntime.InvokeVoidAsync("window.applicationInsightsBlazor.initialize", _dotNetReference);

            // RWM: Honor the developer's settings.
            if (!_telemetryOptions.TrackUnhandledJSExceptions) return;

            // RWM: Register a handler for unhandled JS exceptions.
            Window = await _windowService.GetWindowAsync();
            _errorEventListener = await EventListener<ErrorEvent>.CreateAsync(_jsRuntime, OnUnhandledJsException);
            await Window.AddOnErrorEventListenerAsync(_errorEventListener);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="severityLevel"></param>
        /// <param name="properties"></param>
        /// <param name="metrics"></param>
        /// <returns></returns>
        [JSInvokable]
        public async Task TrackExceptionAsync(WebIDLException exception, SeverityLevel severityLevel = SeverityLevel.Error, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            properties ??= new Dictionary<string, string>();
            properties.TryAdd("blazor.runtime", "JavaScript");
            properties.TryAdd("blazor.exceptionType", "Manual");
            await _telemetryClient.TrackExceptionAsync(exception, severityLevel, properties, metrics);
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
                _telemetryOptions.DefaultUnhandledExceptionSeverityLevel,
                new Dictionary<string, string>
                {
                    { "blazor.runtime", "JavaScript" },
                    { "blazor.exceptionType", "Unhandled" }
                },
                null);
        }

        #endregion

    }

}
