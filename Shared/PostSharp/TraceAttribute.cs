using System.Collections.Generic;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using PostSharp.Patterns.Diagnostics;

[MulticastAttributeUsage(MulticastTargets.Method)]
public class TraceAttribute : MulticastAttribute, IAspectProvider
{
    public IEnumerable<AspectInstance> ProvideAspects(object targetElement)
    {
        MethodBase method = (MethodBase) targetElement;
        if (method.Name.StartsWith("get_"))
        {
            yield break;
        }
        yield return new AspectInstance(method, new LogAttribute());
    }
}