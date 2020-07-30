using System.Xml.Linq;

namespace Nsnbc.Stories.Scenes.Xml
{
    public static  class XmlSceneLoader
    {
        public static Scene LoadDocument(string filename)
        {
            return LoadDocument(XDocument.Load(filename, LoadOptions.SetLineInfo));
        }
        public static  Scene LoadDocument(XDocument xDoc)
        {
            XmlSceneBuilder sceneBuilder = new XmlSceneBuilder();
            return sceneBuilder.Build(xDoc);
        }
    }
}