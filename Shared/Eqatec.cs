using System;
using System.Net;
using System.Threading.Tasks;

namespace Nsnbc.Android
{
    public class Eqatec
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
                    
                }
            });
        }
    }
}