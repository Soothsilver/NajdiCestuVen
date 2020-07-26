using Microsoft.Xna.Framework;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.PostSharp;
using Nsnbc.Sounds;
using Nsnbc.Texts;

namespace Nsnbc.Stories
{
    [Trace]
    public class TrezorPuzzle
    {
        private string code = "";
        public void Begin(AirSession airSession)
        {
            // TODO fix this
            // airSession.PushZoom();
            // code = "";
            // airSession.CurrentZoom = CommonGame.R1920x1080;
            airSession.Session.CurrentLine.SpeakingText = null;
        }

        public void Draw(AirSession airSession)
        {
            // TODO fix this
            // Writer.DrawString(code, new Rectangle(102, 73, 637, 164), Color.DarkBlue, alignment: Writer.TextAlignment.Middle);
            // Ux.DrawButton(new Rectangle(80, 325, 700, 200), G.T("Chci nápovědu"), () =>
            // {
            //     if (airSession.IncomingEvents.Count != 0)
            //     {
            //         return;
            //     }
            //     airSession.Enqueue(StoryId.TrezorNapoveda);
            // }, alignment: Writer.TextAlignment.Middle);
            // Ux.DrawButton(new Rectangle(80, 550, 700, 200), G.T("Zpět"), () =>
            // { 
            //     if (airSession.IncomingEvents.Count != 0)
            //     {
            //         return;
            //     }
            //     airSession.ExitPuzzle();
            // }, alignment: Writer.TextAlignment.Middle);
            // int x = 0;
            // int y = 0;
            // for (int i = 1; i <= 9; i++)
            // {
            //     int j = i;
            //     Ux.DrawButton(new Rectangle(900 + x * 230, 50 + y * 230, 230, 230), i.ToString(), () =>
            //     {
            //         if (airSession.IncomingEvents.Count != 0)
            //         {
            //             return;
            //         }
            //
            //         Sfxs.Play(SoundEffectName.SfxNumber);
            //
            //         code += j.ToString();
            //         if (code.Length == 3)
            //         {
            //             if (code == "423")
            //             {
            //                 airSession.Enqueue(new QSfx(SoundEffectName.SfxTrezorOpen));
            //                 airSession.Enqueue(new QSpeak("", "Uslyšel jsem kovový zvuk a dveře se otevřely!", ArtName.Null, SpeakerPosition.Left));
            //                 airSession.Enqueue(new QAction(sss => code = G.T("otevřeno").ToString()));
            //                 airSession.Enqueue(new QSpeak("Vědátor", "Otevřel jsi trezor? Co je uvnitř?", ArtName.VedatorSpeaking, SpeakerPosition.Left));
            //                 airSession.Enqueue(new QSfx(SoundEffectName.SfxHarp));
            //                 airSession.Enqueue(new QSpeak("Tišík", "Je tu... klíč!", ArtName.TisikSpeaking, SpeakerPosition.Left));
            //                 airSession.Enqueue(new QSpeak("Vědátor", "Hah! To je lepší než zbraně!", ArtName.VedatorPointing, SpeakerPosition.Left));
            //                 airSession.Enqueue(new QAction(sss =>
            //                 {
            //                     airSession.Inventory.Add(new InventoryItem(ArtName.Key));
            //                     // TODO fix this
            //                     // airSession.OriginalScene!.TrezorOpen = true;
            //                     // airSession.OriginalScene.Trezor.SecondEncounter = G.T("Nic kromě klíče v trezoru nebylo.");
            //                     // airSession.OriginalScene.Door.SecondEncounter = G.T("Klikni na klíč a pak na dveře, abys je otevřel.");
            //                     airSession.ExitPuzzle();
            //                 }));
            //
            //
            //             }
            //             else
            //             {
            //                 airSession.Enqueue(new QSpeak("", "Dioda na trezoru zablikala, a trezor zůstal zamčený. Tento kód je nejspíš špatný.", ArtName.Null, SpeakerPosition.Left));
            //                 airSession.Enqueue(new QAction(sss=> code = ""));
            //             }
            //         }
            //
            //     }, alignment: Writer.TextAlignment.Middle);
            //     x += 1;
            //     if (x == 3)
            //     {
            //         x = 0;
            //         y++;
            //     }
            // }
        }

        public void Update()
        {
            
        }

        public void Exit(AirSession airSession)
        {
            // TODO fix this
            airSession.Enqueue(new QZoomInto(CommonGame.R1920x1080, 0.1f));
        }
    }
}