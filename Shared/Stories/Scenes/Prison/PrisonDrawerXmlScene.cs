using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Sounds;
using Nsnbc.Stories.Scenes.Xml;
using Nsnbc.Texts;

namespace Nsnbc.Stories.Scenes.Prison
{
    [JsonObject(MemberSerialization.Fields)]
    public class PrisonDrawerXmlScene : XmlScene
    {
        public string Code { get; set; } = "";

        public override bool HideInventory => true;

        public override void Draw(AirSession airSession)
        {
            base.Draw(airSession);
            Writer.DrawString(Code, new Rectangle(102, 73, 637, 164), Color.DarkBlue, alignment: Writer.TextAlignment.Middle);

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
                    if (Code.Length == 4)
                    {
                        if (Code == "6557")
                        {
                            airSession.Enqueue(new Script
                            {
                                Events =
                                {
                                    new QSfx(SoundEffectName.SfxTrezorOpen),
                                    QSpeak.Quick("Uslyšel jsem kovový zvuk, a šuplík se pootevřel!"),
                                    new QImmediateAction(() => { Code = G.T("odemčeno").ToString(); }),
                                    QSpeak.Quick("A uvnitř je..."),
                                    new QPopScene(),
                                    new QSetInteractibleFirstAndSecondUse("suplik2", QSpeak.Quick("Šuplík už jsme vyrabovali. Nic dalšího v něm není.")),
                                    new QAddToInventory(ArtName.R1Disketa, "Podivná věc. Nejspíš jsou na ní uložená nějaká data do počítače."),
                                    QSpeak.From("Tišík", Pose.Thinking, "...nějaká divná věc?"),
                                    QSpeak.From("Vědátor", Pose.Speaking, "Vypadá jako ikonka uložení ve Wordu."),
                                    QSpeak.From("Tišík", Pose.Speaking, "Je na tom napsáno 3,5\", 1.44 MB."),
                                    QSpeak.From("Vědátor", Pose.Excited, "Megabajty! To znamená, že je to něco jako fleška. Jsou na tom data!"),
                                    QSpeak.From("Tišík", Pose.Pointing, "Ale počkat, je v tom šuplíku ještě jedna věc..."),
                                    new QAddToInventory(ArtName.R1Balicek, "Balíček pro sběratelskou karetní hru Bifröst! Obsahuje mýticky vzácného Konnningskrakena!"),
                                    QSpeak.From("Tišík", Pose.Excited, "Je to balíček karet Bifröstu!"),
                                    QSpeak.From("Vypravěč", Pose.Speaking, "Cože to je?"),
                                    QSpeak.From("Tišík", Pose.Excited, "Bifröst! Sběratelská karetní hra o bozích, příšerách a ledu!"),
                                    QSpeak.From("Vědátor", Pose.Speaking, "Tenhle balíček je plný vzácných a mýticky vzácných karet."),
                                    QSpeak.From("Tišík", Pose.Excited, "Ah! Je tu i Konningskraken, který strhl Himeko do hlubin Říše příšer."),
                                    QSpeak.From("Vědátor", Pose.Thinking, "Není divu. Má 140 životů, a dva epické speciální útoky."), 
                                    QSpeak.From("Vypravěč", Pose.Angry, "Hej, vy dva! Nechtěli byste odložit ty karty a věnovat se hledání cesty ven?!")
                                }
                            });
                        }
                        else
                        {
                            airSession.Enqueue(new Script
                            {
                                Events =
                                {
                                    QSpeak.Quick("Dioda na zařízení zablikala, ale šuplík zůstal zamčený. Kód je nejspíše špatný."),
                                    new QImmediateAction(() => { Code = ""; } )
                                }
                            });

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

    }
}