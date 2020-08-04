using System;
using System.Diagnostics;
using Newtonsoft.Json.Serialization;
using Nsnbc.PostSharp;

namespace Nsnbc.Services
{
    public class JsonNetTraceWriter : ITraceWriter
    {
        public void Trace(TraceLevel level, string message, Exception? ex)
        {
            if (message.StartsWith("Unable"))
            {
                Logs.Error("Serialization: " + message, ex);
            }
        }

        public TraceLevel LevelFilter => TraceLevel.Verbose;
    }
}