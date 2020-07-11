using System.Collections.Generic;
using System.Reflection;
using Nsnbc.PostSharp;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using PostSharp.Patterns.Diagnostics;

[assembly: Trace(AttributeTargetTypes = "Nsnbc.Events.*", AttributeTargetMemberAttributes = ~MulticastAttributes.Abstract)]

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
            yield return new AspectInstance(method, new LogAttribute());
        }
    }
}