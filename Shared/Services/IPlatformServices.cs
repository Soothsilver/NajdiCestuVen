using System;

namespace Nsnbc
{
    public interface IPlatformServices
    {
        void OpenInBrowser(Uri uri);
        void ApplyFullscreenModeChanges();
    }
}