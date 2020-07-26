using Auxiliary;
using Microsoft.Xna.Framework;
using Nsnbc.Events;
using Nsnbc.PostSharp;

namespace Nsnbc.Stories
{
    [Trace]
    public class TrezorPuzzle
    {
        private string code = "";
        public void Begin(Session session)
        {
            session.PushZoom();
            code = "";
            session.Background = ArtName.TrezorBackground;
            session.CurrentZoom = session.FullResolution;
            session.SpeakingText = null;
        }

        public void Draw(Session session)
        {
            Writer.DrawString(code, new Rectangle(102, 73, 637, 164), Color.DarkBlue, alignment: Writer.TextAlignment.Middle);
            Ux.DrawButton(new Rectangle(80, 325, 700, 200), G.T("Chci nápovědu"), () =>
            {
                if (session.IncomingEvents.Count != 0)
                {
                    return;
                }
                session.Enqueue(StoryId.TrezorNapoveda);
            }, alignment: Writer.TextAlignment.Middle);
            Ux.DrawButton(new Rectangle(80, 550, 700, 200), G.T("Zpět"), () =>
            { 
                if (session.IncomingEvents.Count != 0)
                {
                    return;
                }
                session.ExitPuzzle();
            }, alignment: Writer.TextAlignment.Middle);
            int x = 0;
            int y = 0;
            for (int i = 1; i <= 9; i++)
            {
                int j = i;
                Ux.DrawButton(new Rectangle(900 + x * 230, 50 + y * 230, 230, 230), i.ToString(), () =>
                {
                    if (session.IncomingEvents.Count != 0)
                    {
                        return;
                    }

                    Sfxs.Play(Sfxs.SfxNumber);

                    code += j.ToString();
                    if (code.Length == 3)
                    {
                        if (code == "423")
                        {
                            session.Enqueue(new QSfx(Sfxs.SfxTrezorOpen));
                            session.Enqueue(new QSpeak("", "Uslyšel jsem kovový zvuk a dveře se otevřely!", ArtName.Null, SpeakerPosition.Left));
                            session.Enqueue(new QAction(sss => code = G.T("otevřeno").ToString()));
                            session.Enqueue(new QSpeak("Vědátor", "Otevřel jsi trezor? Co je uvnitř?", ArtName.VedatorSpeaking, SpeakerPosition.Left));
                            session.Enqueue(new QSfx(Sfxs.SfxHarp));
                            session.Enqueue(new QSpeak("Tišík", "Je tu... klíč!", ArtName.TisikSpeaking, SpeakerPosition.Left));
                            session.Enqueue(new QSpeak("Vědátor", "Hah! To je lepší než zbraně!", ArtName.VedatorPointing, SpeakerPosition.Left));
                            session.Enqueue(new QAction(sss =>
                            {
                                session.Inventory.Add(new InventoryItem(ArtName.Key));
                                session.Scene!.TrezorOpen = true;
                                session.Scene.Trezor.SecondEncounter = G.T("Nic kromě klíče v trezoru nebylo.");
                                session.Scene.Door.SecondEncounter = G.T("Klikni na klíč a pak na dveře, abys je otevřel.");
                                session.ExitPuzzle();
                            }));


                        }
                        else
                        {
                            session.Enqueue(new QSpeak("", "Dioda na trezoru zablikala, a trezor zůstal zamčený. Tento kód je nejspíš špatný.", ArtName.Null, SpeakerPosition.Left));
                            session.Enqueue(new QAction(sss=> code = ""));
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

        public void Update()
        {
            
        }

        public void Exit(Session session)
        {
            session.Background = ArtName.InteriorDrizzleRain;
            session.PopZoom();
            session.Enqueue(new QZoomInto(session.FullResolution, 0.1f));
        }
    }
}