using System;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Stories.Scenes;

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
                    airSession.Inventory.Clear();
                    break;
                case KnownAction.SwitchToDrizzle:
                    (airSession.ActiveScene as TechDemoScene)!.CurrentBackground = ArtName.InteriorDrizzleRain;
                    break;
                default:
                    throw new ArgumentException("Unknown known action, heh.");
            }
        }
    }

    public enum KnownAction
    {
        ClearInventory,
        SwitchToDrizzle
    }
}