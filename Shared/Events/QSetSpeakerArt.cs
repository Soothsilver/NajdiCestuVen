﻿namespace Nsnbc.Android.Stories
{
    public class QSetSpeakerArt : QEvent
    {
        private readonly ArtName speaker;
        private readonly SpeakerPosition left;

        public QSetSpeakerArt(ArtName speaker, SpeakerPosition left)
        {
            this.speaker = speaker;
            this.left = left;
        }

        public override void Begin(TSession session)
        {
            if (left == SpeakerPosition.Left)
            {
                session.SpeakerLeft = speaker;
            }
            else
            {
                session.SpeakerRight = speaker;
            }
        }
    }
}