using System;
using Android.Content;
using Android.Provider;
using Nsnbc;

namespace Android
{
    public class AndroidPlatformServices : IPlatformServices
    {
        private readonly Activity1 activity1;

        public AndroidPlatformServices(Activity1 activity1)
        {
            this.activity1 = activity1;
        }
        public void OpenInBrowser(Uri uri)
        {
            var androidUri = Android.Net.Uri.Parse(uri.ToString());
            var intent = new Intent (Intent.ActionView, androidUri);
            activity1.StartActivity (intent);
        }

        public void ApplyFullscreenModeChanges()
        {
            // Does nothing
        }
    }
}