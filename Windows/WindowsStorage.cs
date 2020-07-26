using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Nsnbc;
using Nsnbc.PostSharp;
using Nsnbc.Services;

namespace Windows
{
    [Trace]
    public static class WindowsStorage
    {
        private static readonly string settingsFilename =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NajdiCestuVen", "settings.json");
        private static readonly string appDataFolder =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NajdiCestuVen");
        
        [LogAndSwallow]
        public static Stream? ReadSettings()
        {
            if (File.Exists(settingsFilename))
            {
                return File.OpenRead(settingsFilename);
            }

            return null;
        }

        [LogAndSwallow]
        public static void SaveSettings(Settings obj)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(settingsFilename));
            using (StreamWriter writer = File.CreateText(settingsFilename))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, obj);
            }
        }

        public static Stream SaveFile(string filename)
        {
            string fullFilename = Path.Combine(appDataFolder, filename);
            Directory.CreateDirectory(Path.GetDirectoryName(fullFilename));
            return File.Create(fullFilename);
        }
        public static Stream LoadFile(string filename)
        {
            string fullFilename = Path.Combine(appDataFolder, filename);
            Directory.CreateDirectory(Path.GetDirectoryName(fullFilename));
            return File.OpenRead(fullFilename);
        }

        public static string[] EnumerateFiles(string dirName)
        {
            string fullFilename = Path.Combine(appDataFolder, dirName);
            return Directory.EnumerateFiles(fullFilename).Select(fl => fl.Substring(appDataFolder.Length + 1)).ToArray();
        }
    }
}