using System;
using System.IO;
using Auxiliary;
using Newtonsoft.Json;
using Nsnbc;
using Nsnbc.PostSharp;

namespace Windows
{
    [Trace]
    public static class WindowsStorage
    {
        private static string filename =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NajdiCestuVen", "settings.json");
        
        [LogAndSwallow]
        public static Stream? ReadSettings()
        {
            if (File.Exists(filename))
            {
                return File.OpenRead(filename);
            }
            else
            {
                return null;
            }
        }

        [LogAndSwallow]
        public static void SaveSettings(Settings obj)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filename));
            using (StreamWriter writer = File.CreateText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, obj);
            }
        }
    }
}