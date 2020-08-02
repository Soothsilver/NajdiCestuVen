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

        public void Write(string message)
        {
            string logLine = "[" + DateTime.Now.ToString("F") + "] " + message.ToString();
            LogLines.Enqueue(logLine);
            Writer.WriteLine(logLine);
            Writer.Flush();
        }

        IEnumerable<string> ITracer.LogLines => LogLines;
    }
}