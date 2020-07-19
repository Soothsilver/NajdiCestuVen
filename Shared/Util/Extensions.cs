using System.Diagnostics.CodeAnalysis;

namespace Nsnbc
{
    public static class Extensions
    {
        public static bool IsNonBlank([NotNullWhen(true)] this string? text)
        {
            return !string.IsNullOrEmpty(text);
        }
    }
}