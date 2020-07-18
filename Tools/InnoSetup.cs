using System;

namespace Tools
{
    internal class InnoSetup
    {
        public static bool Compile(string outputDir)
        {
            Console.WriteLine("[**] Compiling Windows setup program.");
            if (ProcessRunner.RunProcess("iscc", $"/O\"{outputDir}\" Build\\EscapeTheStormSetup.iss"))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Creating the setup file failed.");
                return false;
            }
        }
    }
}