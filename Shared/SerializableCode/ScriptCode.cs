using System.Linq;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Stories;

namespace Nsnbc.SerializableCode
{
    public class ScriptCode : Code
    {
        public Script Script { get; }
        public ScriptCodeMode Mode { get; }

        public ScriptCode(Script script, ScriptCodeMode mode = ScriptCodeMode.Enqueue)
        {
            Script = script;
            Mode = mode;
        }

        public enum ScriptCodeMode
        {
            Enqueue,
            Replace
        }

        public override void Execute(CodeInput codeInput, AirSession airSession)
        {
            if (Mode == ScriptCodeMode.Replace)
            {
                airSession.Session.IncomingEvents.Clear();
                airSession.Session.CurrentLine.SpeakingText = null;
                airSession.ActiveActivities.RemoveAll(act => act is QSpeak.SpeakActivity);
            }
            airSession.Enqueue(Script);
        }
    }
}