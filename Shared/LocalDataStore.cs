using System;
using System.IO;
using System.IO.IsolatedStorage;
using Nsnbc.PostSharp;

namespace Nsnbc
{
    [Trace]
    public static class LocalDataStore
    {
        private static IsolatedStorageFile store = null!;
        private const string IdentifierFileName = "identifier.txt";

        private static string identifier = "Unloaded";
        

        public static bool AutoMode { get; set; } = true;
        public static bool BeepingMode { get; set; } = true;
        
        

        public static void Init(IsolatedStorageFile storageFile)
        {
            store = storageFile;
            Read();
            if (identifier == "Unloaded" || identifier == "Error")
            {
                identifier = Guid.NewGuid().ToString();
                Write();
            }
        }

        private static void Read()
        {
            // open isolated storage, and write the savefile.
            if(store.FileExists(IdentifierFileName))
            {
                try
                {
                    IsolatedStorageFileStream fs = store.OpenFile(IdentifierFileName, FileMode.Open);


                    if (fs != null)
                    {
                        string fileContents;
                        using (StreamReader reader = new StreamReader(fs))
                        {
                            fileContents = reader.ReadToEnd();
                        }

                        identifier = fileContents;
                        fs.Close();
                    }
                }
                catch (Exception)
                {
                    // The file couldn't be opened, even though it's there.
                    // You can use this knowledge to display an error message
                    // for the user (beyond the scope of this example).
                    identifier = "Error";
                }
            }
        }

        private static void Write()
        {
            // open isolated storage, and write the savefile.
            try {
                IsolatedStorageFileStream fs = store.OpenFile(IdentifierFileName, FileMode.Create);
                fs.Write(System.Text.Encoding.UTF8.GetBytes(identifier), 0, identifier.Length);
                fs.Close();
            }
            catch (Exception)
            {
                // TODO we should notify the user that the game could not be saved at appropriate points (i.e. during true saving,
                // not during random confirm-saves
            }
        }
    }
}