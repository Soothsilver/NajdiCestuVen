using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Windows.Util;
using GetText;

namespace Nsnbc
{
    public static class G
    {
        private static readonly ICatalog englishCatalog;
        private static readonly ICatalog czechCatalog;
        private static ICatalog activeCatalog = null!;

        static G()
        {
            #if ANDROID
            Stream moFileStream = ResourceUtility.GetEmbeddedResourceStream("Nsnbc.Texts.en_US.mo");
            #else
            Stream moFileStream = ResourceUtility.GetEmbeddedResourceStream("Windows.Texts.en_US.mo");
            #endif
            englishCatalog = new Catalog(moFileStream, new CultureInfo("en-US"));
            czechCatalog = new Catalog(new CultureInfo("cs-CZ"));
            ApplyCurrentLanguage();
        }

        public static ICatalog ActiveCatalog => activeCatalog;
        
        public static GString T(string text)
        {
            return new GString(text.Replace("…","...")); activeCatalog.GetString(text.Replace("…","..."));
        }
        public static GString Tn(string text)
        {
            return new GString(text.Replace("\r", ""));
        }
        public static string T(string text, params object[] args)
        {
            return string.Format(activeCatalog.GetString(text), args);
        }

        public static void ApplyCurrentLanguage()
        {
            activeCatalog = GetText.CurrentLanguage == Language.Czech ? czechCatalog : englishCatalog;
        }

        public static T CzEn<T>(T czech, T english) => GetText.CzEn(czech, english);
    }
}