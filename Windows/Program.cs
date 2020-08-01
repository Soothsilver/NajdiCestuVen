using System;
using Nsnbc.PostSharp;
using PostSharp.Patterns.Diagnostics;

namespace Windows
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            LoggingServices.ExceptionHandler = new ThrowingLoggingExceptionHandler();
            LoggingServices.DefaultBackend = new MyLoggingBackend();
            using var game = new WindowsGame();
            game.Run();
        }
    }
#endif
}
