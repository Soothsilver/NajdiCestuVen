using System;
using System.IO;
using System.Runtime.InteropServices;
using Android.Content;
using Android.Content.Res;
using Nsnbc.Services;
using Nsnbc.Sounds;
using Nsnbc.Sounds.BassNet;
using Un4seen.Bass;

namespace Android
{
    public class AndroidPlatformServices : IPlatformServices
    {
        private readonly Activity1 activity1;
        private readonly AssetManager assets;

        public AndroidPlatformServices(Activity1 activity1)
        {
           this.activity1 = activity1;
           assets = activity1.Assets;
        }


        public void OpenInBrowser(Uri uri)
        {
            var androidUri = Net.Uri.Parse(uri.ToString());
            var intent = new Intent (Intent.ActionView, androidUri);
            activity1.StartActivity (intent);
        }

        public void ApplyFullscreenModeChanges()
        {
            // Does nothing
        }

        public int LoadBassFileAsStream(string filename)
        {
            Stream ioStream = assets.Open(filename.Replace("\\", "/"));
            var full = ReadFully(ioStream);
            GCHandle _hGCFile = GCHandle.Alloc(full, GCHandleType.Pinned);
            int stream = Bass.BASS_StreamCreateFile(_hGCFile.AddrOfPinnedObject(), 0, full.Length, BASSFlag.BASS_DEFAULT);
            return stream;
        }

        public LesserBass TheBass { get; } = new GreaterBass();

        private static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}