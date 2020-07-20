using System;
using Microsoft.Xna.Framework;

namespace Nsnbc.Phases
{
    public class Tab
    {
        public GString Caption { get; }
        public Action<Rectangle> Draw { get; }

        public Tab(GString caption, Action<Rectangle> draw)
        {
            Caption = caption;
            Draw = draw;
        }
    }
}