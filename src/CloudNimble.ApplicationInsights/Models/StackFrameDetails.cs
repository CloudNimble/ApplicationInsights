namespace CloudNimble.ApplicationInsights.Models
{

    /// <summary>
    /// Details about a particular stack frame in an Exception call stack
    /// </summary>
    public class StackFrameDetails
    {

        /// <summary>
        /// Name of the assembly (dll, jar, wasm, etc.) containing this function.
        /// </summary>
        public string Assembly { get; set; }

        /// <summary>
        /// File name or URL of the method implementation.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Level in the call stack. For the long stacks SDK may not report every function in a call stack.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Line number of the code implementation.
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// Method name.
        /// </summary>
        public string Method { get; set; }

    }

}
