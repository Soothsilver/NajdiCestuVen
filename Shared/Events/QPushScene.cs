using System;
using Nsnbc.Core;
using Nsnbc.Stories.Scenes;

namespace Nsnbc.Events
{
    public class QPushScene : QEvent
    {
        public SceneName SceneName { get; }
        
        public QPushScene(SceneName sceneName)
        {
            this.SceneName = sceneName;
        }
        
        public override void Begin(AirSession airSession)
        {
            airSession.ActiveActivities.RemoveAll(ac => ac is QZoomInto.ZoomActivity);
            airSession.Session.PushScene(SceneProducer.CreateScene(SceneName));
        }
    }

    public enum SceneName
    {
        None,
        Prison,
        TechDemo
    }
}