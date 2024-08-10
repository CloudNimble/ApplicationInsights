using CloudNimble.ApplicationInsights.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CloudNimble.ApplicationInsights.Blazor.TelemetryPipeline
{

    /// <summary>
    /// Captures browser-specific metrics from the <see cref="BrowserTelemetryInterop" /> instance injected into the
    /// app.
    /// </summary>
    /// <remarks>
    /// https://github.com/microsoft/ApplicationInsights-JS/blob/main/shared/AppInsightsCommon/src/Interfaces/Contracts/ContextTagKeys.ts
    /// </remarks>
    internal class BrowserTelemetryCapture : ITelemetryCapture
    {

        #region Private Members

        private readonly BrowserTelemetryInterop _interop;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the <see cref="BrowserTelemetryCapture" /> class.
        /// </summary>
        /// <param name="interop">
        /// The <see cref="BrowserTelemetryInterop" /> instance from DI to capture browser metrics with.
        /// </param>
        public BrowserTelemetryCapture(BrowserTelemetryInterop interop)
        {
            _interop = interop;
        }

        #endregion

        public async Task CaptureAsync<T>(RequestDetails<T> details) where T : InsightsBase
        {
            await _interop.UpdateBrowserStats();
            var properties = details.Data.BaseData.Properties;
            properties.Add("blazor.browser.app.width", _interop.LatestBrowserStats.AppWidth.ToString());
            properties.Add("blazor.browser.app.height", _interop.LatestBrowserStats.AppHeight.ToString());

            foreach (var version in _interop.BrowserSpecs.UserAgentData.BrandVersions
                .Where(c => !c.Key.StartsWith("not", StringComparison.InvariantCultureIgnoreCase)))
            {
                properties.Add($"blazor.browser.version.brand.{version.Key}", version.Value);
            }

            foreach (var version in _interop.BrowserSpecs.UserAgentData.ComponentVersions
                .Where(c => !c.Key.StartsWith("not", StringComparison.InvariantCultureIgnoreCase)))
            {
                properties.Add($"blazor.browser.version.component.{version.Key}", version.Value);
            }

            properties.Add("blazor.browser.os.locale", _interop.BrowserSpecs.Locale);
            properties.Add("blazor.browser.os.platform", _interop.BrowserSpecs.UserAgentData.CalculatedPlatform);
            properties.Add("blazor.browser.os.version", _interop.BrowserSpecs.UserAgentData.CalculatedOSVersion);
            properties.Add("blazor.browser.os.utcOffset", _interop.BrowserSpecs.UtcOffset.ToString());

            properties.Add("blazor.browser.cookies.enabled", _interop.BrowserSpecs.AreCookiesEnabled.ToString());

            if (!string.IsNullOrWhiteSpace(_interop.BrowserSpecs.UserAgentData.FormFactor))
            {
                properties.Add("blazor.browser.device.formFactor", _interop.BrowserSpecs.UserAgentData.FormFactor);
            }
            properties.Add("blazor.browser.device.ram", _interop.BrowserSpecs.DeviceMemoryInGB.ToString());

            if (!string.IsNullOrWhiteSpace(_interop.BrowserSpecs.UserAgentData.Model))
            {
                properties.Add("blazor.browser.device.model", _interop.BrowserSpecs.UserAgentData.Model);
            }
            properties.Add("blazor.browser.device.processors", _interop.BrowserSpecs.ProcessorCount.ToString());
            properties.Add("blazor.browser.device.isMobile", _interop.BrowserSpecs.UserAgentData.IsMobile.ToString());

            //RWM: These should probably be metrics
            properties.Add("blazor.browser.memory.current", _interop.LatestBrowserStats.MemoryCurrentSizeInBytes.ToString());
            properties.Add("blazor.browser.memory.max", _interop.LatestBrowserStats.MemoryMaxSizeInBytes.ToString());
            properties.Add("blazor.browser.memory.used", _interop.LatestBrowserStats.MemoryUsedSizeInBytes.ToString());

            properties.Add("blazor.browser.screen.colorDepth", _interop.BrowserSpecs.ColorDepth.ToString());
            properties.Add("blazor.browser.screen.height", _interop.BrowserSpecs.ScreenHeight.ToString());
            properties.Add("blazor.browser.screen.multipleMonitors", _interop.BrowserSpecs.HasMultipleMonitors.ToString());
            properties.Add("blazor.browser.screen.pixelRatio", _interop.LatestBrowserStats.DevicePixelRatio.ToString());
            properties.Add("blazor.browser.screen.width", _interop.BrowserSpecs.ScreenWidth.ToString());

            //RWM: These should probably be metrics
            properties.Add("blazor.browser.storage.max", _interop.LatestBrowserStats.StorageQuotaInBytes.ToString());
            properties.Add("blazor.browser.storage.used", _interop.LatestBrowserStats.StorageUsageInBytes.ToString());
            properties.Add("blazor.browser.storage.remaining", _interop.LatestBrowserStats.CalculatedStorageRemainingInBytes.ToString());

            //RWM: Built-in tags support.
            var browserVersion = _interop.BrowserSpecs.UserAgentData.ComponentVersions
                .Where(c => !c.Key.StartsWith("not", StringComparison.InvariantCultureIgnoreCase) && c.Key != "Chromium")
                .FirstOrDefault();

            details.Tags.TryAdd("ai.device.browserVersion", $"{browserVersion.Key} {browserVersion.Value}");

            details.Tags.TryAdd("ai.device.locale", _interop.BrowserSpecs.Locale);
            if (!string.IsNullOrWhiteSpace(_interop.BrowserSpecs.UserAgentData.Model))
            {
                details.Tags.TryAdd("ai.device.model", _interop.BrowserSpecs.UserAgentData.Model);
            }
            details.Tags.TryAdd("ai.device.osVersion", _interop.BrowserSpecs.UserAgentData.CalculatedOSVersion);
            details.Tags.TryAdd("ai.device.type", Enum.GetName(DeviceType.Browser));

        }

    }

}
