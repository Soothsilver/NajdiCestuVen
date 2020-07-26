namespace Nsnbc.Texts
{
    public class GString
    {
        private readonly string canonicalText;

        public GString(string canonicalText)
        {
            this.canonicalText = canonicalText;
        }

        public override string ToString()
        {
            return G.ActiveCatalog.GetString(canonicalText);
        }
    }
}