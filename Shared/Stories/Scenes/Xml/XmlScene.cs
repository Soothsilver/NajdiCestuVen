using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Events;

namespace Nsnbc.Stories.Scenes.Xml
{
    [JsonObject(MemberSerialization.Fields)]
    public class XmlScene : Scene
    {
        public string? Name { get; set; }
        public ArtName MinimapLocal { get; set; }
        public List<XmlInteractible> Items = new List<XmlInteractible>();
        public List<XmlScene> Subscenes = new List<XmlScene>();
        public List<ArtName> Backgrounds { get; set; } = new List<ArtName>();

        public override void Begin(Session hardSession)
        {
            base.Begin(hardSession);
            if (EnterScript != null)
            {
                hardSession.QuickEnqueue(EnterScript);
            }
        }

        public override void Draw(AirSession airSession)
        {
            // Backgrounds
            foreach (ArtName background in Backgrounds)
            {
                Primitives.DrawZoomed(Library.Art(background), CurrentZoom);
            }

            // Items
            foreach (Interactible item in Interactibles)
            {
                if (item.VisualArt != ArtName.Null)
                {
                    Primitives.DrawZoomed(Library.Art(item.VisualArt), CurrentZoom);
                }
            }
            
            // Minimap
            if (this.MinimapLocal != ArtName.Null)
            {
                ArtName minimapBase = ArtName.Null;
                foreach (var scene in airSession.Session.SceneStack.Reverse<Scene>())
                {
                    if (scene.MinimapBase != ArtName.Null)
                    {
                        minimapBase = scene.MinimapBase;
                        break;
                    }
                }  
                Texture2D minimap = Library.Art(minimapBase);
                Primitives.DrawImage(minimap, new Rectangle(Root.Screen.Width - minimap.Width, Root.Screen.Height - minimap.Height, minimap.Width, minimap.Height), Color.White);
                Primitives.DrawImage(Library.Art(this.MinimapLocal), new Rectangle(Root.Screen.Width - minimap.Width, Root.Screen.Height - minimap.Height, minimap.Width, minimap.Height), Color.White);
            }
            
            DrawDirectionButton(ArtName.Left128Disabled, ArtName.Left128,
                new Rectangle(Root.Screen.Width/2-64-128, Root.Screen.Height-128,128,128), Directions.Left, airSession);
            DrawDirectionButton(ArtName.TurnAround128Disabled, ArtName.TurnAround128,
                new Rectangle(Root.Screen.Width / 2 - 64, Root.Screen.Height - 128, 128, 128), Directions.Turnaround, airSession);
            DrawDirectionButton(ArtName.Right128Disabled, ArtName.Right128,
                new Rectangle(Root.Screen.Width/2-64+128, Root.Screen.Height-128,128,128), Directions.Right, airSession);
        }

        private static void DrawDirectionButton(ArtName noMouseOver, ArtName mouseOver, Rectangle rectangle, DirectionButton directionButton, AirSession airSession)
        {
            if (directionButton != null)
            {
                Ux.DrawImageButton(noMouseOver, mouseOver, rectangle, () => airSession.Enqueue(directionButton.Script));
            }
        }

        public override bool DestroyInteractible(string name)
        {
            XmlInteractible? hereItem = Items.FirstOrDefault(item => item.Name == name);
            if (hereItem != null)
            {
                Items.Remove(hereItem);
                return true;
            }
            foreach (XmlScene subscene in Subscenes)
            {
                if (subscene.DestroyInteractible(name))
                {
                    return true;
                }
            }

            return false;
        }
        public override Interactible? FindInteractibleInThisScene(string name)
        {
            XmlInteractible? hereItem = Items.FirstOrDefault(item => item.Name == name);
            if (hereItem != null)
            {
                return hereItem;
            }
            foreach (XmlScene subscene in Subscenes)
            {
                Interactible? inThatScene = subscene.FindInteractibleInThisScene(name);
                if (inThatScene != null)
                {
                    return inThatScene;
                }
            }
            return null;
        }

        public override void AfterPop(AirSession airSession)
        {           
            airSession.Enqueue(new QZoomInto(CommonGame.R1920x1080, 0.1f));
        }
        
        public override Scene? FindExistingScene(string name)
        {
            foreach (XmlScene xmlScene in Subscenes)
            {
                if (xmlScene.Name == name)
                {
                    return xmlScene;
                }
            }
            return null;
        }

        public override IEnumerable<Interactible> Interactibles => Items;
        public Script EnterScript { get; set; }
    }
}