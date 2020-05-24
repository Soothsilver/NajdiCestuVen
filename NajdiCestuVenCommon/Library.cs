using Microsoft.Xna.Framework.Content;

namespace NajdiCestuVenCommon
{
    public static class Library
    {      
        private static ContentManager content;

        public static void Init(ContentManager content)
        {
            content = contentParameter;

        }
        
        private static void LoadTexture(out Texture2D texture2D, string assetName)
        {
            texture2D = Assets.content.Load<Texture2D>(assetName);
        }
    }
}