using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Tools
{
    internal static class Publisher
    {
        public static bool CreatedPackagedOutputs(string versionNumber)
        {
            string artifactsDirectory = "Build\\Output\\" + versionNumber;
            
            Console.WriteLine("[**] Creating output artifacts directory " + artifactsDirectory);
            Directory.CreateDirectory(artifactsDirectory);

            // Android .apk
            Console.WriteLine("[**] Copying Android .apk package");
            File.Copy("Android\\bin\\Release\\org.neocities.nsnbc.najdicestuven-Signed.apk",
                Path.Combine(artifactsDirectory, "NajdiCestuVen-Signed-" + versionNumber + ".apk"));
            
            // Inno Setup
            if (!InnoSetup.Compile(artifactsDirectory))
            {
                return false;
            }
            
            // Windows .zip
            Console.WriteLine("[**] Creating a ZIP file with the Windows installation");
            string startPath = "Windows\\bin\\Release";
            string zipPath = Path.Combine(artifactsDirectory, "EscapeTheStorms-" + versionNumber + ".zip");
            ZipFile.CreateFromDirectory(startPath, zipPath, CompressionLevel.Optimal, false, Encoding.UTF8);
            
            // All done
            return true;
        }
    }
}