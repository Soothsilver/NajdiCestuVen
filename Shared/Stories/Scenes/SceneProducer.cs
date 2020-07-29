using System;
using Nsnbc.Events;
using Nsnbc.Stories.Scenes.Prison;

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
                case SceneName.Prison:
                    return new PrisonScene();
                case SceneName.TechDemo_Trezor:
                    return new TrezorPuzzle();
                default:
                    throw new ArgumentException("Unknown scene name.");
            }
        }
    }
}