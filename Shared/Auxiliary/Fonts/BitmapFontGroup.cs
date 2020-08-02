using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using Nsnbc.PostSharp;
using Nsnbc.Services;

namespace Nsnbc.Auxiliary.Fonts
{   
    [Trace]
    public abstract class BitmapFontGroup
    {
        public static BitmapFontGroup Main40 = null!;
        public static BitmapFontGroup Main32 = null!;
        public static BitmapFontGroup Main24 = null!;
        public static BitmapFontGroup Main12 = null!;
        
        public static BitmapFontGroup MainXna40 = null!;
        public static BitmapFontGroup MainXna32 = null!;
        public static BitmapFontGroup MainXna24 = null!;
        public static BitmapFontGroup MainXna12 = null!;
        public static BitmapFontGroup MainExt40 = null!;
        public static BitmapFontGroup MainExt32 = null!;
        public static BitmapFontGroup MainExt24 = null!;
        
        public static BitmapFontGroup DefaultFont => Main40;

        public BitmapFontGroup? DegradesTo { get; protected set; }

        public abstract IFont Regular { get; }
        public abstract IFont Italics { get; }
        public abstract IFont Bold { get; }
        public abstract IFont BoldItalics { get; }

        public static void UpdateMainFont()
        {
            if (Settings.Instance.FontRenderStyle == FontRenderStyle.Xna)
            {
                Main40 = MainXna40;
                Main32 = MainXna32;
                Main24 = MainXna24;
            }
            else
            {
                Main40 = MainExt40;
                Main32 = MainExt32;
                Main24 = MainExt24;
            }
            Main12 = MainXna12;
        }
    }

    public enum FontRenderStyle
    {
        Xna,
        Extended
    }

    public class XnaFontGroup : BitmapFontGroup
    {
        public XnaFontGroup(SpriteFont regular, SpriteFont italics, SpriteFont bold, SpriteFont boldItalics, BitmapFontGroup? degradesTo = null)
        {
            DegradesTo = degradesTo;
            Regular = new XnaSpriteFont(regular); 
            Italics = new XnaSpriteFont(italics); 
            Bold = new XnaSpriteFont(bold); 
            BoldItalics = new XnaSpriteFont(boldItalics);
        }

        public override IFont Regular { get; }
        public override IFont Italics { get; }
        public override IFont Bold { get; }
        public override IFont BoldItalics { get; }
    }
    public class ExtendedFontGroup : BitmapFontGroup
    {
        public ExtendedFontGroup(BitmapFont regular, BitmapFont italics, BitmapFont bold, BitmapFont boldItalics, BitmapFontGroup? degradesTo = null)
        {
            DegradesTo = degradesTo;
            Regular = new ExtendedSpriteFont(regular); 
            Italics = new ExtendedSpriteFont(italics); 
            Bold = new ExtendedSpriteFont(bold); 
            BoldItalics = new ExtendedSpriteFont(boldItalics);
        }

        public override IFont Regular { get; }
        public override IFont Italics { get; }
        public override IFont Bold { get; }
        public override IFont BoldItalics { get; }
    }

    public class ExtendedSpriteFont : IFont
    {
        private readonly BitmapFont delegateFont;

        public ExtendedSpriteFont(BitmapFont delegateFont)
        {
            this.delegateFont = delegateFont;
        }

        public int LineSpacing => delegateFont.LineHeight;
        public Vector2 MeasureString(string text)
        {
            return delegateFont.MeasureString(text);
        }

        public void Draw(SpriteBatch spriteBatch, string text, Vector2 vector2, Color color)
        {
            spriteBatch.DrawString(delegateFont, text, vector2, color);
        }
    }

    public interface IFont
    {
        int LineSpacing { get; }
        Vector2 MeasureString(string text);
        void Draw(SpriteBatch spriteBatch, string text, Vector2 vector2, Color color);
    }

    public class XnaSpriteFont : IFont
    {
        private readonly SpriteFont delegateFont;

        public XnaSpriteFont(SpriteFont delegateFont)
        {
            this.delegateFont = delegateFont;
        }

        public int LineSpacing => delegateFont.LineSpacing;
        public Vector2 MeasureString(string text)
        {
            return delegateFont.MeasureString(text);
        }

        public void Draw(SpriteBatch spriteBatch, string text, Vector2 vector2, Color color)
        {
            spriteBatch.DrawString(delegateFont, text, vector2, color);
        }
    }
}