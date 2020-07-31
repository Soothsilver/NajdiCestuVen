using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Nsnbc.Phases;

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
        public static BookmarkId AsBookmarkId(this XAttribute? attribute)
        {
            return (BookmarkId) Enum.Parse(typeof(BookmarkId), attribute?.Value ?? "None");
        }
        public static Rectangle AsRectangle(this XElement element)
        {
            string str = element.Value;
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