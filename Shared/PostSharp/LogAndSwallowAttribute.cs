using System;
using PostSharp.Aspects;
using PostSharp.Patterns.Diagnostics;

namespace Nsnbc.PostSharp
{
    [Serializable]
    public class LogAndSwallowAttribute : OnExceptionAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            LogSource.Get().Warning.Write(FormattedMessageBuilder.Formatted("Exception in a method that's swallowing exceptions."), args.Exception);
            args.ReturnValue = null;
            args.FlowBehavior = FlowBehavior.Continue;
        }
    }
}