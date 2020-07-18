using System;
using System.Diagnostics;

namespace Tools
{
    public class ProcessRunner
    {
        public static bool RunProcess(string processName, string arguments)
        {
            Console.WriteLine($"- Running {processName} with arguments '{arguments}'"); 
            Process process = Process.Start(new ProcessStartInfo(processName, arguments)
            {
                RedirectStandardOutput = false,
                RedirectStandardError = false
            });
            process.WaitForExit();
            Console.WriteLine("[**] Process ended with exit code " + process.ExitCode + ".");
            return process.ExitCode == 0;
        }
    }
}