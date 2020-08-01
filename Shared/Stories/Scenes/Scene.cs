using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.PostSharp;
using Nsnbc.SerializableCode;

namespace Nsnbc.Stories.Scenes
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public abstract class Scene
    {      
        public Rectangle CurrentZoom { get; set; }
        public Room? ActiveRoom;
        
        public virtual IEnumerable<Interactible> Interactibles => new Interactible[0];
        public virtual void Begin(Session hardSession)
        {
            CurrentZoom = CommonGame.R1920x1080;
        }

        public virtual Scene? FindExistingScene(SceneName name)
        {
            return null;
        }
        public virtual Scene? FindExistingScene(string name)
        {
            return null;
        }  
        public virtual Room? FindExistingRoom(string name)
        {
            return null;
        }

        [Trace(AttributeExclude = true)]
        public abstract void Draw(AirSession airSession);

        public virtual void AfterPop(AirSession airSession)
        {
        }
        
        /// <summary>
        /// Performs actions in response to a left-click somewhere on the screen while this is the active scene. Returns true if the click was handled by the scene.
        /// </summary>
        /// <param name="airSession">The current air-session.</param>
        /// <returns>True if this scene managed to use the click. False if other stuff should be given a chance to do something with the click.</returns>
        public virtual bool Click(AirSession airSession)
        {
            foreach (Interactible interactible in Interactibles)
            {
                if (Root.IsMouseOver(interactible.Rectangle))
                {
                    var fullResolution = CommonGame.R1920x1080;
                    if (airSession.Session.HeldItem == null)
                    {
                        airSession.Enqueue(new QZoomInto(interactible.Rectangle.Extend(100, 100), 0.1f));
                        airSession.Enqueue(new QWait(0.1f));
                        InteractibleEncounter encounter = (interactible.Interacted ? interactible.SecondEncounter! : interactible.FirstEncounter);
                        encounter.Enqueue(airSession);
                        interactible.Interacted = true;
                        airSession.Enqueue(new QZoomInto(fullResolution, 0.1f));
                        airSession.Enqueue(new QWait(0.1f));
                    }
                    else
                    {
                        if (interactible.OnItemUse != null)
                        {
                            airSession.Enqueue(new QZoomInto(interactible.Rectangle.Extend(100, 100), 0.1f));
                            airSession.Enqueue(new QWait(0.1f));
                            CodeInput codeInput = new CodeInput()
                            {
                                HardSession = airSession.Session,
                                InventoryItem = airSession.Session.HeldItem
                            };
                            interactible.OnItemUse.Execute(codeInput, airSession);
                            airSession.Enqueue(new QZoomInto(fullResolution, 0.1f));
                            airSession.Enqueue(new QWait(0.1f));
                        }
                        else
                        {
                            // TODO better text for other items
                            airSession.Enqueue(new QSpeak("", "Tenhle předmět s touhle věcí nemá co dělat.", ArtName.Null, SpeakerPosition.Left));
                        }
                    }

                    return true;
                    
                }
            }

            return false;
        }

        public virtual bool DestroyInteractible(string name)
        {
            return false;
        }

        public virtual Interactible? FindInteractibleInThisScene(string name)
        {
            return null;
        }
    }
}