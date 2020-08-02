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
using PostSharp.Patterns.Diagnostics;

namespace Nsnbc.Phases
{
    public class DebugLogPhase : GamePhase
    {
        private Rectangle rectangle;
        private List<string> theLines  = new List<string>();
        private int lineHeight;

        public DebugLogPhase()
        {
            this.rectangle = new Rectangle(10, 50, Root.Screen.Width-20, Root.Screen.Height-60);
            var logLines = Logs.Logger.LogLines;
            lineHeight = BitmapFontGroup.Main12.Regular.LineSpacing;
            int maximumheight = rectangle.Height - 100;
            int lines = maximumheight / lineHeight;
            int startAt = Math.Max(0, logLines.Count() - lines);
            theLines = logLines.Skip(startAt).Select(s => s.Replace('{', '[').Replace('}', ']')).ToList();
        }

        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {
            Primitives.FillRectangle(Root.Screen, Color.LightBlue.Alpha(240));
            Writer.DrawString(G.T("Stiskni F1 pro návrat do hry."), Root.Screen.Extend(-4,-4), Color.Black, BitmapFontGroup.Main24, Writer.TextAlignment.TopLeft);
            for (int i = 0; i < theLines.Count; i++)
            {
                Writer.DrawString(theLines[i], new Rectangle(this.rectangle.X, this.rectangle.Y + lineHeight * i, 40000, 2000), Color.Black, BitmapFontGroup.Main12);
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