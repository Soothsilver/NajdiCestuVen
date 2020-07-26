using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Auxiliary;
using Nsnbc.Auxiliary.Fonts;
using Nsnbc.Texts;

namespace Nsnbc.Phases
{
    public class SavingInProgressPhase : GamePhase
    {
        private readonly Task task;
        private readonly Action thenWhat;

        public SavingInProgressPhase(Task task, Action thenWhat)
        {
            this.task = task;
            this.thenWhat = thenWhat;
        }

        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {
            Primitives.FillRectangle(Root.Screen, Color.Black.Alpha(200));
            Writer.DrawString(G.T("Ukládám..."), Root.Screen, Color.White, BitmapFontGroup.Main40, Writer.TextAlignment.Middle);
        }

        protected internal override void Update(Game game, float elapsedSeconds)
        {
            if (task.IsCompleted)
            {
                Root.PopFromPhase();
                thenWhat();
            }
            base.Update(game, elapsedSeconds);
        }
    }
}