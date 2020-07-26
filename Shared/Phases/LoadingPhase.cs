using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nsnbc.Auxiliary;
using Nsnbc.Auxiliary.Fonts;
using Nsnbc.PostSharp;
using Nsnbc.Sounds;
using Nsnbc.Texts;

namespace Nsnbc.Phases
{
    public class LoadingPhase : GamePhase
    {
        private readonly Color backgroundYellow = Color.FromNonPremultiplied(255,245,181,255);
        private int complete;
        private int total;
        private bool allComplete;
        private string loadingWhat = null!;
        private bool ended; 

        [Trace]
        protected internal override void Initialize(Game game)
        {
            base.Initialize(game);
            ArtName[] allArt = (ArtName[])typeof(ArtName).GetEnumValues();
            Voice[] allVoice = ((Voice[])typeof(Voice).GetEnumValues()).Where(vc => vc != Voice.Null).ToArray();

            int musicWorth = 3 * 6;
            int sfxsWorth = 10 * 1;
            
            total = allArt.Length + musicWorth + sfxsWorth + allVoice.Length;
            
            Thread backgroundThread = new Thread(() =>
            {
                try
                {
                    foreach (ArtName art in allArt)
                    {
                        loadingWhat = G.T("Načítám obrázek {0}...", art.ToString());
                        Library.LoadArt(art);
                        complete++;
                    }

                    loadingWhat = G.T("Načítám hudbu...").ToString();
                    Sfxs.LoadMusic(Root.Game.Content);
                    complete += musicWorth;
                    loadingWhat = G.T("Načítám zvukové efekty...").ToString();
                    Sfxs.LoadSfxs(Root.Game.Content);
                    foreach (Voice art in allVoice)
                    {
                        loadingWhat = G.T("Načítám dabovanou repliku {0}...", art.ToString());
                        Sfxs.LoadVoice(Root.Game.Content, art);
                        complete++;
                    }

                    allComplete = true;
                }
                catch (ContentLoadException ex)
                {
                    loadingWhat = G.T("Chyba: {0}", ex.InnerException?.Message ?? ex.Message);
                    ended = true;
                }
            })
            {
                IsBackground = true
            };
            backgroundThread.Start();
        }

        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {
            Primitives.FillRectangle(Root.Screen, backgroundYellow);
            Writer.DrawString(ended ? G.T("Soubory hry jsou poškozeny. Přeinstalujte prosím hru, a pokud to nepomůže, kontaktujte autora hry.") : G.T("Načítání..."), Root.Screen, Color.Black, BitmapFontGroup.Main40, Writer.TextAlignment.Middle);
            Rectangle rectBar = new Rectangle(20, Root.Screen.Height - 150, Root.Screen.Width-40, 100);
            Writer.DrawHpBar(rectBar, Color.Yellow, complete, total);
            Writer.DrawString(loadingWhat, new Rectangle(rectBar.X + 30, rectBar.Y - 100, rectBar.Width, 95),
                Color.Black, BitmapFontGroup.Main40, Writer.TextAlignment.BottomLeft);
        }

        protected internal override void Update(Game game, float elapsedSeconds)
        {
            if (allComplete)
            {
                Root.PopFromPhase();
                Root.PushPhase(new MainMenuPhase());
            }

            if (Root.WasKeyPressed(Keys.Escape) && ended)
            {
                Process.GetCurrentProcess().Kill();
            }
            base.Update(game, elapsedSeconds);
            
           
        }
        
    }
}