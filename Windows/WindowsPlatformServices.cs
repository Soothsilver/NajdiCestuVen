﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Nsnbc.Services;
using Nsnbc.Sounds;
using Nsnbc.Sounds.BassNet;
using Un4seen.Bass;

namespace Windows
{
    public class WindowsPlatformServices : IPlatformServices
    {
        public void OpenInBrowser(Uri uri)
        {
            Process.Start(uri.ToString());
        }

        public void ApplyFullscreenModeChanges()
        {
            WindowsGame.Instance.ApplyFullScreenModeChangesOnWindows();
        }

        public int LoadBassFileAsStream(string path)
        {
            return Bass.BASS_StreamCreateFile(path, 0, 0, BASSFlag.BASS_DEFAULT);
        }

        public LesserBass TheBass { get; } = new GreaterBass();
        public string[] FindAllPngFilesInAssets()
        {
            return Directory.EnumerateFiles("Art", "*.png", SearchOption.AllDirectories).ToArray();
        }

        public Stream LoadAssetFileAsStream(string filename)
        {
            return File.OpenRead(filename);
        }
    }
}