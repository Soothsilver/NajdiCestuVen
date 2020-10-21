using Nsnbc.Core;
using Nsnbc.Visiting;

namespace Nsnbc.SerializableCode
{
    public abstract class Code
    {
        public abstract void Execute(CodeInput codeInput, AirSession airSession);

        public abstract void Accept(Visitor visitor);
    }
}