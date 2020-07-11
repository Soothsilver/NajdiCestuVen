using System;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows.Forms;
using Auxiliary;
using Nsnbc;
using Nsnbc.PostSharp;


namespace Windows
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    [Trace]
    public class WindowsGame : CommonGame
    {
        private readonly Resolution screenResolution;

        public WindowsGame()
        {
            Window.Title = "Najdi cestu ven!";
            IsMouseVisible = true;
            
            // Go to borderless window (part 1):
            IntPtr hWnd = Window.Handle;
            var control = Control.FromHandle(hWnd);
            Form form1 = control.FindForm();
            form1.FormBorderStyle = FormBorderStyle.None;
            screenResolution = Utilities.GetSupportedResolutions().Last();
            form1.Width = screenResolution.Width;
            form1.Height = screenResolution.Height;
            form1.WindowState = FormWindowState.Maximized;
        }
        
        protected override void LoadContent()
        {
            // Go to borderless window (part 2):
            Graphics.PreferredBackBufferWidth = screenResolution.Width;
            Graphics.PreferredBackBufferHeight = screenResolution.Height;
            Graphics.ApplyChanges();
            
            // Init data store:
            LocalDataStore.Init(IsolatedStorageFile.GetUserStoreForDomain());
            
            // Identify self:
            Eqatec.Send("DEVICE WINDOWS");
            
            // Common loading:
            base.LoadContent();
        }
    }
}
