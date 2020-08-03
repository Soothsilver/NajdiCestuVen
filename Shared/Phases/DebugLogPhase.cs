using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nsnbc.Auxiliary;
using Nsnbc.Auxiliary.Fonts;
using Nsnbc.PostSharp;
using Nsnbc.Texts;

namespace Nsnbc.Phases
{
    public class DebugLogPhase : GamePhase
    {
        private readonly Rectangle rectangle;
        private readonly List<string> theLines;
        private readonly int lineHeight;

        public DebugLogPhase()
        {
            rectangle = new Rectangle(10, 50, Root.Screen.Width-20, Root.Screen.Height-60);
            var logLines = Logs.Logger.LogLines.ToList();
            lineHeight = BitmapFontGroup.Main12.Regular.LineSpacing;
            int maximumHeight = rectangle.Height - 100;
            int lines = maximumHeight / lineHeight;
            int startAt = Math.Max(0, logLines.Count - lines);
            theLines = logLines.Skip(startAt).ToList();
        }

        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {
            Primitives.FillRectangle(Root.Screen, Color.LightBlue.Alpha(240));
            Writer.DrawString(G.T("Stiskni F1 pro návrat do hry."), Root.Screen.Extend(-4,-4), Color.Black, BitmapFontGroup.Main24);
            for (int i = 0; i < theLines.Count; i++)
            {
                Writer.DrawString(theLines[i], new Rectangle(rectangle.X, rectangle.Y + lineHeight * i, 40000, 2000), Color.Black, BitmapFontGroup.Main12);
            }
        }

        protected internal override void Update(Game game, float elapsedSeconds)
        {
            if (Root.WasKeyPressed(Keys.Escape))
            {
                Root.PopFromPhase();
            }
            if ((Root.WasMouseLeftClick || Root.WasTouchReleased))
            {
                Root.WasMouseLeftClick = false;
                Root.WasTouchReleased = false;
                if (Ux.MouseOverAction != null)
                {
                    Ux.MouseOverAction();
                }
            }
            if (Root.WasKeyPressed(Keys.F1))
            {
                Root.PopFromPhase();
            }
        }
    }
}