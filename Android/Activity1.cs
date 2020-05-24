using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace Android
{
    [Activity(Label = "Najdi cestu ven!"
        , MainLauncher = true
        , Icon = "@drawable/icon192"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize | ConfigChanges.ScreenLayout)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        private View view;

        public Activity1()
        {
        }
        private void SetImmersive()
        {
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Kitkat)
                this.view.SystemUiVisibility =
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
            var g = new AndroidGame();
            View view = (View)g.Services.GetService(typeof(View));
            this.view = view;
            SetImmersive();
            SetContentView(view);
            g.Run();
        }
    }
}

