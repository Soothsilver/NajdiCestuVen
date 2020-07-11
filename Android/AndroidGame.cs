using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Nsnbc.Android;
using Nsnbc.Auxiliary;

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
            LocalDataStore.Init(IsolatedStorageFile.GetUserStoreForApplication());
            
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
