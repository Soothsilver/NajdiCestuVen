using System;
using Microsoft.Xna.Framework;

namespace Nsnbc.Phases
{
    public class Tab
    {
        public string Caption { get; }
        public Action<Rectangle> Draw { get; }

        public Tab(string caption, Action<Rectangle> draw)
        {
            Caption = caption;
            Draw = draw;
        }
    }
}