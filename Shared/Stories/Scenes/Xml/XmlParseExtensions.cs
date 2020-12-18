using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace Nsnbc.Stories.Scenes.Xml
{
    public static class XmlParseExtensions
    {
        public static ArtName AsArt(this XElement? element)
        {
            return (ArtName) Enum.Parse(typeof(ArtName), element?.Value ?? "Null");
        }
        public static ArtName AsArt(this XAttribute? attribute)
        {
            return (ArtName) Enum.Parse(typeof(ArtName), attribute?.Value ?? "Null");
        }  
        public static int AsInt(this XAttribute? attribute)
        {
            return Convert.ToInt32(attribute?.Value ?? "0");
        }  
        public static float AsFloat(this XAttribute? attribute)
        {
            return Convert.ToSingle(attribute?.Value ?? "0", CultureInfo.InvariantCulture);
        }  
        public static T AsEnum<T>(this XAttribute attribute) where T : Enum
        {
            return (T) Enum.Parse(typeof(T), attribute.Value);
        }  
        public static BookmarkId AsBookmarkId(this XAttribute? attribute)
        {
            return (BookmarkId) Enum.Parse(typeof(BookmarkId), attribute?.Value ?? "None");
        }
        public static Rectangle AsRectangle(this XElement element)
        {
            string str = element.Value;
            return asRectangle(str);
        }
        public static Rectangle AsRectangle(this XAttribute attribute)
        {
            string str = attribute.Value;
            return asRectangle(str);
        }

        private static Rectangle asRectangle(string str)
        {
            string[] split = str.Split(',');
            int x = Int32.Parse(split[0].Trim());
            int y = Int32.Parse(split[1].Trim());
            int w = Int32.Parse(split[2].Trim());
            int h = Int32.Parse(split[3].Trim());
            return new Rectangle(x, y, w, h);
        }

        [return: NotNull]
        public static IEnumerable<T> AsNotNull<T>(this IEnumerable<T>? original) where T: class
        {
            return original ?? Enumerable.Empty<T>();
        }
    }
}