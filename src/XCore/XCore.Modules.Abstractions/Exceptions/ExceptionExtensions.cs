using System;
using System.Security;
using System.Runtime.InteropServices;

namespace XCore.Modules
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// fatal 致命的; 严重的;  重大的;
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static bool IsFatal(this Exception ex)
        {
            return 
                ex is OutOfMemoryException ||
                ex is SecurityException ||
                ex is SEHException;
        }
    }
}