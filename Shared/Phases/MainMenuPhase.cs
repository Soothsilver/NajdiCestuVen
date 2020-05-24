using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Auxiliary;

namespace Nsnbc.Android
{
    public class MainMenuPhase : GamePhase
    {
        MainMenu MainMenu = new MainMenu();
        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {
            MainMenu.Draw();
        }

        protected internal override void Update(Game game, float elapsedSeconds)
        {
            if ((Root.WasMouseLeftClick || Root.WasTouchReleased))
            {
                Root.WasMouseLeftClick = false;
                Root.WasTouchReleased = false;
                if (UX.MouseOverAction != null)
                {
                    UX.MouseOverAction();
                }
            }
        }
    }
}