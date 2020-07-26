using System;
using System.IO;
using Newtonsoft.Json;
using Nsnbc.PostSharp;
using PostSharp.Patterns.Diagnostics;

namespace Nsnbc.Services
{
    [Trace]
    public static class LocalDataStore
    {
        private static readonly LogSource logSource = LogSource.Get();
        private static Action<Settings> _saveSettings = null!;
        
        public static void Init(Stream? stream, Action<Settings> saveSettings)
        {
            _saveSettings = saveSettings;
            if (stream == null)
            {
                Settings.Instance = new Settings();
            }
            else
            {
                try
                {
                    var deserializer = new JsonSerializer();
                    using (var sr = new StreamReader(stream))
                    using (var jsonTextReader = new JsonTextReader(sr))
                    {
                        Settings.Instance = deserializer.Deserialize<Settings>(jsonTextReader) ?? new Settings();
                    }
                }
                catch (Exception ex)
                {
                    logSource.Error.Write(FormattedMessageBuilder.Formatted("Settings could not be loaded."), ex);
                    Settings.Instance = new Settings();
                }
                finally
                {
                    stream.Dispose();
                }
            }

            Settings.Instance.AllLoaded = true;
        }

        public static void Write()
        {
            _saveSettings(Settings.Instance);
        }
    }
}