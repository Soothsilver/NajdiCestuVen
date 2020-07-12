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
        }
    }
}