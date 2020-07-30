using System;
using System.Linq;
using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Events;

namespace Nsnbc.Stories.Scenes.Xml
{
    [JsonObject(MemberSerialization.Fields)]
    internal class QDestroyInteractible : QEvent
    {
        public string Name { get; }

        public QDestroyInteractible(string name)
        {
            Name = name;
        }

        public override void Begin(AirSession airSession)
        {
            foreach (Scene scene in airSession.Session.SceneStack.Reverse<Scene>())
            {
                if (scene.DestroyInteractible(Name))
                {
                    return;
                }
            }
            throw new InvalidOperationException("Interactible '" + Name + "' was not found.");
        }
    }
}