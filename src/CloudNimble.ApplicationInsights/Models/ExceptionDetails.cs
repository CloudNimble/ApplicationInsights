using System;

namespace CloudNimble.ApplicationInsights.Models
{

    /// <summary>
    /// 
    /// </summary>
    public class ExceptionDetails
    {

        /// <summary>
        /// 
        /// </summary>
        public bool HasFullStack { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Stack { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public ExceptionDetails(Exception exception)
        {
            Message = exception.Message;
            Stack = exception.StackTrace;
            TypeName = exception.GetType().FullName;
            HasFullStack = true;
        }

    }

}
