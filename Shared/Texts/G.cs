using System;
using System.Globalization;
using System.IO;
using System.Linq;
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
            Stream moFileStream = File.OpenRead("Texts\\en_US.mo");
            englishCatalog = new Catalog(moFileStream, new CultureInfo("en-US"));
            czechCatalog = new Catalog(new CultureInfo("cs_CZ"));
            ApplyCurrentLanguage();
        }
        
        
        public static string T(string text)
        {
            return activeCatalog.GetString(text.Replace("…","..."));
        }
        public static string Tn(string text)
        {
            return activeCatalog.GetString(text.Replace("\r", ""));
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