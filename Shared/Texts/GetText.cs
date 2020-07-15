using System;

namespace Nsnbc
{
    public static class GetText
    {
        public static void ToggleLanguage()
        {
            if (CurrentLanguage == Language.Czech)
            {
                CurrentLanguage = Language.English;
            }
            else
            {
                CurrentLanguage = Language.Czech;
            }
            G.ApplyCurrentLanguage();
            WhenLanguageToggled?.Invoke();
        }

        public static event Action? WhenLanguageToggled; 

        static GetText()
        {
            CurrentLanguage = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "cs"
                ? Language.Czech
                : Language.English;
            G.ApplyCurrentLanguage();
        }

        public static Language CurrentLanguage { get; set; }

        public static T CzEn<T>(T czech, T english) => CurrentLanguage == Language.Czech ? czech : english;
    }
}