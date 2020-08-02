using System;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;

namespace Nsnbc.PostSharp
{
    [AspectTypeDependency(AspectDependencyAction.Order, AspectDependencyPosition.After, typeof(InternalTraceAspect))]
    [Serializable]
    public class LogAndSwallowAttribute : OnExceptionAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            Logs.Warning("Exception in a method that's swallowing exceptions.", args.Exception);
            args.ReturnValue = null;
            args.FlowBehavior = FlowBehavior.Continue;
        }
    }
}