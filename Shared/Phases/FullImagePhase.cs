using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Auxiliary;

namespace Nsnbc.Phases
{
    internal class FullImagePhase : GamePhase
    {
        private readonly ArtName artName;
        private readonly Texture2D art;

        public FullImagePhase(ArtName artName)
        {
            this.artName = artName;
            art = Library.Art(artName);
        }

        protected internal override void Initialize(Game game)
        {
            Eqatec.Send("VIEW GALLERY ITEM " + artName);
            base.Initialize(game);
        }

        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {
            Primitives.FillRectangle(Root.Screen, Color.White);
            Primitives.DrawImage(art, Root.Screen, Color.White);
        }

        protected internal override void Update(Game game, float elapsedSeconds)
        {
            if (Root.WasMouseLeftClick || Root.WasMouseRightClick || Root.WasTouchReleased)
            {
                Root.PopFromPhase();
                Root.WasMouseLeftClick = false;
                Root.WasMouseRightClick = false;
                Root.WasTouchReleased = false;
                return;
            }
            base.Update(game, elapsedSeconds);
        }
    }
}