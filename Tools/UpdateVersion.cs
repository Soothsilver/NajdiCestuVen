using System;
using System.Linq;
using System.Xml.Linq;

namespace Tools
{
    public class UpdateVersion
    {
        public static void Execute()
        {
            string gameVersionFileName = "Build\\GameVersion.props";
            XDocument xDocument = XDocument.Load(gameVersionFileName);
            XElement xGameVersion = xDocument.Descendants("GameVersion").First();
            Version currentVersion = Version.Parse(xGameVersion.Value);
            Version updatedVersion = new Version(currentVersion.Major, currentVersion.Minor,
                currentVersion.Revision + 1, currentVersion.Build);
            
            // Update main version file
            xGameVersion.Value = updatedVersion.ToString(3);
            xDocument.Save(gameVersionFileName);
            
            // Update android manifest
            string manifestFileName = "Android\\Properties\\AndroidManifest.xml";
            XDocument xManifest = XDocument.Load(manifestFileName);
            XAttribute versionCode =
                xManifest.Root.Attribute(XName.Get("versionCode", "http://schemas.android.com/apk/res/android"));
            XAttribute versionName =
                xManifest.Root.Attribute(XName.Get("versionName", "http://schemas.android.com/apk/res/android"));
            versionCode.Value = (int.Parse(versionCode.Value) + 1).ToString();
            versionName.Value = updatedVersion.ToString(3);
            xManifest.Save(manifestFileName);
        }
    }
}