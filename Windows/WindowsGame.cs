using System;
using System.Linq;
using System.Windows.Forms;
using Nsnbc;
using Nsnbc.Auxiliary;
using Nsnbc.PostSharp;
using Nsnbc.Services;
using Nsnbc.Texts;

namespace Windows
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    [Trace]
    public class WindowsGame : CommonGame
    {
        private Resolution screenResolution = new Resolution(1,1);
        private readonly Form form;
        public static WindowsGame Instance = null!;

        public WindowsGame()
        {
            // Accessibilize:
            Instance = this;
            
            // The first thing must be reading settings and language:
            WindowsPlatformServices windowsPlatformServices = new WindowsPlatformServices();
            PlatformServices.Initialize(windowsPlatformServices, Platform.Windows);
            LocalDataStore.Init(WindowsStorage.ReadSettings(), WindowsStorage.SaveSettings);
         
            // Now we can use language:
            SetTitle();
            Nsnbc.Texts.GetText.WhenLanguageToggled += SetTitle;
            IsMouseVisible = true;
            
            // Go to borderless window (part 1):
            IntPtr hWnd = Window.Handle;
            var control = Control.FromHandle(hWnd);
            form = control.FindForm();
            
        }

        private void SetTitle()
        {
            Window.Title = G.T("Najdi cestu ven!").ToString();
        }

        protected override void LoadContent()
        {
            // Go to borderless window (part 2):
            ApplyFullScreenModeChangesOnWindows();

            // Identify self:
            Eqatec.Send("DEVICE WINDOWS");
            
            // Common loading:
            base.LoadContent();
        }

        public void ApplyFullScreenModeChangesOnWindows()
        {
            if (Settings.Instance.FullScreenMode == FullScreenMode.Fullscreen)
            {
                GoToBorderlessWindowed();
            }
            else
            {
                GoToNormalWindow();
            }
            ResetViewport();
        }

        private void GoToNormalWindow()
        {
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            screenResolution = new Resolution(1920,1080);
            form.Width = screenResolution.Width;
            form.Height = screenResolution.Height;
            form.WindowState = FormWindowState.Normal;
            Graphics.PreferredBackBufferWidth = screenResolution.Width;
            Graphics.PreferredBackBufferHeight = screenResolution.Height;
            Graphics.ApplyChanges();
        }

        private void GoToBorderlessWindowed()
        {
            form.FormBorderStyle = FormBorderStyle.None;
            screenResolution = Utilities.GetSupportedResolutions().Last();
            form.Width = screenResolution.Width;
            form.Height = screenResolution.Height;
            form.WindowState = FormWindowState.Maximized;
            Graphics.PreferredBackBufferWidth = screenResolution.Width;
            Graphics.PreferredBackBufferHeight = screenResolution.Height;
            Graphics.ApplyChanges();
        }
    }
}
