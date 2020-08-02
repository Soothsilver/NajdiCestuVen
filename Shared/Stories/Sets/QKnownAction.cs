using System;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Stories.Scenes;
using Nsnbc.Stories.Scenes.Xml;
using Nsnbc.Texts;

namespace Nsnbc.Stories.Sets
{
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
                      airSession.Session.Inventory.Add(new InventoryItem(ArtName.Key));
                      (airSession.Session.SceneStack[0] as TechDemoScene)!.TrezorOpen = true; 
                      (airSession.Session.SceneStack[0] as TechDemoScene)!.Trezor.SecondEncounter = G.T("Nic kromě klíče v trezoru nebylo.");
                      (airSession.Session.SceneStack[0] as TechDemoScene)!.Door.SecondEncounter = G.T("Klikni na klíč a pak na dveře, abys je otevřel.");
                      break;
                case KnownAction.R1_SetMoveRight:
                    ((airSession.Session.ActiveScene as XmlScene)!.ActiveRoom! as XmlRoom)!.Directions.Right = new DirectionButton
                    {
                        Script = new Script
                        {
                            Events =
                            {
                                new QGoToRoom("Guardhouse3")
                            }
                        }
                    };
                    break;
                case KnownAction.R1_AddFire:
                    ((airSession.Session.ActiveScene as XmlScene)!.FindRoom("Guardhouse2") as XmlRoom)!.Backgrounds.Add(ArtName.R1Fire);
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
        R1_AddFire
    }
}