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
            Scene? scene = airSession.ActiveScene?.FindExistingScene(SceneName);
            airSession.Session.PushScene(scene ?? SceneProducer.CreateScene(SceneName));
        }
    }

    public enum SceneName
    {
        None,
        Prison,
        TechDemo,
        TechDemo_Trezor,
        R1_Table,
        R1_Fridge,
        R1_Suplik2
    }
}