using System.IO;
using System.Xml.Linq;

namespace Nsnbc.Stories.Scenes.Xml
{
    public static  class XmlSceneLoader
    {
        public static Scene LoadDocument(Stream xStream)
        {
            return LoadDocument(XDocument.Load(xStream, LoadOptions.SetLineInfo));
        }
        public static  Scene LoadDocument(XDocument xDoc)
        {
            XmlSceneBuilder sceneBuilder = new XmlSceneBuilder();
            return sceneBuilder.Build(xDoc);
        }
    }
}