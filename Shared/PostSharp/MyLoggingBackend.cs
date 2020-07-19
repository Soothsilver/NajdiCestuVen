using System;
using System.IO;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends;
using PostSharp.Patterns.Diagnostics.RecordBuilders;
using PostSharp.Patterns.Formatters;

namespace Nsnbc.PostSharp
{
   

    public class MyLoggingBackend : TextLoggingBackend
    {
        public readonly StreamWriter Writer;
        private readonly TextLoggingBackendOptions options = new TextLoggingBackendOptions();

        public MyLoggingBackend()
        {
            string logFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NajdiCestuVen.log.txt");
            Console.WriteLine("Logging into " + logFile);
            Writer = new StreamWriter(logFile);
        }
        protected override LoggingTypeSource CreateTypeSource(LoggingNamespaceSource parent, Type type)
        {
            return new MyLoggingTypeSource(parent, type);
        }

        public override LogRecordBuilder CreateRecordBuilder()
        {
            return new MyLoggingRecordBuilder(this);
        }

        protected override void Dispose(bool disposing)
        {
            Writer.Close();
            base.Dispose(disposing);
        }

        protected override TextLoggingBackendOptions GetTextBackendOptions() => options;
    }

    public class MyLoggingRecordBuilder : TextLogRecordBuilder
    {
        public MyLoggingBackend MyBackend { get; }

        public MyLoggingRecordBuilder(MyLoggingBackend myBackend) : base(myBackend)
        {
            MyBackend = myBackend;
        }

        protected override void Write(UnsafeString message)
        {
            MyBackend.Writer.WriteLine(message.ToString());
            MyBackend.Writer.Flush();
        }
    }

    public class MyLoggingTypeSource : LoggingTypeSource
    {
        public MyLoggingTypeSource(LoggingNamespaceSource parent, Type type) : base(parent,type)
        {
        }

        protected override bool IsBackendEnabled(LogLevel level)
        {
            return true;
        }
    }
}