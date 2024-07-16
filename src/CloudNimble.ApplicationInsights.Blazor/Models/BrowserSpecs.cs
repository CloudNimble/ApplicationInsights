namespace CloudNimble.ApplicationInsights.Blazor.Models
{

    /// <summary>
    /// Attributes about the Browser that are <i>highly unlikely</i> to change at runtime.
    /// </summary>
    internal record BrowserSpecs
    {

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public bool AreCookiesEnabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ColorDepth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DeviceMemoryInGB { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasMultipleMonitors { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PixelDepth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProcessorCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ScreenHeight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ScreenWidth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BrowserUserAgentData UserAgentData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int UtcOffset { get; set; }

        #endregion

    }

}
