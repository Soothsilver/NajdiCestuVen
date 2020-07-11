using System;
using System.Diagnostics;
using Nsnbc.PostSharp;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends;
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
            LoggingServices.DefaultBackend = new MyLoggingBackend();
            using var game = new WindowsGame();
            game.Run();
        }
    }
#endif
}
