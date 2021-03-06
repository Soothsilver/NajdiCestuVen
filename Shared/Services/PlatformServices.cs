﻿namespace Nsnbc.Services
{
    public static class PlatformServices
    {
        public static IPlatformServices Services { get; private set; } = null!;
        public static Platform Platform { get; private set; } = Platform.Windows;
        public static bool HideExperimentalFeatures = false;

        public static void Initialize(IPlatformServices platformServices, Platform platform)
        {
            Services = platformServices;
            Platform = platform;
        }
    }

    public enum Platform
    {
        Windows,
        Android
    }
}