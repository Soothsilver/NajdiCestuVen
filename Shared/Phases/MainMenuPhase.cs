using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nsnbc.Auxiliary;
using Nsnbc.Auxiliary.Fonts;
using Nsnbc.Core;
using Nsnbc.PostSharp;
using Nsnbc.Services;
using Nsnbc.Sounds;
using Nsnbc.Stories;
using Nsnbc.Texts;

namespace Nsnbc.Phases
{
    public class MainMenuPhase : GamePhase
    {
        [Trace]
        protected internal override void Initialize(Game game)
        {
            base.Initialize(game);
            Sfxs.BeginSong(Truesong.Menu);
            Eqatec.Send("ENTER MAIN MENU");
        }

        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {
            if (PlatformServices.HideExperimentalFeatures)
            {
                Primitives.DrawImage(Library.Art(ArtName.Exterior), Root.Screen);
                Primitives.FillRectangle(Root.Screen, Color.White.Alpha(150));
                Texture2D logo = Library.Art(G.CzEn(ArtName.Logo, ArtName.Logo));
                Root.SpriteBatch.Draw(logo, new Rectangle(1920/2-logo.Width/2,0,logo.Width, logo.Height), Color.White);
            }
            else
            {
                Primitives.DrawImage(Library.Art(ArtName.Background1), Root.Screen);
                Primitives.FillRectangle(Root.Screen, Color.White.Alpha(150));
                Texture2D logo = Library.Art(G.CzEn(ArtName.MainLogoCzech, ArtName.MainLogoEnglish));
                Root.SpriteBatch.Draw(logo, new Rectangle(1920/2-logo.Width/2,0,logo.Width, logo.Height), Color.White);
            }

            int baseY = 230;
            int y = baseY;
            int width = 490;
            int height = 80;
            var android = PlatformServices.Platform == Platform.Android;
            if (android)
            {
                width += 100;
                height += 50;
            }
            int buttonX = Root.Screen.Width - width - 10;
            int gapHeight = height + 10;
            
            // Android: double menu
            if (android)
            {
                buttonX -= width - 10;
            }
            // Main menu
            bool notImplemented = false;
            if (notImplemented) // TODO Continue
            {
                Ux.DrawButton(new Rectangle(buttonX, y, width, height), G.T("Nová hra"), NotImplemented, font: BitmapFontGroup.Main32);
                y += gapHeight;
            }
            Ux.DrawButton(new Rectangle(buttonX, y, width, height), G.T("Nová hra"), StartNewGame, font: BitmapFontGroup.Main32);
            y += gapHeight;
            if (!PlatformServices.HideExperimentalFeatures)
            {
                Ux.DrawButton(new Rectangle(buttonX, y, width, height), G.T("Pokračovat od scény"), LoadScene, font: BitmapFontGroup.Main32);
                y += gapHeight;
                Ux.DrawButton(new Rectangle(buttonX, y, width, height), G.T("Načíst hru"), LoadGame, font: BitmapFontGroup.Main32);
                y += gapHeight;
            }

            if (android)
            {
                buttonX += width + 10;
                y = baseY;
            }

            if (!PlatformServices.HideExperimentalFeatures)
            {
                Ux.DrawButton(new Rectangle(buttonX, y, width, height), G.T("Galerie"), StartGallery, font: BitmapFontGroup.Main32);
                y += gapHeight;
            }

            Ux.DrawButton(new Rectangle(buttonX, y, width, height), G.T("Nastavení"), OpenSettings, font: BitmapFontGroup.Main32);
            y += gapHeight;
            Ux.DrawButton(new Rectangle(buttonX, y, width, height), G.T("Poslat zpětnou vazbu"), ReportFeedback, font: BitmapFontGroup.Main32);
            y += gapHeight;
            Ux.DrawButton(new Rectangle(buttonX,  y, width, height), G.T("O autorech"), GoToCredits, font: BitmapFontGroup.Main32);
            y += gapHeight;
            Ux.DrawButton(new Rectangle(buttonX,  y, width, height), G.T("Ukončit hru"), QuitGame, font: BitmapFontGroup.Main32);
            
            // Language
            int flagX = 30;
          
            Ux.DrawLanguageSelector(new Rectangle(flagX, Root.Screen.Height - 220 - (android ? 130 : 0), 490, 150));
        
            
            // Version
            Writer.DrawString(
                string.Format(G.T("Verze {0}").ToString(), typeof(CommonGame).Assembly.GetName().Version.ToString(3)),
                new Rectangle(0, Root.Screen.Height - 80, 400, 80).Extend(-4, -4), Color.Black, BitmapFontGroup.Main24,
                Writer.TextAlignment.BottomLeft);
        }

        protected internal override void Update(Game game, float elapsedSeconds)
        {
            if (Root.WasKeyPressed(Keys.Escape))
            {
                QuitGame();
            }
            if ((Root.WasMouseLeftClick || Root.WasTouchReleased))
            {
                Root.WasMouseLeftClick = false;
                Root.WasTouchReleased = false;
                if (Ux.MouseOverAction != null)
                {
                    Ux.MouseOverAction();
                }
            }
            if (Root.WasKeyPressed(Keys.F1))
            {
                Root.PushPhase(new DebugLogPhase());
            }
        }

        private void LoadGame()
        {
            Root.PushPhase(new LoadGamePhase());
        }

        private void LoadScene()
        {
           Root.PushPhase(new LoadScenePhase());
        }

        private void OpenSettings()
        {
            Root.PushPhase(new SettingsPhase());
        }

        private void StartGallery()
        {
            Root.PushPhase(new GalleryPhase());
        }

        private void ReportFeedback()
        {
            if (PlatformServices.Platform == Platform.Windows)
            {
                string uri = G.CzEn("https://forms.gle/DKGPGmWf7RUbZBqR8", "https://forms.gle/ZYj5K3FiaimJDr63A");
                PlatformServices.Services.OpenInBrowser(new Uri(uri));
            }
            else
            {
                ConfirmationPhase cp = new ConfirmationPhase(G.T("Otevře se prohlížeč internetu. Pokračovat?"), () =>
                {
                    string uri = G.CzEn("https://forms.gle/DKGPGmWf7RUbZBqR8", "https://forms.gle/ZYj5K3FiaimJDr63A");
                    PlatformServices.Services.OpenInBrowser(new Uri(uri));
                });
                Root.PushPhase(cp);
            }
        }

        private void NotImplemented()
        {
            
        }

        private void QuitGame()
        {
            ConfirmationPhase.Confirm(G.T("Ukončit hru?"), () =>
                {
                    if (PlatformServices.Platform == Platform.Windows)
                    {
                        Root.Game.Exit();
                    }
                    else
                    {
                        Process.GetCurrentProcess().Kill();
                    }
                }
            );
        }

        private void GoToCredits()
        {
            Root.PushPhase(new AboutPhase());
        }

        private void StartNewGame()
        {
            Eqatec.Send("START NEW GAME");
            Root.PushPhase(new SessionPhase(SessionLoader.LoadFromBookmark(BookmarkId.TechDemoStart)));
        }
    }
}