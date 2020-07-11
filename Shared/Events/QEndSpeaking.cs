namespace Nsnbc.Events
{
    public class QEndSpeaking : QEvent
    {
        public override void Begin(Session session)
        {
            session.SpeakingText = null;
        }
    }
}