using System;

namespace Nsnbc
{
    public static class GetText
    {
        public static void OnLanguageToggled()
        {
            G.ApplyCurrentLanguage();
            WhenLanguageToggled?.Invoke();
        }

        public static event Action? WhenLanguageToggled; 

        static GetText()
        {
            G.ApplyCurrentLanguage();
        }

        public static Language CurrentLanguage => Settings.Instance.Language;

        public static T CzEn<T>(T czech, T english) => CurrentLanguage == Language.Czech ? czech : english;
    }
}