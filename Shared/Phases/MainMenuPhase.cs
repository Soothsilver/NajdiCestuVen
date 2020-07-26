﻿using System;
using System.Diagnostics;
using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Android;
using Nsnbc.Auxiliary.Fonts;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.PostSharp;
using Nsnbc.Services;

namespace Nsnbc.Phases
{
    public class MainMenuPhase : GamePhase
    {
        [Trace]
        protected internal override void Initialize(Game game)
        {
            base.Initialize(game);
            Sfxs.BeginSong(Sfxs.MusicMenu);
            Eqatec.Send("ENTER MAIN MENU");
        }

        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {
            Primitives.DrawImage(Library.Art(ArtName.Exterior), Root.Screen);
            Primitives.FillRectangle(Root.Screen, Color.White.Alpha(150));
            Texture2D logo = Library.Art(G.CzEn(ArtName.Logo, ArtName.Logo));
            Root.SpriteBatch.Draw(logo, new Rectangle(1920/2-logo.Width/2,0,logo.Width, logo.Height), Color.White);


            int y = logo.Height - 100;
            int width = 490;
            int height = 80;
            if (PlatformServices.Platform == Platform.Android)
            {
                width += 150;
                height += 50;
            }
            int buttonX = Root.Screen.Width - width - 10;
            int gapHeight = height + 10;
            
            // Main menuN
            bool notImplemented = false;
            if (notImplemented) // TODO Continue
            {
                Ux.DrawButton(new Rectangle(buttonX, y, width, height), G.T("Nová hra"), NotImplemented);
            }
            y += gapHeight;
            Ux.DrawButton(new Rectangle(buttonX, y, width, height), G.T("Nová hra"), StartNewGame);
            y += gapHeight;
            if (!PlatformServices.HideExperimentalFeatures)
            {
                Ux.DrawButton(new Rectangle(buttonX, y, width, height), G.T("Pokračovat od scény"), LoadScene);
                y += gapHeight;
                Ux.DrawButton(new Rectangle(buttonX, y, width, height), G.T("Galerie"), StartGallery);
                y += gapHeight;
            }
            Ux.DrawButton(new Rectangle(buttonX, y, width, height), G.T("Nastavení"), OpenSettings);
            y += gapHeight;
            Ux.DrawButton(new Rectangle(buttonX, y, width, height), G.T("Poslat zpětnou vazbu"), ReportFeedback);
            y += gapHeight;
            Ux.DrawButton(new Rectangle(buttonX,  y, width, height), G.T("O autorech"), GoToCredits);
            y += gapHeight;
            Ux.DrawButton(new Rectangle(buttonX,  y, width, height), G.T("Ukončit hru"), QuitGame);
            
            // Language
            int flagX = 30;
          
            Ux.DrawLanguageSelector(new Rectangle(flagX, Root.Screen.Height - 220 - (PlatformServices.Platform == Platform.Android ? 130 : 0), 490, 150));
        
            
            // Version
            Writer.DrawString(
                string.Format(G.T("Verze {0}").ToString(), typeof(CommonGame).Assembly.GetName().Version.ToString(3)),
                new Rectangle(0, Root.Screen.Height - 80, 400, 80).Extend(-4, -4), Color.Black, BitmapFontGroup.Main24,
                Writer.TextAlignment.BottomLeft, true);
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
            Root.PushPhase(new SessionPhase(SessionLoader.LoadFromBookmark(BookmarkId.TechDemoStart)));
        }
    }
}