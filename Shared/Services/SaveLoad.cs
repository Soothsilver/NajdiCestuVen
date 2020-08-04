using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nsnbc.Core;
using Nsnbc.PostSharp;
using Nsnbc.Texts;

namespace Nsnbc.Services
{
    [Trace]
    public static class SaveLoad
    {
        private static Func<string, Stream> saveFile = null!;
        private static Func<string, Stream> loadFile = null!;
        private static Func<string[]> enumerateSavesDirectory = null!;
        private static Func<string, DateTime> getLastWriteTime = null!;
        private static readonly JsonSerializer serializer = new JsonSerializer();

        public static void Init(Func<string, Stream> saveFileArg, Func<string, Stream> loadFileArg, Func<string[]> enumerateFiles, Func<string, DateTime> getLastWriteTimeArg)
        {
            saveFile = saveFileArg;
            loadFile = loadFileArg;
            enumerateSavesDirectory = enumerateFiles;
            getLastWriteTime = getLastWriteTimeArg;
            serializer.TypeNameHandling = TypeNameHandling.Objects;
            serializer.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            serializer.Formatting = Formatting.Indented;
            serializer.MissingMemberHandling = MissingMemberHandling.Error;
            serializer.Error += (sender, args) => throw args.ErrorContext.Error;
            serializer.TraceWriter = new JsonNetTraceWriter();
            serializer.Converters.Add(new StringEnumConverter());
        }
        
        [LogAndSwallow]
        public static void SaveGame(Session hardSession, Texture2D texture, int slotNumber)
        {
            using (StreamWriter writer = new StreamWriter(saveFile(slotNumber + ".json")))
            {
                serializer.Serialize(writer, new SavedGame(DateTime.Now.ToString("g"), hardSession));
            }

            Stream imgStream = saveFile(slotNumber + ".png");
            try
            {
                texture.SaveAsPng(imgStream, texture.Width, texture.Height);
            }
            finally
            {
                imgStream.Close();
            }
        }

        public static List<SavedGameWithScreenshot> GetSavedGames()
        {
            string[] filenames = enumerateSavesDirectory();
            List<SavedGameWithScreenshot> saves = new List<SavedGameWithScreenshot>();
            foreach (string filename in filenames)
            {
                if (filename.EndsWith(".json"))
                {
                    string simpleFilename = Path.GetFileNameWithoutExtension(filename);
                    int number;
                    if (simpleFilename.StartsWith("Save"))
                    {
                        number = Int32.Parse(simpleFilename.Substring(4));
                    }
                    else
                    {
                        number = Int32.Parse(simpleFilename);
                    }

                    try
                    {
                        {
                            DateTime lastWriteTime =getLastWriteTime(filename);

                            DelayedTexture screenshot;
                            try
                            {
                                screenshot = new DelayedTexture(() => loadFile(number + ".png"));
                            }
                            catch
                            {
                                screenshot = DelayedTexture.From(Library.Art(ArtName.SlotQuestion));
                            }

                            saves.Add(new SavedGameWithScreenshot(lastWriteTime.Year < 1970 ? G.T("[pozice]").ToString() : lastWriteTime.ToString("g"), number, screenshot));

                        }
                    }
                    catch (Exception ex)
                    {
                        Logs.Error("Failed to load a saved game", ex);
                        saves.Add(new SavedGameWithScreenshot(G.T("[poškozená pozice]").ToString(), number, DelayedTexture.From(Library.Art(ArtName.SlotQuestion))));
                    }
                }
            }
            return saves;
        }
        
        public static Session LoadGame(int slotNumber)
        { 
            using (StreamReader reader = new StreamReader(loadFile(slotNumber + ".json")))
            using (var jsonTextReader = new JsonTextReader(reader))
            {
                return serializer.Deserialize<SavedGame>(jsonTextReader)?.HardSession ?? throw new InvalidOperationException();
            }
        }
    }

    public class SavedGameWithScreenshot
    {
        public string Caption { get; }
        public int SlotNumber { get; }
        public DelayedTexture Screenshot { get; }

        public SavedGameWithScreenshot(string caption, int slotNumber, DelayedTexture screenshot)
        {
            Caption = caption;
            SlotNumber = slotNumber;
            Screenshot = screenshot;
        }
    }
}