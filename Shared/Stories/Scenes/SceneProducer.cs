using System;
using Nsnbc.Events;

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
                default:
                    throw new ArgumentException("Unknown scene name.");
            }
        }
    }
}