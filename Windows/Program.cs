using System;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.Trace;

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
            LoggingServices.DefaultBackend = new TraceLoggingBackend();
            using var game = new WindowsGame();
            game.Run();
        }
    }
#endif
}
