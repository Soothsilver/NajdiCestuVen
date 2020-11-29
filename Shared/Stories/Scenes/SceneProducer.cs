using System;
using Nsnbc.Events;
using Nsnbc.Stories.Scenes.Xml;
using Nsnbc.Util;

namespace Nsnbc.Stories.Scenes
{
    public class SceneProducer
    {
        public static Scene CreateScene(SceneName name)
        {
            switch (name)
            {
                case SceneName.TechDemo:
                    return new TechDemoScene();
                case SceneName.PrisonXML:
                    return XmlSceneLoader.LoadDocument(ResourceUtility.GetEmbeddedResourceStream("Nsnbc.Stories.Scenes.Prison.Prison.xml"));
                case SceneName.CourtyardXML:
                    return XmlSceneLoader.LoadDocument(ResourceUtility.GetEmbeddedResourceStream("Nsnbc.Stories.Scenes.Courtyard.Courtyard.xml"));
                case SceneName.PrologueXML:
                    return XmlSceneLoader.LoadDocument(ResourceUtility.GetEmbeddedResourceStream("Nsnbc.Stories.Scenes.Prologue.Prologue.xml"));
                case SceneName.TechDemo_Trezor:
                    return new TrezorPuzzle();
                default:
                    throw new ArgumentException("Unknown scene name.");
            }
        }
    }
}