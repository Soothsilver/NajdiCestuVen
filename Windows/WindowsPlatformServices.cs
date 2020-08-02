using System;
using System.Diagnostics;
using Nsnbc.Services;

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
    }
}