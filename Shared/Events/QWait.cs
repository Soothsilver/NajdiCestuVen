using Microsoft.Xna.Framework.Input;
using Nsnbc.Auxiliary;

namespace Nsnbc.Android.Stories
{
    public class QWait : QEvent, IQActivity
    {
        private float seconds;
        private readonly bool unskippable;
        private bool ended;

        public QWait(float seconds, bool unskippable = false)
        {
            this.seconds = seconds;
            this.unskippable = unskippable;
        }

        public override void Begin(TSession session)
        {
            session.ActiveActities.Add(this);
        }

        public bool Blocking => true;
        public bool Dead => seconds <= 0 || ended;
        public void Run(TSession session, float elapsedSeconds)
        {
            seconds -= elapsedSeconds;
            if ((Root.Keyboard_NewState.IsKeyDown(Keys.Tab) || session.FastForwarding) && !unskippable)
            {
                if (seconds > 0.05f)
                {
                    seconds = 0.05f;
                }
            }
            if (Root.WasTouchReleased || Root.WasMouseLeftClick)
            {
                ended = true;
                Root.WasTouchReleased = false;
                Root.WasMouseLeftClick = false;
            }
        }
    }
}