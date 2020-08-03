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

            string fullString = new string(' ', indentation.Value) + message;
            Logger.Write(fullString.Replace("{b}", "").Replace("{/b}", ""), fullString);
        }

        public static void Error(string message, Exception? exception = null)
        {
            WriteCore("{b}[ERROR] " + Clean(message) + (exception != null ? exception.ToString() : "") + "{/b}");
        }

        public static void Info(string message)
        {
            WriteCore("{b}INFO:{/b} " + Clean(message));
        }

        public static void Debug(string message)
        {
            WriteCore(Clean(message));
        }

        private static string Clean(string message)
        {
            return message
                    .Replace('{', '[').Replace('}', ']')

                    .Replace("[BOLD]", "{b}")
                    .Replace("[ENDBOLD]", "{/b}")
                ;
        }
    }

    public interface ITracer
    {
        void Write(string message, string formattedMessages);
        IEnumerable<string> LogLines { get; }
    }

    public class NothingLogger : ITracer
    {
        public void Write(string message, string formattedMessages)
        {
        }

        public IEnumerable<string> LogLines { get; } = new List<string>();
    }
}