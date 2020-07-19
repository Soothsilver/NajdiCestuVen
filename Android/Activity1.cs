using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using JetBrains.Annotations;
using Nsnbc;
using Nsnbc.PostSharp;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.Trace;

namespace Android
{
    [Activity(Label = "Najdi cestu ven!"
        , MainLauncher = true
        , Icon = "@drawable/icon192"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize | ConfigChanges.ScreenLayout)]
    [UsedImplicitly]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        private View view;

        private void SetImmersive()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
                view.SystemUiVisibility =
                    (StatusBarVisibility)(SystemUiFlags.LayoutStable | SystemUiFlags.LayoutHideNavigation | SystemUiFlags.LayoutFullscreen | SystemUiFlags.HideNavigation | SystemUiFlags.Fullscreen | SystemUiFlags.ImmersiveSticky);
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);

            if (hasFocus)
                SetImmersive();
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            AndroidPlatformServices androidPlatformServices = new AndroidPlatformServices(this);
            PlatformServices.Services = androidPlatformServices;
            AndroidGame g = new AndroidGame();
            LoggingServices.DefaultBackend = new TraceLoggingBackend();
            View theView = (View)g.Services.GetService(typeof(View));
            view = theView;
            SetImmersive();
            SetContentView(theView);
            g.Run();
        }
    }
}

