using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Tools
{
    class Program
    {
        private static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            if (args.Length == 0)
            {
                Console.WriteLine("pot: Regenerate the .pot template file");
                Console.WriteLine("updateversion: Alter the code files to set the next patch version.");
                Console.WriteLine("package: Single command to create a signed publishable Android .apk a distributable Windows installer");
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
            else if (args[0] == "package")
            {
                UpdateVersion.Execute();
            }
            else
            {
               Console.WriteLine("Argument not recognized.");  
            }
        }
    }
}