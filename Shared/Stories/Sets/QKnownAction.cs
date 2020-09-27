using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Stories.Scenes;
using Nsnbc.Stories.Scenes.Xml;
using Nsnbc.Texts;

namespace Nsnbc.Stories.Sets
{
    [JsonObject(MemberSerialization.Fields)] 

    public class QKnownAction : QEvent
    {
        public KnownAction ActionName { get; }

        public QKnownAction(KnownAction actionName)
        {
            ActionName = actionName;
        }

        public override void Begin(AirSession airSession)
        {
            switch (ActionName)
            {
                case KnownAction.ClearInventory:
                    airSession.Session.Inventory.Clear();
                    break;
                case KnownAction.SwitchToDrizzle:
                    (airSession.ActiveScene as TechDemoScene)!.CurrentBackground = ArtName.InteriorDrizzleRain;
                    break;
                case KnownAction.TechDemo_SetOtevreno:
                    (airSession.ActiveScene as TrezorPuzzle)!.Code = G.T("otevřeno").ToString();
                    break;
                case KnownAction.TechDemo_SetClear:
                    (airSession.ActiveScene as TrezorPuzzle)!.Code = "";
                    break;
                case KnownAction.TechDemo_GetKey:
                      airSession.Session.Inventory.Add(new InventoryItem(ArtName.Key, "Klíč vedoucí ven z místnosti!"));
                      (airSession.Session.SceneStack[0] as TechDemoScene)!.TrezorOpen = true; 
                      (airSession.Session.SceneStack[0] as TechDemoScene)!.Trezor.SecondEncounter = G.T("Nic kromě klíče v trezoru nebylo.");
                      (airSession.Session.SceneStack[0] as TechDemoScene)!.Door.SecondEncounter = G.T("Klikni na klíč a pak na dveře, abys je otevřel.");
                      break;
                case KnownAction.R1_SetMoveRight:
                    airSession.ActiveXmlScene.Directions.Right = new DirectionButton(new Script(new QGoToRoom("Guardhouse3")));
                    break;
                case KnownAction.R1_AddFire:
                    airSession.ActiveXmlScene.Backgrounds.Add(ArtName.R1Fire);
                    break;
                case KnownAction.R2_DropSword:
                    airSession.ActiveXmlScene.Backgrounds.Remove(ArtName.R2CourtSwordInHand);
                    airSession.ActiveXmlScene.Items.Add(new XmlInteractible()
                    {
                        Name = "fallenSword",
                        VisualArt = ArtName.R2CourtyardSwordFell,
                        Rectangle = new Rectangle(88,696,164,54),
                        FirstEncounter = new InteractibleEncounter()
                        {
                            Script = new Script()
                            {
                                Events =
                                {
                                    QSpeak.Quick("Kovový meč rytíře, který sloužil Blaersonovi. Teď je náš."),
                                    new QAddToInventory(ArtName.Sword128, "Meč brnění rytíře z nádvoří. Celý kovový."),
                                    new QDestroyInteractible("fallenSword")
                                }
                            }
                        }
                    });
                    break;
                default:
                    throw new ArgumentException("Unknown known action, heh.");
            }
        }
    }

    public enum KnownAction
    {
        ClearInventory,
        SwitchToDrizzle,
        TechDemo_SetOtevreno,
        TechDemo_GetKey,
        TechDemo_SetClear,
        R1_SetMoveRight,
        R1_AddFire,
        R2_DropSword,
    }
}