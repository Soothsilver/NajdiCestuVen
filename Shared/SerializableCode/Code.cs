using Nsnbc.Core;

namespace Nsnbc.SerializableCode
{
    public abstract class Code
    {
        public abstract void Execute(CodeInput codeInput, AirSession airSession);
    }
}