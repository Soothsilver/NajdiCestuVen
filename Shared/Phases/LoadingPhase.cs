using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nsnbc.Auxiliary;
using Nsnbc.Auxiliary.Fonts;
using Nsnbc.PostSharp;
using Nsnbc.Sounds;
using Nsnbc.Stories;
using Nsnbc.Texts;

namespace Nsnbc.Phases
{
    public class LoadingPhase : GamePhase
    {
        private readonly Color backgroundYellow = Color.FromNonPremultiplied(255,245,181,255);
        private int complete;
        private int total;
        private string loadingWhat = null!;
        private bool ended;

        [Trace]
        protected internal override void Initialize(Game game)
        {
            base.Initialize(game);
            ArtName[] allArt = (ArtName[])typeof(ArtName).GetEnumValues();

            int musicWorth = 3 * 6;
            int sfxsWorth = 10 * 1;
            int scriptsWorth = 1;
            
            total = allArt.Length + musicWorth + sfxsWorth + /*allVoice.Length +*/ scriptsWorth;
            
            Thread backgroundThread = new Thread(() =>
            {
                try {
                    List<Task> tasks = new List<Task>();
                    foreach (ArtName art in allArt)
                    {
                        tasks.Add(Task.Run(() =>
                        {
                            loadingWhat = G.T("Načítám obrázek {0}...", art.ToString());
                            Library.LoadArt(art);
                            Interlocked.Increment(ref complete);
                        }));
                    }
                    
                    tasks.Add(Task.Run(() =>
                    {
                        loadingWhat = G.T("Načítám hudbu...").ToString();
                        Sfxs.LoadMusic(Root.Game.Content);
                        Interlocked.Add(ref complete, musicWorth);
                        
                        loadingWhat = G.T("Načítám zvukové efekty...").ToString();
                        Sfxs.LoadSfxs();
                        Interlocked.Add(ref complete, sfxsWorth);
                    }));
                  
                    tasks.Add(Task.Run(() =>
                    {
                        loadingWhat = G.T("Načítám skript...").ToString();
                        Scripts.LoadAll();
                        Interlocked.Add(ref complete, scriptsWorth);
                    }));
                    Task all = Task.WhenAll(tasks);
                    all.Wait();
                }
                catch (AggregateException ex)
                {
                    loadingWhat = G.T("Chyba: {0}", string.Join(" ", ex.InnerExceptions.Select(exc => exc.InnerException?.Message ?? exc.Message)));
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
            Writer.DrawString(ended ? G.T("Soubory hry jsou poškozeny. Přeinstalujte prosím hru, a pokud to nepomůže, kontaktujte autora hry.") : G.T("Načítání..."), Root.Screen.Extend(-20,-20), Color.Black, BitmapFontGroup.Main40, Writer.TextAlignment.Middle);
            Rectangle rectBar = new Rectangle(20, Root.Screen.Height - 150, Root.Screen.Width-40, 100);
            Writer.DrawHpBar(rectBar, Color.Yellow, complete, total);
            Writer.DrawString(loadingWhat, new Rectangle(rectBar.X + 30, rectBar.Y - 400, rectBar.Width, 395),
                Color.Black, BitmapFontGroup.Main40, Writer.TextAlignment.BottomLeft);
        }

        protected internal override void Update(Game game, float elapsedSeconds)
        {
            if (complete == total)
            {
                Root.PopFromPhase();
                Root.PushPhase(new MainMenuPhase());
            }

            if (Root.WasKeyPressed(Keys.Escape))
            {
                Process.GetCurrentProcess().Kill();
            }
            base.Update(game, elapsedSeconds);
            
           
        }
        
    }
}