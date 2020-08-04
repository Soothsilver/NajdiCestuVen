using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Nsnbc;
using Nsnbc.Auxiliary;
using Nsnbc.Services;

namespace Android
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class AndroidGame : CommonGame
    {
        public AndroidGame()
        {
            Graphics.IsFullScreen = true;
            TouchPanel.EnabledGestures = GestureType.None;
            Graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }
        
        protected override void LoadContent()
        {
            // Init data store:
            AndroidDataStore androidDataStore = new AndroidDataStore(IsolatedStorageFile.GetUserStoreForApplication());
            LocalDataStore.Init(androidDataStore.ReadSettings(), androidDataStore.WriteSettings);
            SaveLoad.Init(androidDataStore.SaveFile, androidDataStore.LoadFile, androidDataStore.EnumerateFiles, androidDataStore.GetLastWriteDate);

            // Identify self:
            Eqatec.Send("DEVICE ANDROID");
            
            // Common loading:
            base.LoadContent();
        }
        
        protected override void Update(GameTime gameTime)
        {
            Root.UpdateTouch();
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                Activity.MoveTaskToBack(true);
            }

            base.Update(gameTime);
        }
    }

}
