using System.IO;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;
using Nsnbc.PostSharp;

namespace Nsnbc
{
    [Trace]
    public class AndroidDataStore
    {
        private const string filename = "settings.json";
        private readonly IsolatedStorageFile storageFile;

        public AndroidDataStore(IsolatedStorageFile storageFile)
        {
            this.storageFile = storageFile;
        }

        [LogAndSwallow]
        public Stream? Read()
        {
            // open isolated storage, and write the savefile.
            if (storageFile.FileExists(filename))
            {
                IsolatedStorageFileStream fs = storageFile.OpenFile(filename, FileMode.Open);
                if (fs != null)
                {
                    return fs;
                }
            }

            return null;
        }

        [LogAndSwallow]
        public void Write(Settings obj)
        {
            // open isolated storage, and write the savefile.

            using IsolatedStorageFileStream fs = storageFile.OpenFile(filename, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fs))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, obj);
            }

        }
    }
}