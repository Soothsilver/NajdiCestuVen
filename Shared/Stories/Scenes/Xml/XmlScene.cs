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
    public class XmlScene : Scene, IRoomOrScene
    {
        public string? Name { get; set; }
        public ArtName Minimap { get; set; }
        public List<Room> Rooms { get; set; } = new List<Room>();
        public List<XmlInteractible> Items = new List<XmlInteractible>();
        public List<XmlScene> Subscenes = new List<XmlScene>();
        public List<ArtName> Backgrounds { get; set; } = new List<ArtName>();
        public Directions Directions { get; set; } = new Directions();

        public override void Draw(AirSession airSession)
        {
            // Backgrounds
            foreach (ArtName background in Backgrounds)
            {
                Primitives.DrawZoomed(Library.Art(background), CurrentZoom);
            }
            
            // Room
            ActiveRoom?.Draw(airSession);

            // Items
            foreach (Interactible item in Interactibles)
            {
                if (item.VisualArt != ArtName.Null)
                {
                    Primitives.DrawZoomed(Library.Art(item.VisualArt), CurrentZoom);
                }
            }
            
            // Minimap
            if (ActiveRoom != null)
            {
                Texture2D minimap = Library.Art(Minimap);
                Primitives.DrawImage(minimap, new Rectangle(Root.Screen.Width - minimap.Width, Root.Screen.Height - minimap.Height, minimap.Width, minimap.Height), Color.White);
                Primitives.DrawImage(Library.Art(ActiveRoom.YouAreHere), new Rectangle(Root.Screen.Width - minimap.Width, Root.Screen.Height - minimap.Height, minimap.Width, minimap.Height), Color.White);
            }
            
            XmlRoom.DrawDirectionButton(ArtName.Left128Disabled, ArtName.Left128,
                new Rectangle(Root.Screen.Width/2-64-128, Root.Screen.Height-128,128,128), Directions.Left, airSession);
            XmlRoom.DrawDirectionButton(ArtName.TurnAround128Disabled, ArtName.TurnAround128,
                new Rectangle(Root.Screen.Width / 2 - 64, Root.Screen.Height - 128, 128, 128), Directions.Turnaround, airSession);
            XmlRoom.DrawDirectionButton(ArtName.Right128Disabled, ArtName.Right128,
                new Rectangle(Root.Screen.Width/2-64+128, Root.Screen.Height-128,128,128), Directions.Right, airSession);
        }

        public override bool DestroyInteractible(string name)
        {
            XmlInteractible? hereItem = Items.FirstOrDefault(item => item.Name == name);
            if (hereItem != null)
            {
                Items.Remove(hereItem);
                return true;
            }
            XmlInteractible? activeRoomItem = ActiveRoom?.Items.OfType<XmlInteractible>().FirstOrDefault(item => item.Name == name);
            if (activeRoomItem != null)
            {
                ActiveRoom!.Items.Remove(activeRoomItem);
                return true;
            }

            foreach (Room room in Rooms)
            {
                XmlInteractible? roomItem = room.Items.OfType<XmlInteractible>().FirstOrDefault(item => item.Name == name);
                if (roomItem != null)
                {
                    room.Items.Remove(roomItem);
                    return true;
                }
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
            XmlInteractible? activeRoomItem = ActiveRoom?.Items.OfType<XmlInteractible>().FirstOrDefault(item => item.Name == name);
            if (activeRoomItem != null)
            {
                return activeRoomItem;
            }

            foreach (Room room in Rooms)
            {
                XmlInteractible? roomItem = room.Items.OfType<XmlInteractible>().FirstOrDefault(item => item.Name == name);
                if (roomItem != null)
                {
                    return roomItem;
                }
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

        public Room FindRoom(string roomName)
        {
            foreach (Room room in Rooms)
            {
                if (room.Name == roomName)
                {
                    return room;
                }
            }
            throw new ArgumentException("Room '" + roomName + "' does not exist.");
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

        public override Room? FindExistingRoom(string name)
        {
            foreach (XmlScene xmlScene in Subscenes)
            {
                Room? r = xmlScene.FindExistingRoom(name);
                if (r != null)
                {
                    return r;
                }
            }

            var room = Rooms.FirstOrDefault(rm => rm.Name == name);
            return room;
        }

        public override IEnumerable<Interactible> Interactibles => ActiveRoom != null ? ActiveRoom.Items.Cast<XmlInteractible>() : Items;
    }
}