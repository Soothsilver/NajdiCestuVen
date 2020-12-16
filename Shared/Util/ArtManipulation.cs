using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nsnbc.Util
{
    public class ArtManipulation
    {
        public static Rectangle GetRelevantRectangle(Texture2D art)
        {
            
            Color[] oldData = new Color[art.Width * art.Height];
            art.GetData(oldData);
         
            // Flipped copy
            int xLeft = 0;
            int xRight = art.Width - 1;
            int yTop = 0;
            int yBottom = art.Height - 1;
            
            for (int column = 0; column < art.Width; column++)
            {
                xLeft = column;
                bool end = false;
                for (int row = 0; row < art.Height; row++)
                {
                    Color color = oldData[row * art.Width + column];
                    if (color != Color.Transparent)
                    {
                        end = true;
                        break;
                    }
                }

                if (end)
                {
                    break;
                }
            }
            
            for (int column = art.Width-1; column >= 0; column--)
            {
                xRight = column;
                bool end = false;
                for (int row = 0; row < art.Height; row++)
                {
                    Color color = oldData[row * art.Width + column];
                    if (color != Color.Transparent)
                    {
                        end = true;
                        break;
                    }
                }

                if (end)
                {
                    break;
                }
            }
            for (int row = 0; row < art.Height; row++)
            {
                yTop = row;
                bool end = false;
                for (int column = 0; column < art.Width; column++)
                {
                    Color color = oldData[row * art.Width + column];
                    if (color != Color.Transparent)
                    {
                        end = true;
                        break;
                    }
                }

                if (end)
                {
                    break;
                }
            }
            for (int row = art.Height-1; row >=0;row--)
            {
                yBottom = row;
                bool end = false;
                for (int column = 0; column < art.Width; column++)
                {
                    Color color = oldData[row * art.Width + column];
                    if (color != Color.Transparent)
                    {
                        end = true;
                        break;
                    }
                }

                if (end)
                {
                    break;
                }
            }

            return new Rectangle(xLeft, yTop, xRight - xLeft, yBottom - yTop);
        }
    }
}