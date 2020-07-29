using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.SerializableCode;

namespace Nsnbc.Stories.Scenes.Prison
{
    public class KnownCode : Code
    {
        public KnownCodes Code { get; }

        public KnownCode(KnownCodes code)
        {
            Code = code;
        }

        public override void Execute(CodeInput codeInput, AirSession airSession)
        {
            switch (Code)
            {
                case KnownCodes.KamnaDole:
                    KamnaDole(codeInput, airSession);
                    break;
                default:
                    airSession.Enqueue(new QKnownCodeEvent(Code, codeInput));
                    break;
            }
        }

        private void KamnaDole(CodeInput codeInput, AirSession airSession)
        {
            PrisonR2 r2 = (airSession.ActiveScene as PrisonScene)!.Guardhouse2;
            if (codeInput.InventoryItem.Art == ArtName.R1Triska)
            {
                r2.Trisek++;
                codeInput.HardSession.RemoveHeldItemFromInventory();
                airSession.Enqueue(QSpeak.Quick("Nyni je tam " + r2.Trisek + " trisek."));
                if (r2.Trisek == 3)
                {
                    r2.FireExists = true;
                    airSession.Enqueue(QSpeak.Quick("Nyni je tam " + r2.Trisek + " trisek. Nice!!"));
                }
            }
            else
            {
                airSession.Enqueue(QSpeak.Quick("Nope."));
            }
        }
    }
}