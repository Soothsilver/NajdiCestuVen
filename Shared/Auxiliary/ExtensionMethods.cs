using JetBrains.Annotations;
using Microsoft.Xna.Framework;

namespace Auxiliary
{
    /// <summary>
    /// Provides several extension methods to XNA-only types (general extension methods are implemented in Cother).
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Returns the given color with modified alpha component.
        /// </summary>
        /// <param name="color">The base color.</param>
        /// <param name="alpha">New alpha component (0 to 255).</param>
        public static Color Alpha(this Color color, int alpha)
        {
            return Color.FromNonPremultiplied(color.R, color.G, color.B, alpha);
        }
        /// <summary>
        /// Pushes the rectangle out by the horizontal and vertical amount specified, then returns this new rectangle. You may deflate by using negative values. Each side will be pushed out by the given
        /// number of pixels. For example, an inflation of 10 will increase the width of the rectangle by 20.
        /// </summary>
        /// <param name="rectangle">The inflated rectangle.</param>
        /// <param name="horizontalInflation">Extend by this amount horizontally.</param>
        /// <param name="verticalInflation">Extend by this amount vertically.</param>
        /// <returns></returns>
        public static Rectangle Extend(this Rectangle rectangle, int horizontalInflation, int verticalInflation)
        {
            Rectangle newRectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            newRectangle.Inflate(horizontalInflation, verticalInflation);
            return newRectangle;
        }

        /// <summary>
        /// Returns a rectangle moved to the right, with its width cut proportionately.
        /// </summary>
        /// <param name="rectangle">The original rectangle.</param>
        /// <param name="amount">Move rectangle to the right by this amount of pixels.</param>
        [PublicAPI]
        public static Rectangle MoveToRight(this Rectangle rectangle, int amount)
        {
            Rectangle newRectangle = new Rectangle(rectangle.X + amount, rectangle.Y, rectangle.Width - amount, rectangle.Height);
            return newRectangle;
        }
    }
}
