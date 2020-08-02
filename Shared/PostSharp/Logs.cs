using System;
using System.Collections.Generic;
using System.Threading;

namespace Nsnbc.PostSharp
{
    public static class Logs
    {
        private static readonly ThreadLocal<int> indentation = new ThreadLocal<int>(()=>0);
        
        static Logs()
        {
        }

        public static ITracer Logger { get; set; } = new NothingLogger();

        public static void Indent()
        {
            indentation.Value += 1;
        }
        public static void Unindent()
        {
            indentation.Value -= 1;
        }
        
        
        public static void Warning(string message, Exception exception)
        {
            WriteCore("[WARNING] " + message + (exception != null ? exception.ToString() : ""));
        }

        private static void WriteCore(string message)
        {
            if (message.Contains("DebugLogPhase"))
            {
                return;
            }
            Logger.Write(new string(' ', indentation.Value) + message);
        }

        public static void Error(string message, Exception? exception = null)
        {
            WriteCore("[ERROR] " + message + (exception != null ? exception.ToString() : ""));
        }

        public static void Info(string message)
        {
            WriteCore(message);
        }
    }

    public interface ITracer
    {
        void Write(string message);
        IEnumerable<string> LogLines { get; }
    }

    public class NothingLogger : ITracer
    {
        public void Write(string message)
        {
        }

        public IEnumerable<string> LogLines { get; } = new List<string>();
    }
}