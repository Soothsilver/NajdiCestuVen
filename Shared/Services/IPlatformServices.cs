using System;

namespace Nsnbc.Services
{
    public interface IPlatformServices
    {
        void OpenInBrowser(Uri uri);
        void ApplyFullscreenModeChanges();
    }
}