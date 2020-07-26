using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.PostSharp;
using Nsnbc.Stories;
using PostSharp.Patterns.Diagnostics;

namespace Nsnbc.Services
{
    [Trace]
    public static class SaveLoad
    {
        private static Func<string, Stream> saveFile = null!;
        private static Func<string, Stream> loadFile = null!;
        private static Func<string, string[]> enumerateDirectory = null!;
        private static readonly JsonSerializer serializer = new JsonSerializer();

        public static void Init(Func<string, Stream> saveFileArg, Func<string, Stream> loadFileArg, Func<string, string[]> enumerateFiles)
        {
            saveFile = saveFileArg;
            loadFile = loadFileArg;
            enumerateDirectory = enumerateFiles;
            serializer.TypeNameHandling = TypeNameHandling.Objects;
            serializer.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            serializer.Formatting = Formatting.Indented;
            serializer.Error += (sender, args) => throw args.ErrorContext.Error;
            serializer.TraceWriter = new MyTraceWriter();
            serializer.Converters.Add(new StringEnumConverter());
        }
        
        [LogAndSwallow]
        public static void SaveGame(Session hardSession, Texture2D texture, int slotNumber)
        {
            using (StreamWriter writer = new StreamWriter(saveFile("Saves/" + slotNumber + ".json")))
            {
                serializer.Serialize(writer, new SavedGame(DateTime.Now.ToString("g"), hardSession));
            }
            texture.SaveAsPng(saveFile("Saves/" + slotNumber + ".png"), texture.Width, texture.Height);
        }

        public static List<SavedGameWithScreenshot> GetSavedGames()
        {
            string[] filenames = enumerateDirectory("Saves");
            List<SavedGameWithScreenshot> saves = new List<SavedGameWithScreenshot>();
            foreach (string filename in filenames)
            {
                if (filename.EndsWith(".json"))
                {
                    using (StreamReader reader = new StreamReader(loadFile(filename)))
                    using (var jsonTextReader = new JsonTextReader(reader))
                    {
                        SavedGame savedGame =  serializer.Deserialize<SavedGame>(jsonTextReader)!;
                        string simpleFilename = Path.GetFileNameWithoutExtension(filename);
                        int number = Int32.Parse(simpleFilename);
                        Texture2D screenshot;
                        try
                        {
                            screenshot = Texture2D.FromStream(Root.Graphics.GraphicsDevice, loadFile("Saves/" + simpleFilename + ".png"));
                        }
                        catch
                        {
                            screenshot = Library.Art(ArtName.Pixel);
                        }

                        saves.Add(new SavedGameWithScreenshot(savedGame, number, screenshot));
                    }
                }
            }
            return saves;
        }
        
        public static Session LoadGame(int slotNumber)
        { 
            using (StreamReader reader = new StreamReader(loadFile("Saves/" + slotNumber + ".json")))
            using (var jsonTextReader = new JsonTextReader(reader))
            {
                return serializer.Deserialize<SavedGame>(jsonTextReader)?.HardSession ?? throw new InvalidOperationException();
            }
        }
    }

    public class MyTraceWriter : ITraceWriter
    {
        LogSource logSource = LogSource.Get();
        public void Trace(TraceLevel level, string message, Exception? ex)
        {
            logSource.Info.Write(FormattedMessageBuilder.Formatted("SERIALIZATION: " + level.ToString() +": " + message + ex?.ToString()));
            Debug.WriteLine(message);
            Console.WriteLine(message);
        }

        public TraceLevel LevelFilter => TraceLevel.Verbose;
    }

    public class SavedGameWithScreenshot
    {
        public SavedGame SavedGame { get; }
        public int SlotNumber { get; }
        public Texture2D Screenshot { get; }

        public SavedGameWithScreenshot(SavedGame savedGame, int slotNumber, Texture2D screenshot)
        {
            SavedGame = savedGame;
            SlotNumber = slotNumber;
            Screenshot = screenshot;
        }
    }
}