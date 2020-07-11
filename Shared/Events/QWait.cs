﻿using Auxiliary;
using Microsoft.Xna.Framework.Input;

namespace Nsnbc.Events
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

        public override void Begin(Session session)
        {
            session.ActiveActivities.Add(this);
        }

        public bool Blocking => true;
        public bool Dead => seconds <= 0 || ended;
        public void Update(Session session, float elapsedSeconds)
        {
            seconds -= elapsedSeconds;
            if ((Root.KeyboardNewState.IsKeyDown(Keys.Tab) || session.FastForwarding) && !unskippable)
            {
                if (seconds > 0.05f)
                {
                    seconds = 0.05f;
                }
            }
            else if (Root.KeyboardNewState.IsKeyDown(Keys.F1))
            {
                seconds = 0;
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