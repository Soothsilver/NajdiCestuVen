﻿using System;
using System.Collections.Generic;
using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Android;

namespace Nsnbc.Phases
{
    public class SettingsPhase : TabbedPhase
    {
        public SettingsPhase() : base(G.T("Nastavení"))
        {
        }
        
        protected internal override void Initialize(Game game)
        {
            Eqatec.Send("PHASE SETTINGS");
            int itemHeight = 70;
            int itemHeightGap = 85;
            int halfSize = 800;
            Tabs.Add(new Tab(G.T("Zobrazení"), (r) =>
            {
                Ux.DrawCheckbox(new Rectangle(r.X, r.Y, halfSize, itemHeight), G.T("Celoobrazovkový režim"), () => Settings.Instance.FullScreenMode == FullScreenMode.Fullscreen,
                    () =>
                    {
                        if (Settings.Instance.FullScreenMode == FullScreenMode.Fullscreen)
                        {
                            Settings.Instance.FullScreenMode = FullScreenMode.Windowed;
                        }
                        else
                        {
                            Settings.Instance.FullScreenMode = FullScreenMode.Fullscreen;
                        }
                        ((CommonGame) game).ApplyFullScreenModeChanges();
                    });
                DrawSlider(new Rectangle(r.X, r.Y + itemHeightGap * 1, 800, itemHeight), G.T("Sytost okénka s textem"), () => Settings.Instance.WindowOpacity, (val) => Settings.Instance.WindowOpacity = val);
                Ux.DrawLanguageSelector(new Rectangle(r.X, r.Y + itemHeightGap * 2, 490, 150));
            }));
            Tabs.Add(new Tab(G.T("Zvuk"), (r) =>
            {
                Ux.DrawCheckbox(new Rectangle(r.X, r.Y, halfSize, itemHeight), G.T("Pípat při nenamluveném dialogu"), () => Settings.Instance.BeepUnvoicedLines,
                    () => { Settings.Instance.BeepUnvoicedLines = !Settings.Instance.BeepUnvoicedLines;});
                Ux.DrawCheckbox(new Rectangle(r.X, r.Y+itemHeightGap, halfSize, itemHeight), G.T("Přehrávat namluvený dialog"), () => Settings.Instance.UseVoices,
                    () => { Settings.Instance.UseVoices = !Settings.Instance.UseVoices;});
                Ux.DrawCheckbox(new Rectangle(r.X, r.Y+itemHeightGap*2, halfSize, itemHeight), G.T("Ztlumit hudbu, když je hra na pozadí"), () => Settings.Instance.PauseMusicWhileInactive,
                    () => { Settings.Instance.PauseMusicWhileInactive = !Settings.Instance.PauseMusicWhileInactive;});
                
                DrawSlider(new Rectangle(r.X, r.Y + itemHeightGap * 3, 800, itemHeight), G.T("Celková hlasitost"), () => Settings.Instance.MasterVolume, (val) =>
                {
                    Settings.Instance.MasterVolume = val;
                    Sfxs.UpdateVolumes();
                }, () => Sfxs.Play(Sfxs.SfxHarp));
                DrawSlider(new Rectangle(r.X, r.Y + itemHeightGap * 4, 800, itemHeight), G.T("Hlasitost hudby"), () => Settings.Instance.MusicVolume, (val) =>
                {
                    Settings.Instance.MusicVolume = val;
                    Sfxs.UpdateVolumes();
                });
                DrawSlider(new Rectangle(r.X, r.Y + itemHeightGap * 5, 800, itemHeight), G.T("Hlasitost zvuků"), () => Settings.Instance.SfxVolume, (val) =>
                {
                    Settings.Instance.SfxVolume = val;
                    Sfxs.UpdateVolumes();
                }, () => Sfxs.Play(Sfxs.SfxHarp));
                DrawSlider(new Rectangle(r.X, r.Y + itemHeightGap * 6, 800, itemHeight), G.T("Hlasitost hlasů"), () => Settings.Instance.VoiceVolume, (val) =>
                {
                    Settings.Instance.VoiceVolume = val;
                    Sfxs.UpdateVolumes();
                }, () => Sfxs.PlayVoice(Voice.A1_Akela));

                Ux.DrawButton(new Rectangle(r.X, r.Y + itemHeightGap * 7, r.Width / 2, itemHeight + 40), G.T("Resetovat na výchozí hodnoty"), () =>
                {
                    Settings.Instance.ResetSoundToDefaults();
                    Sfxs.Play(Sfxs.SfxHarp);
                });
            }));
            Tabs.Add(new Tab(G.T("Ostatní"), (r) =>
            {
                Ux.DrawCheckbox(new Rectangle(r.X, r.Y, r.Width - 200, itemHeight), G.T("Potvrzovací dialog při ukončení hry nebo návratu do menu"), () => Settings.Instance.ConfirmExitGame,
                    () => { Settings.Instance.ConfirmExitGame = !Settings.Instance.ConfirmExitGame;});
                
                Ux.DrawCheckbox(new Rectangle(r.X, r.Y + itemHeightGap, r.Width - 200, itemHeight), G.T("Auto mód (přehrávat dialogy automaticky)"), () => Settings.Instance.AutoMode,
                    () => { Settings.Instance.AutoMode = !Settings.Instance.AutoMode;});
            }));
            SelectedTab = Tabs[0];
            base.Initialize(game);
        }

        private void DrawSlider(Rectangle rectangle, GString caption, Func<float> getValue, Action<float> setValue, Action? whenSliderRelease = null)
        { 
            bool mo = Root.IsMouseOver(rectangle);
            int textWidth = 400;
            Vector2 start = new Vector2(rectangle.X + textWidth, rectangle.Y + rectangle.Height / 2);
            Vector2 end = new Vector2(rectangle.Right, rectangle.Y + rectangle.Height / 2);
            // TODO tap
            float percentage = getValue();
            float percentageOfMouse = (float) MathHelper.Clamp((Root.MouseNewState.X - start.X) / (end.X - start.X), 0, 1);
            Primitives.DrawLine(start, end, Color.Black, 2);
            float xMidPoint = start.X + (end.X - start.X) * percentage;
            Primitives.DrawLine(new Vector2(xMidPoint, rectangle.Y), new Vector2(xMidPoint, rectangle.Bottom), Color.Black, 2);
            Writer.DrawString(caption, new Rectangle(rectangle.X, rectangle.Y, textWidth, rectangle.Height), Color.Black, BitmapFontGroup.Main40, Writer.TextAlignment.Left);
            Writer.DrawString(((int)(100 * percentage)) + "%", new Rectangle(rectangle.Right + 5, rectangle.Y, textWidth, rectangle.Height), Color.Black, BitmapFontGroup.Main40, Writer.TextAlignment.Left);
          
                if (Root.MouseNewState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    if (mo)
                    {
                        pressedSlider = caption.ToString();
                        sliderRelease = whenSliderRelease;
                    }

                    if (pressedSlider == caption.ToString())
                    {
                        setValue(percentageOfMouse);
                    }
                }
        }

        private string? pressedSlider = null;
        private Action? sliderRelease = null;
        
        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {
            if (Root.MouseNewState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                // TODO android
                if (pressedSlider != null)
                {
                    sliderRelease?.Invoke();
                    LocalDataStore.Write();
                }
                pressedSlider = null;
            }
            base.Draw(sb, game, elapsedSeconds);
        }

    }
}