using Newtonsoft.Json;
using Nsnbc.Core;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)] 

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