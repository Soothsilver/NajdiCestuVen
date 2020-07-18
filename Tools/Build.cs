using System;
using System.Diagnostics;

namespace Tools
{
    public class Build
    {
        public static bool Execute()
        {
            Console.WriteLine("[**] Building...");

            if (!ProcessRunner.RunProcess("msbuild",
                "Windows\\Windows.csproj /T:Restore,Build /P:Configuration=Release"))
            {
                Console.WriteLine("[**] Building Windows.csproj failed.");
                return false;
            }
            if (!ProcessRunner.RunProcess("msbuild",
                "Android\\Android.csproj /T:Restore,Build,SignAndroidPackage /P:Configuration=Release"))
            {
                Console.WriteLine("[**] Building Android.csproj failed.");
                return false;
            }

            return true;
        }
    }
}