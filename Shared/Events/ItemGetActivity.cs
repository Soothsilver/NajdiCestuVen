using System;
using Microsoft.Xna.Framework;
using Nsnbc.Auxiliary;
using Nsnbc.Events;
using Nsnbc.Stories;

namespace Nsnbc.Core
{
    public class ItemGetActivity : IDrawableActivity
    {
        public InventoryItem InventoryItem { get; }

        public ItemGetActivity(InventoryItem inventoryItem)
        {
            InventoryItem = inventoryItem;
            drawWhere = Rectangle.Empty;
        }

        public bool Blocking => false;
        public bool Dead { get; private set; }
        private float timeLeft = 0.5f;
        private bool movingAway = false;
        private float opacity = 0;
        private float percentComplete = 0;
        private Rectangle drawWhere;
        
        public void Update(AirSession airSession, float elapsedSeconds)
        {
            timeLeft -= elapsedSeconds;
            if (movingAway)
            {
                percentComplete += elapsedSeconds;
                opacity -= elapsedSeconds;
            }
            else if (opacity < 1)
            {
                opacity += elapsedSeconds * 4;
                if (opacity >= 1)
                {
                    opacity = 1;
                }
            }
            if (timeLeft <= 0)
            {
                if (movingAway)
                {
                    this.Dead = true;
                }
                else
                {
                    timeLeft = 1;
                    movingAway = true;
                }
            }
            
            Rectangle originalRectangle = new Rectangle(Root.Screen.Width / 2 - 128, Root.Screen.Height / 2 - 128, 256, 256);
            int inventoryIndex = airSession.Session.Inventory.IndexOf(InventoryItem);
            if (inventoryIndex == -1)
            {
                drawWhere = originalRectangle;
            }
            else
            {
                Rectangle targetRectangle = new Rectangle((inventoryIndex +1) * 128, 0, 128, 128);
                Rectangle rectNow = new Rectangle(
                    (int)((targetRectangle.X - originalRectangle.X) * percentComplete) + originalRectangle.X,
                    (int)((targetRectangle.Y - originalRectangle.Y) * percentComplete) + originalRectangle.Y,
                    (int)((targetRectangle.Width - originalRectangle.Width) * percentComplete) + originalRectangle.Width,
                    (int)((targetRectangle.Height - originalRectangle.Height) * percentComplete) + originalRectangle.Height
                );
                drawWhere = rectNow;
            }
        }

        public void Draw()
        {
            Rectangle rect = drawWhere;
            Primitives.FillRectangle(rect, Color.LightBlue.Alpha(OpacityInt));
            Primitives.DrawImage(Library.Art(InventoryItem.Art), rect, Color.White.Alpha(OpacityInt), scale: true, scaleUp: true);
            Primitives.DrawRectangle(rect.Extend(2,2), Color.Black.Alpha(OpacityInt), 2);
        }

        public int OpacityInt => (int) MathHelper.Clamp((255 * opacity), 0, 255);
    }
}