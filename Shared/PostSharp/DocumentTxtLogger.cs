using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace Nsnbc.PostSharp
{
    public class DocumentTxtLogger : ITracer
    {
        public readonly StreamWriter Writer;
        public ConcurrentQueue<string> LogLines = new ConcurrentQueue<string>();

        public DocumentTxtLogger()
        {
            
            string logFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NajdiCestuVen.log.txt");
            Console.WriteLine("Logging into " + logFile);
            Writer = new StreamWriter(logFile);
        }

        public void Write(string message, string formattedMessage)
        {
            string logLine = "[" + DateTime.Now.ToString("F") + "] " + message;
            LogLines.Enqueue(formattedMessage);
            Writer.WriteLine(logLine);
            Writer.Flush();
        }

        IEnumerable<string> ITracer.LogLines => LogLines;
    }
}