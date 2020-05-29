using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace Nsnbc.Android
{
    public static class LocalDataStore
    {
        private static IsolatedStorageFile store;
        private const string IdentifierFileName = "identifier.txt";

        public static string Identifier = "Unloaded";

        static LocalDataStore()
        {
          
        }

        public static void Init(IsolatedStorageFile store)
        {
            LocalDataStore.store = store;
            Read();
            if (Identifier == "Unloaded" || Identifier == "Error")
            {
                Identifier = Guid.NewGuid().ToString();
                Write();
            }
        }

        public static void Read()
        {
            // open isolated storage, and write the savefile.
            if(store.FileExists(IdentifierFileName))
            {
                IsolatedStorageFileStream fs = null;
                try
                {
                    fs = store.OpenFile(IdentifierFileName, System.IO.FileMode.Open);


                    if (fs != null)
                    {
                        string fileContents;
                        using (StreamReader reader = new StreamReader(fs))
                        {
                            fileContents = reader.ReadToEnd();
                        }

                        Identifier = fileContents;
                        fs.Close();
                    }
                }
                catch (Exception e)
                {
                    // The file couldn't be opened, even though it's there.
                    // You can use this knowledge to display an error message
                    // for the user (beyond the scope of this example).
                    Identifier = "Error";
                }
            }
            else
            {
                
            }
        }

        public static void Write()
        {
            // open isolated storage, and write the savefile.
            try {
                IsolatedStorageFileStream fs = null;
                fs = store.OpenFile(IdentifierFileName, System.IO.FileMode.Create);
                fs.Write(System.Text.Encoding.UTF8.GetBytes(Identifier), 0, Identifier.Length);
                fs.Close();
            }
            catch (Exception e)
            {
            }
        }
    }
}