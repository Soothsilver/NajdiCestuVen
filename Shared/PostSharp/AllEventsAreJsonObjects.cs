using System;
using System.Reflection;
using Newtonsoft.Json;
using Nsnbc.PostSharp;
using PostSharp.Constraints;
using PostSharp.Extensibility;

[assembly: AllEventsAreJsonObjects(AttributeTargetTypes = "*QEvent", AttributeInheritance = MulticastInheritance.Strict)]

namespace Nsnbc.PostSharp
{
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Class)]
    public class AllEventsAreJsonObjects : ScalarConstraint
    {
        public override void ValidateCode(object target)
        {
            if (target is Type t)
            {
                foreach (CustomAttributeData customAttributeData in t.GetCustomAttributesData())
                {
                    if (customAttributeData.AttributeType == typeof(JsonObjectAttribute))
                    {
                        if (customAttributeData.ConstructorArguments.Count >= 1 &&
                            (int)customAttributeData.ConstructorArguments[0].Value == (int) MemberSerialization.Fields)
                        {
                            // Success
                            return;
                        }
                    }
                }

                Message.Write(t, SeverityType.Error, "CUS2", "The event '" + target + "' is not JsonObject(Fields).");
            }
            base.ValidateCode(target);
        }
    }
}