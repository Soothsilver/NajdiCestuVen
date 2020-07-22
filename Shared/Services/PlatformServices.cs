namespace Nsnbc
{
    public static class PlatformServices
    {
        public static IPlatformServices Services { get; set; } = null!;
        #if ANDROID
        public static Platform Platform = Platform.Android;
        #elif WINDOWS
        public static Platform Platform = Platform.Windows;
        #endif
    }

    public enum Platform
    {
        Windows,
        Android
    }
}