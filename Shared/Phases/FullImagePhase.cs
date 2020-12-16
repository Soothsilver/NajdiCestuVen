using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Auxiliary;

namespace Nsnbc.Phases
{
    internal class FullImagePhase : GamePhase
    {
        private Texture2D art;
        private int index;
        private readonly ArtName[] arts;

        public FullImagePhase(ArtName artName) : this(new ArtName[] { artName})
        {
        }
        public FullImagePhase(ArtName[] artNames)
        {
            this.arts = artNames;
            this.index = 0;
            art = Library.Art(artNames[0]);
        }

        protected internal override void Initialize(Game game)
        {
            Eqatec.Send("VIEW GALLERY ITEM " + arts[0]);
            base.Initialize(game);
        }

        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {
            Primitives.FillRectangle(Root.Screen, Color.White);
            Primitives.DrawImage(art, Root.Screen, Color.White, scale: true, scaleUp: true, scaleBgColor: Color.CornflowerBlue);
        }

        protected internal override void Update(Game game, float elapsedSeconds)
        {
            if (Root.WasMouseLeftClick || Root.WasTouchReleased)
            {
                index++;
                if (index == arts.Length)
                {
                    Root.PopFromPhase();
                }
                else
                {
                    this.art = Library.Art(arts[index]);
                }
                Root.WasMouseLeftClick = false;
                Root.WasTouchReleased = false;
            }
            else if (Root.WasMouseRightClick)
            {
                Root.PopFromPhase();
                Root.WasMouseRightClick = false;
            }
            base.Update(game, elapsedSeconds);
        }
    }
}