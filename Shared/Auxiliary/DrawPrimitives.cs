using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nsnbc.Auxiliary
{
    /// <summary>
    /// This class contains methods for drawing 2D primitives. 
    /// WARNING! Before using it, you must call the Primitives.Init() function. This method is called automatically by Root.Init(), however.
    /// </summary>
    public static class Primitives
    {
        private static SpriteBatch spriteBatch = null!;
        private static GraphicsDevice graphicsDevice = null!;


        /// <summary>
        /// Draws a filled rectangle without borders. 
        /// </summary>
        public static void FillRectangle(Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(Library.Pixel, rectangle, color);
        }
        /// <summary>
        /// Draw the border of a rectangle, without filling it in.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="color">Color of the border.</param>
        /// <param name="thickness">Number of pixels (line width)</param>
        public static void DrawRectangle(Rectangle rectangle, Color color, int thickness = 1)
        {
            spriteBatch.Draw(Library.Pixel, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, thickness), color);
            spriteBatch.Draw(Library.Pixel, new Rectangle(rectangle.X, rectangle.Y, thickness, rectangle.Height), color);
            spriteBatch.Draw(Library.Pixel, new Rectangle(rectangle.X + rectangle.Width - thickness, rectangle.Y, thickness, rectangle.Height), color);
            spriteBatch.Draw(Library.Pixel, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - thickness, rectangle.Width, thickness), color);
        }
        /// <summary>
        /// Draw a filled rectangle with a border.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="innerColor">The fill-in color.</param>
        /// <param name="outerColor">The border color.</param>
        /// <param name="thickness">Border width.</param>
        public static void DrawAndFillRectangle(Rectangle rectangle, Color innerColor, Color outerColor, int thickness = 1)
        {
            FillRectangle(rectangle, innerColor);
            DrawRectangle(rectangle, outerColor, thickness);
        }
        /// <summary>
        /// Draws a square centered on the specified position.
        /// </summary>
        /// <param name="position">Center of the square.</param>
        /// <param name="color">Color of the square.</param>
        /// <param name="size">VideoWidth of the square.</param>
        [PublicAPI]
        public static void DrawPoint(Vector2 position, Color color, int size = 1)
        {
            FillRectangle(new Rectangle((int)position.X - size /2, (int)position.Y - size/2, size, size), color);
        }
        /// <summary>
        /// Draws a line between two points in 2D space.
        /// </summary>
        /// <param name="startPoint">Line starts at this point.</param>
        /// <param name="endPoint">Line ends at this point.</param>
        /// <param name="color">Color of the line.</param>
        /// <param name="width">VideoWidth of the line in pixels.</param>
        public static void DrawLine(Vector2 startPoint, Vector2 endPoint, Color color, int width = 1)
        {
            float angle = (float)Math.Atan2(endPoint.Y - startPoint.Y, endPoint.X - startPoint.X);
            float length = Vector2.Distance(startPoint, endPoint);
            spriteBatch.Draw(Library.Pixel, startPoint, null, color, angle, Vector2.Zero, new Vector2(length, width), SpriteEffects.None, 0);
        }
        /// <summary>
        /// Draws string using the basic XNA method. Does not perform word-wrap (it is much faster, though).
        /// </summary>
        /// <param name="text">Text to draw.</param>
        /// <param name="position">Position of the top-left corner.</param>
        /// <param name="color">Text color.</param>
        /// <param name="font">Font of the text.</param>
        /// <param name="scale">Text will be scaled down or up. A scale of 1 means normal size.</param>
        [PublicAPI]
        public static void DrawSingleLineText(string text, Vector2 position, Color color, SpriteFont font, float scale = 1)
        {
            spriteBatch.DrawString(font, text, position, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
        /// <summary>
        /// Draws a Texture2D, possibly preserving aspect-ratio (based on parameters).
        /// </summary>
        /// <param name="texture">The texture to draw.</param>
        /// <param name="rectangle">The rectangle to fill with the texture.</param>
        /// <param name="color">Tint the image with this color. A value of null (default) means no tinting (i.e. white).</param>
        /// <param name="scale">If true, the drawing will preserve aspect ratio.</param>
        /// <param name="scaleUp">If true, and we preserve aspect ratio, then the image will be scaled up if necessary.</param>
        /// <param name="scaleBgColor">The color that fills the background in case aspect-ratio is preserved. By default, this is null, i.e. no color.</param>
        public static void DrawImage(Texture2D texture, Rectangle rectangle, Color? color = null, bool scale = false, bool scaleUp = true, Color? scaleBgColor = null)
        {
            Color clr = color ?? Color.White;
            
            if (scale)
            {
                Color clrB = scaleBgColor ?? Color.Transparent;
                FillRectangle(rectangle, clrB);
                spriteBatch.Draw(texture, Utilities.ScaleRectangle(rectangle, texture.Width, texture.Height, scaleUp), clr);

            }
            else
            {
                spriteBatch.Draw(texture, rectangle, clr);
            }
        }

        /* ROUNDED RECTANGLES */
        /// <summary>
        /// Draw the border of a rounded rectangle.
        /// WARNING! Methods for drawing rounded rectangles store rectangle mask textures in memory for performance reasons.
        /// If you draw multiple rounded rectangles of different sizes, you may have both performance and memory problems.
        /// You may, however, draw a rectangle of the same dimensions multiple times on different areas of the screen without problems.
        /// </summary>
        [PublicAPI]
        public static void DrawRoundedRectangle(Rectangle rectangle, Color color, int thickness = 1)
        {
            if (rectangle.Width > 1500 || rectangle.Height > 1500)
            {
                DrawRectangle(rectangle, color, thickness);
                return;
            }
            InnerDrawRoundedRectangle(rectangle, color, color, thickness, false, true);
        }
        /// <summary>
        /// Draws a filled rounded rectangle without a border.
        /// WARNING! Methods for drawing rounded rectangles store rectangle mask textures in memory for performance reasons.
        /// If you draw multiple rounded rectangles of different sizes, you may have both performance and memory problems.
        /// You may, however, draw a rectangle of the same dimensions multiple times on different areas of the screen without problems.
        /// </summary>
        [PublicAPI]
        public static void FillRoundedRectangle(Rectangle rectangle, Color color)
        {
            if (rectangle.Width > 1500 || rectangle.Height > 1500)
            {
                FillRectangle(rectangle, color);
                return;
            }
            InnerDrawRoundedRectangle(rectangle, color, Color.Transparent, 0, true, false);
        }
        /// <summary>
        /// Draws a filled rounded rectangle with a border.
        /// WARNING! Methods for drawing rounded rectangles store rectangle mask textures in memory for performance reasons.
        /// If you draw multiple rounded rectangles of different sizes, you may have both performance and memory problems.
        /// You may, however, draw a rectangle of the same dimensions multiple times on different areas of the screen without problems.
        /// </summary>
        [PublicAPI]
        public static void DrawAndFillRoundedRectangle(Rectangle rectangle, Color innerColor, Color outerColor, int thickness = 1)
        {
            FillRoundedRectangle(rectangle, innerColor);
            DrawRoundedRectangle(rectangle, outerColor, thickness);
        }
        /// <summary>
        /// The size, in pixels, of the rounded corners.
        /// </summary>
        [PublicAPI]
        public static int CornerSize = 8;
        private static void InnerDrawRoundedRectangle(Rectangle rectangle, Color innerColor, Color outerColor, int width, bool doFill, bool doDrawBorder)
        {
            // In case of insufficient size, fall back to full rectangle.
            if (rectangle.Width < CornerSize*2 || rectangle.Height < CornerSize*2)
            {
                if (doFill)
                    FillRectangle(rectangle, innerColor);
                if (doDrawBorder)
                    DrawRectangle(rectangle, outerColor, width);
                return;
            }
            // Check the cache.
            Vector2 rectDimensions = new Vector2(rectangle.Width, rectangle.Height);
            bool containsKeyDimension = RoundedRectangleCache.ContainsKey(rectDimensions);
            if (containsKeyDimension)
            {
                foreach (var rr in RoundedRectangleCache[rectDimensions])
                {
                    if (rr.Thickness == width)
                    {
                        if (doFill)
                            spriteBatch.Draw(rr.FillInTexture, rectangle, innerColor);
                        else if (doDrawBorder)
                            spriteBatch.Draw(rr.BorderTexture, rectangle, outerColor);
                        return;
                    }
                }
                
            }
            // Cache search failed. Create new texture.
            Texture2D textureFill = new Texture2D(graphicsDevice, rectangle.Width, rectangle.Height);
            Texture2D textureDraw = new Texture2D(graphicsDevice, rectangle.Width, rectangle.Height);
       
            Color[] dataFill = new Color[rectangle.Width * rectangle.Height];
            Color[] dataDraw = new Color[rectangle.Width * rectangle.Height];
            for (int i = 0; i < dataFill.Length; i++)
            {
                dataFill[i] = Color.Transparent;
                dataDraw[i] = Color.Transparent;
            }
            for (int x = CornerSize; x < rectangle.Width - CornerSize; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    dataDraw[y * rectangle.Width + x] = Color.White;
                    dataDraw[(rectangle.Height - 1 - y) * rectangle.Width + x] = Color.White;
                }
                for (int y = 0; y < rectangle.Height; y++)
                {
                    dataFill[y * rectangle.Width + x] = Color.White;
                    dataFill[(rectangle.Height - 1 -y) * rectangle.Width + x] = Color.White;
                }
            }
            for (int y = CornerSize; y < rectangle.Height - CornerSize; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    dataDraw[y * rectangle.Width + x] = Color.White;
                    dataDraw[y * rectangle.Width + (rectangle.Width - x - 1)] = Color.White;
                }
                for (int x = 0; x < rectangle.Width; x++)
                {
                    dataFill[y * rectangle.Width + x] = Color.White;
                    dataFill[y * rectangle.Width + (rectangle.Width - x - 1)] = Color.White;
                }
            }


            const double angleStep = 0.01f;
            Point[] centerPoints = { new Point(rectangle.Width-CornerSize-1, rectangle.Height-CornerSize-1),
                                                 new Point(CornerSize, rectangle.Height-CornerSize-1),
                                                 new Point(CornerSize, CornerSize),
                                                 new Point(rectangle.Width-CornerSize-1, CornerSize)};
            double[] startAngles = { 0, Math.PI / 2, Math.PI, Math.PI * 3 / 2 };
            for (int i = 0; i < 4; i++)
            {
                for (double angle = startAngles[i]; angle < startAngles[i]+Math.PI / 2; angle += angleStep)
                {
                    for (int reach = CornerSize; reach > CornerSize - width; reach--)
                    {
                        int y = (int)(Math.Round(Math.Sin(angle) * reach));
                        int x = (int)(Math.Round(Math.Cos(angle) * reach));
                        Point target = new Point(centerPoints[i].X + x, centerPoints[i].Y + y);
                        dataDraw[target.Y * rectangle.Width + target.X] = Color.White;
                    } 
                    for (int reach = CornerSize; reach >= 0; reach--)
                    {
                        int y = (int)(Math.Round(Math.Sin(angle) * reach));
                        int x = (int)(Math.Round(Math.Cos(angle) * reach));
                        Point target = new Point(centerPoints[i].X + x, centerPoints[i].Y + y);
                        dataFill[target.Y * rectangle.Width + target.X] = Color.White;
                    }
                }
            }
            textureFill.SetData(dataFill);
            textureDraw.SetData(dataDraw);
            if (!containsKeyDimension)
            {
                RoundedRectangleCache.Add(rectDimensions, new List<RoundedRectangle>());
            }
            RoundedRectangleCache[rectDimensions].Add(new RoundedRectangle(textureFill, textureDraw, width));
            InnerDrawRoundedRectangle(rectangle, innerColor, outerColor, width, doFill, doDrawBorder);
        }
        private static readonly Dictionary<Vector2, List<RoundedRectangle>> RoundedRectangleCache = new Dictionary<Vector2, List<RoundedRectangle>>();

        /* CIRCLES */
       /// <summary>
        /// Draws a filled circle. 
        /// WARNING! This and the DrawCircle method store circle textures in memory for performance reasons.
        /// If you draw multiple circles of different radii, you may have both performance and memory problems.
        /// </summary>
        [PublicAPI]
        public static void FillCircle(Vector2 center, int radius, Color color)
        {
            InnerDrawCircle(center, radius, color, true, 1);
        }    
        /// <summary>
        /// Draws an outline of a circle. 
        /// WARNING! This and the FillCircle method store circle textures in memory for performance reasons.
        /// If you draw multiple circles of different radii, you may have both performance and memory problems.
        /// </summary>
        [PublicAPI]
        public static void DrawCircle(Vector2 center, int radius, Color color, int thickness = 1)
        {
            InnerDrawCircle(center, radius, color, false, thickness);
        }
        /// <summary>
        /// Clears all cached Circle textures. This will clear space from memory, but drawing circles will take longer. Then runs garbage collector.
        /// </summary>
        [PublicAPI]
        public static void ClearCircleCache()
        {
            CirclesCache.Clear();
            GC.Collect();
        }
        private static void InnerDrawCircle(Vector2 center, int radius, Color color, bool filled, int thickness)
        {
            bool containsKeyRadius = CirclesCache.ContainsKey(radius);
            if (containsKeyRadius)
            {
                foreach(Circle c in CirclesCache[radius])
                {
                    if (c.Filled == filled && (filled || c.Thickness == thickness))
                    {
                        spriteBatch.Draw(c.Texture, center, null, color, 0, new Vector2(radius + 1, radius + 1), 1, SpriteEffects.None, 0);
                        return;
                    }
                }
            }
            int outerRadius = radius * 2 + 2;
            Texture2D texture = new Texture2D(graphicsDevice, outerRadius, outerRadius);
            if (filled) thickness = radius + 1;

            Color[] data = new Color[outerRadius * outerRadius];
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;

            double angleStep = 0.5f / radius;

            int lowpoint = radius - thickness;
            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                for (int i = radius; i > lowpoint; i--)
                {
                    int x = (int)Math.Round(radius + i * Math.Cos(angle));
                    int y = (int)Math.Round(radius + i * Math.Sin(angle));
                    data[y * outerRadius + x + 1] = color;
                }
            }
            texture.SetData(data);
            if (containsKeyRadius)
                CirclesCache[radius].Add(new Circle(texture, filled, thickness));
            else
                CirclesCache.Add(radius, new List<Circle>(new[] { new Circle(texture, filled, thickness) }));

            spriteBatch.Draw(texture, center, null, color, 0, new Vector2(radius + 1, radius + 1), 1, SpriteEffects.None, 0);
        }
        private static readonly Dictionary<int, List<Circle>> CirclesCache = new Dictionary<int, List<Circle>>();

        /// <summary>
        /// This method is called automatically from Root.Init(). It will enable the use of this static class.
        /// </summary>
        public static void Init(SpriteBatch spriteBatchParameter, GraphicsDevice graphicsDeviceParameter)
        {
            spriteBatch = spriteBatchParameter;
            graphicsDevice = graphicsDeviceParameter;
        }
        private class Circle
        {
            public readonly Texture2D Texture;
            public readonly bool Filled;
            public readonly int Thickness;
            public Circle(Texture2D texture, bool filled, int thickness)
            {
                Texture = texture; Filled = filled; Thickness = thickness;
            }
        }
        private class RoundedRectangle
        {
            public readonly Texture2D FillInTexture;
            public readonly Texture2D BorderTexture;
            public readonly int Thickness;
            public RoundedRectangle(Texture2D fillInTexture, Texture2D borderTexture, int thickness)
            {
                Thickness = thickness;
                FillInTexture = fillInTexture;
                BorderTexture = borderTexture;
            }
        }

        public static void DrawZoomed(Texture2D art, Rectangle currentZoom)
        {
            spriteBatch.Draw(art, CommonGame.R1920x1080, currentZoom, Color.White);
        }
    }
 
}
