using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.Core;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    public class QWait : QEvent
    {
        private readonly float seconds;
        private readonly bool unskippable;

        public QWait(float seconds, bool unskippable = false)
        {
            this.seconds = seconds;
            this.unskippable = unskippable;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.ActiveActivities.Add(new WaitActivity(seconds, unskippable));
        }

        public override void FastForward(AirSession airSession)
        {
            // Skip waiting.
        }

        public class WaitActivity : IQActivity
        {
            private float secondsRemaining;
            private readonly bool unskippable;
            private bool ended;

            public WaitActivity(float secondsRemaining, bool unskippable)
            {
                this.secondsRemaining = secondsRemaining;
                this.unskippable = unskippable;
            }

            public bool Blocking => true;
            public bool Dead => secondsRemaining <= 0 || ended;
            public void Update(AirSession airSession, float elapsedSeconds)
            {
                secondsRemaining -= elapsedSeconds;
                if ((Root.KeyboardNewState.IsKeyDown(Keys.Tab) || airSession.FastForwarding) && !unskippable)
                {
                    if (secondsRemaining > 0.05f)
                    {
                        secondsRemaining = 0.05f;
                    }
                }
                else if (Root.KeyboardNewState.IsKeyDown(Keys.F1))
                {
                    secondsRemaining = 0;
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
}