using System;
using Newtonsoft.Json;

namespace Nsnbc.Texts
{
    [JsonObject(MemberSerialization.Fields)]
    public class GString
    {
        protected readonly string canonicalText;

        public GString(string canonicalText)
        {
            this.canonicalText = canonicalText;
        }

        public override string ToString()
        {
            return G.ActiveCatalog.GetString(canonicalText);
        }

        public static GString Pure(string untranslatedString)
        {
            return new PureGString(untranslatedString);    
        }
    }

    public class PureGString : GString
    {
        public PureGString(string untranslatedString) : base(untranslatedString)
        {
        }

        public override string ToString()
        {
            return canonicalText;
        }
    }
}