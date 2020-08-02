using System;
using System.Linq;
using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Stories.Scenes;

namespace Nsnbc.Events
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