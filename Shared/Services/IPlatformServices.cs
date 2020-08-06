using System;
using Nsnbc.Sounds;

namespace Nsnbc.Services
{
    public interface IPlatformServices
    {
        void OpenInBrowser(Uri uri);
        void ApplyFullscreenModeChanges();
        int LoadBassFileAsStream(string path);
        LesserBass TheBass { get; }
    }
}