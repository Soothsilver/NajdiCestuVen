using System.Collections.Generic;
using System.Reflection;
using Nsnbc.PostSharp;
using PostSharp.Aspects;
using PostSharp.Community.ToString;
using PostSharp.Extensibility;

[assembly: Trace(AttributeTargetTypes = "Nsnbc.Events.*", AttributeTargetMemberAttributes = ~MulticastAttributes.Abstract)]
[assembly: ToStringGlobalOptions(IncludePrivate = true, WriteTypeName = false, WrapWithBraces = false)]
//[assembly: ToString]

namespace Nsnbc.PostSharp
{
    [MulticastAttributeUsage(MulticastTargets.Method)]
    public class TraceAttribute : MulticastAttribute, IAspectProvider
    {
        public IEnumerable<AspectInstance> ProvideAspects(object targetElement)
        {
            MethodBase method = (MethodBase) targetElement;
            if (method.Name.StartsWith("get_") || method.Name == "Draw" || method.Name == "Update")
            {
                yield break;
            }
            yield return new AspectInstance(method, new InternalTraceAspect());
        }
    }
}