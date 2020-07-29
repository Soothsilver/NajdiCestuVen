using System.Linq;
using Microsoft.Xna.Framework;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Phases;
using Nsnbc.SerializableCode;
using SharpDX.Direct3D11;

namespace Nsnbc.Stories.Scenes.Prison
{
    public class QKnownCodeEvent : QEvent
    {
        private readonly KnownCodes knownCode;
        private readonly CodeInput codeInput;

        public QKnownCodeEvent(KnownCodes knownCode, CodeInput codeInput)
        {
            this.knownCode = knownCode;
            this.codeInput = codeInput;
        }

        public override void Begin(AirSession airSession)
        {
            switch (knownCode)
            { 
                case KnownCodes.Detektor:
                    PrisonTableScene scene = airSession.ActiveScene as PrisonTableScene;
                    if (codeInput.InventoryItem.Art == ArtName.R1Baterie)
                    {
                        codeInput.HardSession.RemoveHeldItemFromInventory();
                        airSession.QuickSpeak("Nice.");
                        scene.BaterieIn = true;
                    }
                    else if (codeInput.InventoryItem.Art == ArtName.R1Disketa)
                    {
                        codeInput.HardSession.RemoveHeldItemFromInventory();
                        airSession.QuickSpeak("Nice.");
                        scene.DisketaIn = true;
                    }
                    else
                    {
                        
                        airSession.QuickSpeak("Ehh.");
                    }

                    if (scene.DisketaIn && scene.BaterieIn)
                    {
                        airSession.Enqueue(BookmarkId.R1_True_Victory);   
                    }

                    break;
                case KnownCodes.Led:
                    FridgeScene f1 = (codeInput.HardSession.ActiveScene as FridgeScene)!;
                    if (codeInput.InventoryItem.Art == ArtName.R1HrnecekSHorkouVodou)
                    {
                        airSession.QuickSpeak("Nice.");
                        f1.Led.Visible = false;
                        f1.Led.Rectangle = new Rectangle(0,0,0,0);
                        airSession.Session.Inventory.Add(new InventoryItem(ArtName.R1Baterie));
                    }
                    else
                    {
                        airSession.QuickSpeak("Um-um");
                    }

                    break;
                case KnownCodes.Dvere1:
                    PrisonR1 r1 = (codeInput.HardSession.ActiveScene as PrisonScene)!.Guardhouse1;
                    if (codeInput.InventoryItem.Art == ArtName.R1ZelenyKlic)
                    {
                        airSession.QuickSpeak("Nice.");
                        airSession.Session.RemoveHeldItemFromInventory();
                        r1.ZeleneDvere.FirstEncounter = r1.ZeleneDvere.SecondEncounter = new Script(BookmarkId.None, new QEvent[] { new QGoToRoom(r1.Parent.Guardhouse3) }); 
                    }
                    else
                    {
                        airSession.QuickSpeak("Um-um");
                    }
                    break;
                case KnownCodes.KamnaNahore:
                    PrisonR2 r2 = (codeInput.HardSession.ActiveScene as PrisonScene)!.Guardhouse2;
                    if (r2.FireExists)
                    {
                        if (codeInput.InventoryItem.Art == ArtName.R1HrnecekSVodou)
                        {
                            airSession.QuickEnqueue(new QReplaceInventoryItem(ArtName.R1HrnecekSVodou, ArtName.R1HrnecekSHorkouVodou));
                            airSession.QuickSpeak("Horko.");
                        }
                        else if (codeInput.InventoryItem.Art == ArtName.R1PrazdnyPapir)
                        {
                            
                            airSession.QuickEnqueue(new QReplaceInventoryItem(ArtName.R1PrazdnyPapir, ArtName.R1PapirSeStavou));
                            airSession.QuickSpeak("Horko.");
                        }
                        else
                        {
                            airSession.QuickSpeak("Horko!!!!");
                        }
                    }
                    else
                    {
                        airSession.QuickSpeak("No fire.");
                    }
                    break;
                case KnownCodes.Umyvadlo:
                    if (codeInput.InventoryItem.Art == ArtName.R1Hrnecek)
                    {
                        airSession.QuickEnqueue(
                            QSpeak.Quick("Blabla"),
                            new QReplaceInventoryItem(ArtName.R1Hrnecek, ArtName.R1HrnecekSVodou)
                        );
                        airSession.QuickSpeak("Blabla");
                    }
                    else
                    {
                        airSession.QuickSpeak("Haw-haw.");
                    }
                    break;
            }
        }
    }
}