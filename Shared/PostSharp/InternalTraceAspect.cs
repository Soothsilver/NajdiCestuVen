using System.Linq;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace Nsnbc.PostSharp
{
    [PSerializable]
    public class InternalTraceAspect : OnMethodBoundaryAspect
    {
        private string self;
        private string selfStart;
        private string selfEnd;
        private string selfError;
        
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            self = method.DeclaringType.Name + ".[BOLD]" + method.Name + "[ENDBOLD](";
            bool first = true;
            if (!method.IsStatic)
            {
                self += "this={0}";
                first = false;
            }

            int i = 1;
            foreach (var _ in method.GetParameters())
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    self += ",";
                }

                self += "{" + i + "}";
                i++;
            }
            self += ")";
            selfStart = self + " Starting.";
            selfEnd = self + " Finished.";
            selfError = self + " Failed.";
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            Logs.Debug(string.Format(selfStart, new[] { args.Instance }.Concat(args.Arguments).ToArray()));
            Logs.Indent();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            Logs.Unindent();
            // Logs.Info(string.Format(selfEnd, new[] { args.Instance }.Concat(args.Arguments).ToArray()));
        }
#if !DEBUG
        public override void OnException(MethodExecutionArgs args)
        {           
            Logs.Error(string.Format(selfEnd, new[] { args.Instance }.Concat(args.Arguments).ToArray()), args.Exception);
            base.OnException(args);
        }
#endif
    }
}