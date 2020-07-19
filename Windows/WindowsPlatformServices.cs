using System;
using System.Diagnostics;
using Nsnbc;

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