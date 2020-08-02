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
            SceneName = sceneName;
        }
        public QPushScene(string sceneName)
        {
            SceneNameAsString = sceneName;
        }

        public override void Begin(AirSession airSession)
        {
            airSession.ActiveActivities.RemoveAll(ac => ac is QZoomInto.ZoomActivity);
            Scene? scene = airSession.ActiveScene?.FindExistingScene(SceneName);
            scene ??= airSession.Session.FindExistingScene(SceneNameAsString);
            airSession.Session.PushScene(scene ?? SceneProducer.CreateScene(SceneName));
        }
    }

    /// <summary>
    /// These names are <b>not</b> used from XML, they're for major sections available from the scene loader or legacy.
    /// </summary>
    public enum SceneName
    {
        None,
        TechDemo,
        TechDemo_Trezor,
        PrisonXML
    }
}