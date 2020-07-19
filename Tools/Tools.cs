using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    static class Program
    {
        private static async Task Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            if (args.Length == 0)
            {
                Console.WriteLine("pot: Regenerate the .pot template file");
                Console.WriteLine("inno: Compile the Inno setup program");
                Console.WriteLine("updateversion: Alter the code files to set the next patch version.");
                Console.WriteLine("package: Single command to create a signed publishable Android .apk a distributable Windows installer");
                Console.WriteLine("github: Create a new release on GitHub from existing packaged files");
                Console.WriteLine("puregithub: Create a new release on GitHub, but don't Git commit first");
                Console.WriteLine("publish: Single command to create a new release on GitHub");
            }
            if (args[0] == "pot")
            {
                foreach (string filename in Directory.EnumerateFiles(".", "*.cs", SearchOption.AllDirectories))
                {
                    sb.AppendLine(Path.GetFullPath(filename));
                }
                File.WriteAllText("~allCSharpFiles.txt", sb.ToString());
                Process xGetText = Process.Start("Extended\\GetText\\xgettext.exe",
                    "-kT -kQSpeak:2 -kTn --from-code=utf-8 --package-name=NajdiCestuVen --package-version=2.0 -o Shared\\Texts\\NajdiCestuVen.pot --files-from=~allCSharpFiles.txt");
                xGetText.WaitForExit();
                Console.WriteLine(".pot creation exit code: " + xGetText.ExitCode);
            }
            else if (args[0] == "updateversion")
            {
                UpdateVersion.Execute();
            }
            else if (args[0] == "inno")
            {
                string artifactsDirectory = "Build\\Output\\" + UpdateVersion.ReadVersion();
                InnoSetup.Compile(artifactsDirectory);
            }
            else if (args[0] == "package")
            {
                string packagedVersion = UpdateVersion.Execute();
                if (!CreateArtifacts(packagedVersion))
                {
                    return;
                }
                Console.WriteLine("[**] All done.");
            }
            else if (args[0] == "github")
            {
                string version = UpdateVersion.ReadVersion();
                await GitHub.CompleteCreateNewRelease(version);
                Console.WriteLine("[**] All done.");
            }  
            else if (args[0] == "puregithub")
            {
                string version = UpdateVersion.ReadVersion();
                await GitHub.UploadToGitHub(version);
                Console.WriteLine("[**] All done.");
            }
            else if (args[0] == "publish")
            {
                string packagedVersion = UpdateVersion.Execute();
                if (!CreateArtifacts(packagedVersion))
                {
                    return;
                }
                await GitHub.CompleteCreateNewRelease(packagedVersion);
                Console.WriteLine("[**] All done.");
            }
            else
            {
               Console.WriteLine("Argument not recognized.");  
            }
        }

        private static bool CreateArtifacts(string packagedVersion)
        {
            if (!Build.Execute())
            {
                return false;
            }

            if (!Publisher.CreatedPackagedOutputs(packagedVersion))
            {
                return false;
            }

            return true;
        }
    }
}