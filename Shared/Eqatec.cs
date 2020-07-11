using System;
using System.Net;
using System.Threading.Tasks;
using Nsnbc.PostSharp;
using PostSharp.Patterns.Diagnostics;

namespace Nsnbc
{
    /// <summary>
    /// Sends usage data to the NSNBC server.
    /// </summary>
    [Trace]
    public static class Eqatec
    {
        public static void Send(string data)
        {
            Task.Run(() =>
            {
                try
                {
                    WebClient wc = new WebClient();
                    // wc.DownloadString("http://najdicestuven.wz.cz/eqatec.php?identifier=" + LocalDataStore.Identifier + "&text=" + data);
                }
                catch (Exception)
                {
                    // If sending data fails, perhaps we don't have internet access, that's fine, just don't interrupt gameplay.
                }
            });
        }
    }
}