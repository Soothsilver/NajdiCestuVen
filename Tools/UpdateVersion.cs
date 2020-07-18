using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Tools
{
    public class UpdateVersion
    {
        public static string Execute()
        {
            Console.Write("[**] Updating version number");
            string gameVersionFileName = "Build\\GameVersion.props";
            XDocument xDocument = XDocument.Load(gameVersionFileName);
            XElement xGameVersion = xDocument.Descendants("GameVersion").First();
            Version currentVersion = Version.Parse(xGameVersion.Value);
            Version updatedVersion = new Version(currentVersion.Major, currentVersion.Minor,
                currentVersion.Build + 1, 0);
            Console.WriteLine(" to " + updatedVersion.ToString(3) + ".");

            // Update main version file
            xGameVersion.Value = updatedVersion.ToString(3);
            xDocument.Save(gameVersionFileName);
            
            // Update android manifest
            Console.Write("[**] Updating Android manifest code");
            string manifestFileName = "Android\\Properties\\AndroidManifest.xml";
            XDocument xManifest = XDocument.Load(manifestFileName);
            XAttribute versionCode =
                xManifest.Root.Attribute(XName.Get("versionCode", "http://schemas.android.com/apk/res/android"));
            XAttribute versionName =
                xManifest.Root.Attribute(XName.Get("versionName", "http://schemas.android.com/apk/res/android"));
            versionCode.Value = (int.Parse(versionCode.Value) + 1).ToString();
            versionName.Value = updatedVersion.ToString(3);
            xManifest.Save(manifestFileName);
            Console.WriteLine(" to " +versionCode.Value + ".");
            
            // Update Windows readme
            UpdateReadmeFile(updatedVersion, "Windows\\README.txt");
            UpdateReadmeFile(updatedVersion, "Windows\\ČTIMĚ.txt");

            return updatedVersion.ToString(3);
        }

        private static void UpdateReadmeFile(Version updatedVersion, string filename)
        {
            Console.Write($"[**] Updating Windows readme file ({filename}) to {updatedVersion.ToString(3)}.");
            string windowsReadmeText = File.ReadAllText(filename, Encoding.UTF8);
            File.WriteAllText(filename, windowsReadmeText.Replace("[[VERSION]]", updatedVersion.ToString(3)),
                Encoding.UTF8);
        }
    }
}