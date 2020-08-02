using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.PostSharp;
using Nsnbc.Sounds;
using Nsnbc.Stories.Scenes;
using Nsnbc.Stories.Sets;
using Nsnbc.Texts;

namespace Nsnbc.Stories
{
    [Trace]
    [JsonObject(MemberSerialization.Fields)]
    public class TrezorPuzzle : Scene
    {
        public string Code { get; set; } = "";

        public override void Begin(Session hardSession)
        {
            base.Begin(hardSession);
            Code = "";
        }

        public override void Draw(AirSession airSession)
        {
            Primitives.DrawZoomed(Library.Art(ArtName.TrezorBackground), CurrentZoom);
            Writer.DrawString(Code, new Rectangle(102, 73, 637, 164), Color.DarkBlue, alignment: Writer.TextAlignment.Middle);
            Ux.DrawButton(new Rectangle(80, 325, 700, 200), G.T("Chci nápovědu"), () =>
            {
                if (airSession.Session.IncomingEvents.Count != 0)
                {
                    return;
                }
                airSession.Enqueue(BookmarkId.TrezorNapoveda);
            }, alignment: Writer.TextAlignment.Middle);
            Ux.DrawButton(new Rectangle(80, 550, 700, 200), G.T("Zpět"), () =>
            { 
                if (airSession.Session.IncomingEvents.Count != 0)
                {
                    return;
                }
                airSession.Enqueue(new QPopScene());
            }, alignment: Writer.TextAlignment.Middle);
            int x = 0;
            int y = 0;
            for (int i = 1; i <= 9; i++)
            {
                int j = i;
                Ux.DrawButton(new Rectangle(900 + x * 230, 50 + y * 230, 230, 230), i.ToString(), () =>
                {
                    if (airSession.Session.IncomingEvents.Count != 0)
                    {
                        return;
                    }
            
                    Sfxs.Play(SoundEffectName.SfxNumber);
            
                    Code += j.ToString();
                    if (Code.Length == 3)
                    {
                        if (Code == "423")
                        {
                            airSession.Enqueue(new QSfx(SoundEffectName.SfxTrezorOpen));
                            airSession.Enqueue(new QSpeak("", "Uslyšel jsem kovový zvuk a dveře se otevřely!", ArtName.Null, SpeakerPosition.Left));
                            airSession.Enqueue(new QKnownAction(KnownAction.TechDemo_SetOtevreno));
                            airSession.Enqueue(new QSpeak("Vědátor", "Otevřel jsi trezor? Co je uvnitř?", ArtName.VedatorSpeaking, SpeakerPosition.Left));
                            airSession.Enqueue(new QSfx(SoundEffectName.SfxHarp));
                            airSession.Enqueue(new QSpeak("Tišík", "Je tu... klíč!", ArtName.TisikSpeaking, SpeakerPosition.Left));
                            airSession.Enqueue(new QSpeak("Vědátor", "Hah! To je lepší než zbraně!", ArtName.VedatorPointing, SpeakerPosition.Left));
                            airSession.Enqueue(new QKnownAction(KnownAction.TechDemo_GetKey));
                            airSession.Enqueue(new QPopScene());

                        }
                        else
                        {
                            airSession.Enqueue(new QSpeak("", "Dioda na trezoru zablikala, a trezor zůstal zamčený. Tento kód je nejspíš špatný.", ArtName.Null, SpeakerPosition.Left));     
                            airSession.Enqueue(new QKnownAction(KnownAction.TechDemo_SetClear));

                        }
                    }
            
                }, alignment: Writer.TextAlignment.Middle);
                x += 1;
                if (x == 3)
                {
                    x = 0;
                    y++;
                }
            }
        }

        public override void AfterPop(AirSession airSession)
        {
            airSession.Enqueue(new QZoomInto(CommonGame.R1920x1080, 0.1f));
        }
    }
}