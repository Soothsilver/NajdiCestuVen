using System;
using System.IO;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;
using Nsnbc.PostSharp;
using Nsnbc.Services;

namespace Nsnbc
{
    [Trace]
    public class AndroidDataStore
    {
        private const string settingsFilename = "settings.json";
        private readonly IsolatedStorageFile storageFile;

        public AndroidDataStore(IsolatedStorageFile storageFile)
        {
            this.storageFile = storageFile;
        }

        [LogAndSwallow]
        public Stream? ReadSettings()
        {
            // open isolated storage, and write the savefile.
            if (storageFile.FileExists(settingsFilename))
            {
                IsolatedStorageFileStream fs = storageFile.OpenFile(settingsFilename, FileMode.Open);
                if (fs != null)
                {
                    return fs;
                }
            }

            return null;
        }

        [LogAndSwallow]
        public void WriteSettings(Settings obj)
        {
            // open isolated storage, and write the savefile.

            using IsolatedStorageFileStream fs = storageFile.OpenFile(settingsFilename, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fs))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, obj);
            }

        }

        public Stream SaveFile(string filename)
        {
            return storageFile.OpenFile("Save" + filename, FileMode.Create);
        }

        public Stream LoadFile(string filename)
        {
            return storageFile.OpenFile("Save" + filename, FileMode.Open);
        }

        public string[] EnumerateFiles()
        {
            return storageFile.GetFileNames("Save*");
        }

        public DateTime GetLastWriteDate(string filename)
        {
            return storageFile.GetLastWriteTime(filename).LocalDateTime;
        }
    }
}