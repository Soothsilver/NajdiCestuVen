using Nsnbc.Core;
using Nsnbc.Stories;

namespace Nsnbc.SerializableCode
{
    public class ScriptCode : Code
    {
        public Script Script { get; }
        public ScriptCode(Script script)
        {
            Script = script;
        }

        public override void Execute(CodeInput codeInput)
        {
            codeInput.AirSession.Enqueue(Script);
        }
    }
}