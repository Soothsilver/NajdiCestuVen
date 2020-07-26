using Nsnbc.Core;
using Nsnbc.Events;

namespace Nsnbc.Stories.Sets
{
    public class QEqatec : QEvent
    {
        public string EqatecString { get; }

        public QEqatec(string eqatecString)
        {
            EqatecString = eqatecString;
        }

        public override void Begin(AirSession airSession)
        {
            Eqatec.Send(EqatecString);
        }
    }
}