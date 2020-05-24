﻿﻿using Microsoft.Xna.Framework.Graphics;

 namespace Origin.Display
{
    public class BitmapFontGroup
    {
        public static BitmapFontGroup ASemi48;
        public static BitmapFontGroup DefaultFont => ASemi48;

        public readonly SpriteFont Regular;
        public readonly SpriteFont Italics;
        public readonly SpriteFont Bold;
        public readonly SpriteFont BoldItalics;
        public BitmapFontGroup DegradesTo { get; }

        public BitmapFontGroup(SpriteFont regular, SpriteFont italics, SpriteFont bold, SpriteFont boldItalics, BitmapFontGroup degradesTo = null)
        {
            DegradesTo = degradesTo;
            Regular = regular; Italics = italics; Bold = bold; BoldItalics = boldItalics;
        }

    }
}