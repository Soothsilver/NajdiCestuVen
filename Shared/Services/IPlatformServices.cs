using System;
using System.IO;
using Nsnbc.Sounds;

namespace Nsnbc.Services
{
    public interface IPlatformServices
    {
        void OpenInBrowser(Uri uri);
        void ApplyFullscreenModeChanges();
        int LoadBassFileAsStream(string path);
        LesserBass TheBass { get; }
        string[] FindAllPngFilesInAssets();
        Stream LoadAssetFileAsStream(string filename);
    }
}