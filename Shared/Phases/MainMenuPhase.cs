﻿using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Android;
using Nsnbc.PostSharp;

namespace Nsnbc.Phases
{
    public class MainMenuPhase : GamePhase
    {
        private readonly MainMenu mainMenu = new MainMenu();
        [Trace]

        protected internal override void Initialize(Game game)
        {
            base.Initialize(game);
            Sfxs.BeginSong(Sfxs.MusicMenu);
            Eqatec.Send("ENTER MAIN MENU");
        }

        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {
            mainMenu.Draw();
        }

        protected internal override void Update(Game game, float elapsedSeconds)
        {
            if ((Root.WasMouseLeftClick || Root.WasTouchReleased))
            {
                Root.WasMouseLeftClick = false;
                Root.WasTouchReleased = false;
                if (Ux.MouseOverAction != null)
                {
                    Ux.MouseOverAction();
                }
            }
        }
    }
}