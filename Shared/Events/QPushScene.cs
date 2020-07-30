using System;
using Newtonsoft.Json;
using Nsnbc.Core;
using Nsnbc.Stories.Scenes;

namespace Nsnbc.Events
{
    [JsonObject(MemberSerialization.Fields)]
    public class QPushScene : QEvent
    {
        public SceneName SceneName { get; set; }
        public string SceneNameAsString { get; set; }

        [JsonConstructor]
        public QPushScene()
        {
        }
        
        public QPushScene(SceneName sceneName)
        {
            this.SceneName = sceneName;
        }
        public QPushScene(string sceneName)
        {
            this.SceneNameAsString = sceneName;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.ActiveActivities.RemoveAll(ac => ac is QZoomInto.ZoomActivity);
            Scene? scene = airSession.ActiveScene?.FindExistingScene(SceneName);
            scene ??= airSession.ActiveScene?.FindExistingScene(SceneNameAsString);
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
        R1_Suplik2,
        PrisonXML
    }
}