using System;
using System.IO;

namespace Tools
{
    public class Build
    {
        public static bool Execute()
        {
            CleanReleaseFolder();

            Console.WriteLine("[**] Building...");

            if (!ProcessRunner.RunProcess("msbuild",
                "Windows\\Windows.csproj /T:Clean,Restore,Build /V:m /P:Configuration=Release"))
            {
                Console.WriteLine("[**] Building Windows.csproj failed.");
                return false;
            }
            if (!ProcessRunner.RunProcess("msbuild",
                "Android\\Android.csproj /T:Restore,Rebuild /V:m /P:Configuration=Release"))
            {
                Console.WriteLine("[**] Building Android.csproj failed.");
                return false;
            }
            if (!ProcessRunner.RunProcess("msbuild",
                "Android\\Android.csproj /T:SignAndroidPackage /V:m /P:Configuration=Release"))
            {
                Console.WriteLine("[**] Signing the resulting Android package failed.");
                return false;
            }

            return true;
        }

        private static void CleanReleaseFolder()
        {
            Console.WriteLine("[**] Cleaning...");
            BestEffortDelete("Windows\\bin");
            BestEffortDelete("Android\\bin");
            BestEffortDelete("Android\\obj");
            try
            {
                if (Directory.Exists("Windows\\bin\\Release"))
                {
                    Directory.Delete("Windows\\bin\\Release", true);
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Warning -- failed to delete the Windows release folder.");
            }
        }

        private static void BestEffortDelete(string folderName)
        {
            try
            {
                if (Directory.Exists(folderName))
                {
                    Directory.Delete(folderName, true);
                }
            }
            catch (IOException)
            {
                Console.WriteLine("[WARNING] Warning! -- failed to delete the Windows folder " + folderName);
            }
        }
    }
}