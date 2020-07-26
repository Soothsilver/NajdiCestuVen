using Newtonsoft.Json;
using Nsnbc.Core;

namespace Nsnbc.Services
{
    [JsonObject(MemberSerialization.OptOut)]
    public class SavedGame
    {
        public string Caption { get; }
        public Session HardSession { get; }

        public SavedGame(string caption, Session hardSession)
        {
            Caption = caption;
            HardSession = hardSession;
        }
    }
}