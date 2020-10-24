using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.PostSharp;
using Nsnbc.Stories.Sets;
using Nsnbc.Texts;

namespace Nsnbc.Stories.Scenes.Xml
{
    public class XmlSceneBuilder
    {
        public Scene Build(XDocument xDoc)
        {
            XmlScene xmlScene = LoadScene(xDoc.Root);
            return xmlScene;
        }
        

        private XmlScene LoadScene(XElement xScene)
        {
            string? type = xScene.Attribute("type")?.Value;
            XmlScene scene = (XmlScene) (type != null ? Activator.CreateInstance(typeof(Scene).Assembly.GetType(type)) : new XmlScene());
            scene.Name = xScene.Attribute("name")?.Value;
            scene.MinimapBase = xScene.Element("minimapBase").AsArt();
            scene.MinimapLocal = xScene.Element("minimap").AsArt();
            foreach (XElement xArt in (xScene.Element("backgrounds")?.Elements("art")).AsNotNull())
            {
                scene.Backgrounds.Add(xArt.AsArt());
            }
            LoadDirectionsInto(xScene.Element("directions"), scene);
            scene.Items = LoadInteractibles(xScene.Element("items")).Cast<XmlInteractible>().ToList();
            foreach (XElement xSubscene in (xScene.Element("scenes")?.Elements("scene")).AsNotNull())
            {
                scene.Subscenes.Add(LoadScene(xSubscene));
            }

            if (xScene.Element("enter") != null)
            {
                scene.EnterScript = LoadScript(xScene.Element("enter"));
            }

            if (xScene.Attribute("escapeToTurnaround")?.Value == "true")
            {
                scene.EscapeToTurnaround = true;
            }

            XElement xStorage = xScene.Element("storage");
            if (xStorage != null)
            {
                foreach (XElement xScript in xStorage.Elements("script"))
                {
                    string scriptName = xScript.Attribute("name").Value;
                    scene.StorageScripts.Add(scriptName, LoadScript(xScript));
                }
            }
            return scene;
        }
        
        private List<Interactible> LoadInteractibles(XElement? xItems)
        {
            List<Interactible> items = new List<Interactible>();
            foreach (XElement xItem in (xItems?.Elements("interactible")).AsNotNull())
            {
                var item = LoadInteractible(xItem);
                items.Add(item);
            }
            return items;
        }

        private XmlInteractible LoadInteractible(XElement xItem)
        {
            XmlInteractible item = new XmlInteractible();
            item.Name = xItem.Attribute("name").Value;
            item.Rectangle = xItem.Element("rectangle").AsRectangle();
            item.VisualArt = xItem.Attribute("art").AsArt();
            XElement first = xItem.Element("first");
            XElement? second = xItem.Element("second");
            item.FirstEncounter = LoadEncounter(first);
            item.SecondEncounter = second != null ? LoadEncounter(second) : null;
            XElement xItemuse = xItem.Element("itemuse");
            if (xItemuse != null)
            {
                item.OnItemUse = LoadItemUseCode(xItemuse);
            }
            return item;
        }

        private ItemUseCode LoadItemUseCode(XElement xItemuse)
        {
            ItemUseCode itemUseCode = new ItemUseCode();
            foreach (XElement xWithItem in xItemuse.Elements("withItem"))
            {
                ArtName art = xWithItem.Attribute("art").AsArt();
                itemUseCode.AddResponse(art, LoadScript(xWithItem));
            }

            if (xItemuse.Attribute("failure") != null)
            {
                itemUseCode.AddDefault(xItemuse.Attribute("failure").Value);
            }
            else if (xItemuse.Element("failure") != null)
            {
                itemUseCode.AddDefault(LoadScript(xItemuse.Element("failure")));
            }
            else
            {
                itemUseCode.AddDefault(G.T("Tenhle předmět s tou věcí nesouvisí.").ToString());
            }
            return itemUseCode;
        }

        private InteractibleEncounter LoadEncounter(XElement encounter)
        {
            if (encounter.Attribute("think") != null)
            {
                return new GString(encounter.Attribute("think").Value.Replace("…","..."));
            }

            return LoadScript(encounter);
        }

        private void LoadDirectionsInto(XElement? xDirections, XmlScene scene)
        {
            if (xDirections != null)
            {
                XElement? xTurnaround = xDirections.Element("turnaround");
                XElement? xLeft = xDirections.Element("left");
                XElement? xRight = xDirections.Element("right");
                if (xTurnaround != null)
                {
                    scene.Directions.Turnaround = new DirectionButton(LoadScript(xTurnaround));
                }
                if (xLeft != null)
                {
                    scene.Directions.Left = new DirectionButton(LoadScript(xLeft));
                }
                if (xRight != null)
                {
                    scene.Directions.Right = new DirectionButton(LoadScript(xRight));
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
                    return new QAddToInventory(xLine.Attribute("art").AsArt(), xLine.Attribute("description").Value);
                case "destroyInteractible":
                    return new QDestroyInteractible(xLine.Attribute("name").Value);
                case "removeHeldItem":
                    return new QRemoveHeldItem();
                case "setInteractibleFirstAndSecondUse":
                    return new QSetInteractibleFirstAndSecondUse(xLine.Attribute("interactible").Value, LoadScript(xLine));
                case "setInteractibleArt":
                    return new QSetInteractibleArt(xLine.Attribute("interactible").Value, xLine.Attribute("art").AsArt());
                case "popScene":
                    return new QPopScene();
                case "pushFullArtScene":
                    return new QSetBackground(xLine.Attribute("art").AsArt());
                case "enqueue":
                    return new QEnqueue(xLine.Attribute("bookmark").AsBookmarkId());
                case "setFlag":
                    return new QSetFlag(xLine.Attribute("flag").Value);
                case "ifFlag":
                    return new QIfFlag(xLine.Attribute("flag").Value,
                        LoadScript(xLine.Element("then")),
                        LoadScript(xLine.Element("else"))
                    );
                case "knownAction":
                    return new QKnownAction(xLine.Attribute("action").AsEnum<KnownAction>());
                case "replaceHeldItem":
                    return new QReplaceInventoryItem(xLine.Attribute("with").AsArt(), xLine.Attribute("withDescription").Value);
                case "removeBackgroundArt":
                    return new QRemoveBackgroundArt(xLine.Attribute("art").AsArt());
                case "addBackgroundArt":
                    return new QAddBackgroundArt(xLine.Attribute("art").AsArt());
                default:
                    Logs.Error($"Script element {xLine.Name} is not a recognized script element at line {((IXmlLineInfo) xLine).LineNumber}");
                    return new QNop();
            }
        }
    }

    [JsonObject(MemberSerialization.Fields)]
    internal class QAddBackgroundArt : QEvent
    {
        private readonly ArtName art;

        public QAddBackgroundArt(ArtName art)
        {
            this.art = art;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.ActiveXmlScene.Backgrounds.Add(art);
        }
    }
    [JsonObject(MemberSerialization.Fields)]
    internal class QRemoveBackgroundArt : QEvent
    {
        private readonly ArtName art;

        public QRemoveBackgroundArt(ArtName art)
        {
            this.art = art;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.ActiveXmlScene.Backgrounds.Remove(art);
        }
    }
}