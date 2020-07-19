﻿using Microsoft.Xna.Framework.Graphics;
 using Nsnbc.PostSharp;

 namespace Auxiliary
{   
    [Trace]

    public class BitmapFontGroup
    {
        public static BitmapFontGroup Main40 = null!;
        public static BitmapFontGroup Main32 = null!;
        public static BitmapFontGroup Main24 = null!;
        public static BitmapFontGroup DefaultFont => Main40;

        public readonly SpriteFont Regular;
        public readonly SpriteFont Italics;
        public readonly SpriteFont Bold;
        public readonly SpriteFont BoldItalics;
        public BitmapFontGroup? DegradesTo { get; }

        public BitmapFontGroup(SpriteFont regular, SpriteFont italics, SpriteFont bold, SpriteFont boldItalics, BitmapFontGroup? degradesTo = null)
        {
            DegradesTo = degradesTo;
            Regular = regular; Italics = italics; Bold = bold; BoldItalics = boldItalics;
        }

    }
}