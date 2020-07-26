using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Nsnbc.Services;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.Null;

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
    public class Activity1 : AndroidGameActivity
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
            // Must be done at earliest opportunity to distinguish from Windows:
            AndroidPlatformServices androidPlatformServices = new AndroidPlatformServices(this);
            PlatformServices.Initialize(androidPlatformServices, Platform.Android);
            
            AndroidGame g = new AndroidGame();
            LoggingServices.DefaultBackend = NullLoggingBackend.Instance;
            View theView = (View)g.Services.GetService(typeof(View));
            view = theView;
            SetImmersive();
            SetContentView(theView);
            g.Run();
        }
    }
}

