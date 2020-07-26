using Nsnbc.Core;

namespace Nsnbc.Events
{
    public class QEndSpeaking : QEvent
    {
        public override void Begin(AirSession airSession)
        {
            airSession.Session.CurrentLine.SpeakingText = null;
        }
    }
}