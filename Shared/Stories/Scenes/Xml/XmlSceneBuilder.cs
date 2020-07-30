using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Stories.Scenes.Prison;
using Nsnbc.Texts;
using PostSharp.Patterns.Diagnostics;

namespace Nsnbc.Stories.Scenes.Xml
{
    public class XmlSceneBuilder
    {
        private static readonly LogSource logSource = LogSource.Get();
        
        public Scene Build(XDocument xDoc)
        {
            XmlScene xmlScene = LoadScene(xDoc.Root);
            return xmlScene;
        }
        

        private XmlScene LoadScene(XElement xScene)
        {
            XmlScene scene = new XmlScene();
            scene.Name = xScene.Attribute("name")?.Value;
            scene.Minimap = xScene.Element("minimap").AsArt();
            foreach (XElement xArt in (xScene.Element("backgrounds")?.Elements("art")).AsNotNull())
            {
                scene.Backgrounds.Add(xArt.AsArt());
            }
            var xRooms = xScene.Element("rooms");
            foreach (XElement xRoom in (xRooms?.Elements("room")).AsNotNull())
            {
                Room room = LoadRoom(xRoom);
                scene.Rooms.Add(room);
                room.Parent = scene;
            }
            scene.Items = LoadInteractibles(xScene.Element("items")).Cast<XmlInteractible>().ToList<XmlInteractible>();
            scene.ActiveRoom = (xRooms?.Attribute("activeRoom") != null) ? scene.FindRoom(xRooms.Attribute("activeRoom").Value) : null;
            foreach (XElement xSubscene in (xScene.Element("scenes")?.Elements("scene")).AsNotNull())
            {
                scene.Subscenes.Add(LoadScene(xSubscene));
            }
            return scene;
        }

        private Room LoadRoom(XElement xRoom)
        {
            XmlRoom room = new XmlRoom();
            room.Name = xRoom.Attribute("name").Value;
            foreach (XElement xArt in (xRoom.Element("backgrounds")?.Elements("art")).AsNotNull())
            {
                room.Backgrounds.Add(xArt.AsArt());
            }

            LoadDirectionsInto(xRoom.Element("directions"), room);
            room.MinimapLocal = xRoom.Element("minimap").AsArt();
            room.Items = LoadInteractibles(xRoom.Element("items"));
            return room;
        }

        private List<Interactible> LoadInteractibles(XElement? xItems)
        {
            List<Interactible> items = new List<Interactible>();
            foreach (XElement xItem in (xItems?.Elements("interactible")).AsNotNull())
            {
                XmlInteractible item = new XmlInteractible();
                item.Name = xItem.Attribute("name").Value;
                item.Rectangle = xItem.Element("rectangle").AsRectangle();
                item.VisualArt = xItem.Attribute("art").AsArt();
                XElement first = xItem.Element("first");
                XElement? second = xItem.Element("second");
                item.FirstEncounter = LoadEncounter(first);
                item.SecondEncounter = second != null ? LoadEncounter(second) : null;
                items.Add(item);
            }
            return items;
        }

        private InteractibleEncounter LoadEncounter(XElement encounter)
        {
            if (encounter.Attribute("think") != null)
            {
                return new GString(encounter.Attribute("think").Value);
            }
            else
            {
                return LoadScript(encounter);
            }
        }

        private void LoadDirectionsInto(XElement? xDirections, XmlRoom room)
        {
            if (xDirections != null)
            {
                XElement? xTurnaround = xDirections.Element("turnaround");
                XElement? xLeft = xDirections.Element("left");
                XElement? xRight = xDirections.Element("right");
                if (xTurnaround != null)
                {
                    room.Directions.Turnaround = new DirectionButton { Script = LoadScript(xTurnaround) };
                }
                if (xLeft != null)
                {
                    room.Directions.Left = new DirectionButton { Script = LoadScript(xLeft) };
                }
                if (xRight != null)
                {
                    room.Directions.Right = new DirectionButton { Script = LoadScript(xRight) };
                }
            }
        }

        private Script LoadScript(XElement xScript)
        {
            Script script = new Script();
            foreach (XElement xLine in xScript.Elements())
            {
                script.Events.Add(LoadLine(xLine));
            }

            return script;
        }

        private QEvent LoadLine(XElement xLine)
        {
            switch (xLine.Name.LocalName)
            {
                case "goToRoom":
                    return new QGoToRoom(xLine.Value);
                case "pushScene":
                    return new QPushScene(xLine.Attribute("name").Value);
                case "think":
                    return new QSpeak("", xLine.Value, ArtName.Null, SpeakerPosition.Left);
                case "s":
                    string speaker = xLine.Attribute("speaker").Value;
                    string pose = xLine.Attribute("pose").Value;
                    string text = xLine.Value;
                    ArtName speakerArt = XmlCharacters.FindArt(speaker, pose);
                    return new QSpeak(speaker, text, speakerArt, SpeakerPosition.Left);
                case "addToInventory":
                    return new QAddToInventory(xLine.Attribute("art").AsArt());
                case "destroyInteractible":
                    return new QDestroyInteractible(xLine.Attribute("name").Value);
                case "removeHeldItem":
                    return new QRemoveHeldItem();
                case "setInteractibleFirstAndSecondUse":
                    return new QSetInteractibleFirstAndSecondUse(xLine.Attribute("interactible").Value, LoadScript(xLine));
                default:
                    logSource.Error.Write(FormattedMessageBuilder.Formatted("Script element {0} is not a recognized script element at line {1}", xLine.Name, ((IXmlLineInfo) xLine).LineNumber));
                    return new QNop();
            }
        }
    }

    internal class QSetInteractibleFirstAndSecondUse : QEvent
    {
        public string InteractibleName { get; }
        public Script Script { get; }

        public QSetInteractibleFirstAndSecondUse(string interactibleName, Script script)
        {
            InteractibleName = interactibleName;
            Script = script;
        }

        public override void Begin(AirSession airSession)
        {
            var interactible = airSession.Session.FindInteractible(InteractibleName)!;
            interactible.FirstEncounter = interactible.SecondEncounter = Script;
        }
    }
}