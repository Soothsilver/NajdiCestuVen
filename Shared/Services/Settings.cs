using System;
using System.Globalization;
using Nsnbc.Auxiliary.Fonts;
using Nsnbc.PostSharp;
using Nsnbc.Sounds;
using Nsnbc.Texts;
using PostSharp.Aspects;

namespace Nsnbc.Services
{
    [Trace]
    public class Settings
    {
        public static Settings Instance { get; set; } = null!;

         // Display
         public FullScreenMode FullScreenMode { get; [ThenSave] set; } = FullScreenMode.Fullscreen;
         public float WindowOpacity { get; set; } = 1;
         public FontRenderStyle FontRenderStyle { get; set; } = FontRenderStyle.Xna;

         // Sound
         public bool PauseMusicWhileInactive { get; [ThenSave] set; } // TODO
         public float MasterVolume { get; set; } 
         public float MusicVolume { get; set; } 
         public float SfxVolume { get; set; }
         public float VoiceVolume { get; set; }
         public bool BeepUnvoicedLines { get; [ThenSave] set; }
         public bool UseVoices { get; [ThenSave] set; }
         
         // Language
         public Language Language { get; [ThenSave] set; }

         // Visual novel
         public bool AutoMode { get; [ThenSave] set; } = false;

         // Miscellaneous
         public bool ConfirmExitGame { get; [ThenSave] set; } = true; // TODO
         public string Identifier { get; [ThenSave] set; }

         [field:NonSerialized]
         public bool AllLoaded { get; set; }
         
         public int Opacity255 => (int) (255 * WindowOpacity);

         public Settings()
         {     
             Identifier = Guid.NewGuid().ToString();
             ResetSoundToDefaults();
             Language = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "cs"
                 ? Language.Czech
                 : Language.English;
         }

         public void ResetSoundToDefaults()
         {
             PauseMusicWhileInactive = true;
             MasterVolume = 1;
             MusicVolume = 0.2f;
             SfxVolume = 0.5f;
             UseVoices = true;
             VoiceVolume = 1;
             Sfxs.UpdateVolumes();
         }
    }

    [Serializable]
    public class ThenSaveAttribute : OnMethodBoundaryAspect
    {
        public override void OnSuccess(MethodExecutionArgs args)
        {
            if (((Settings)args.Instance).AllLoaded)
            {
                LocalDataStore.Write();
            }
        }
    }

    public enum FullScreenMode
    {
        Fullscreen,
        Windowed
    }
}