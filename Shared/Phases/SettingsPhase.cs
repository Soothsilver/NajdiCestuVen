using System;
using System.Collections.Generic;
using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Android;

namespace Nsnbc.Phases
{
    public class SettingsPhase : GamePhase
    {
        public List<Tab> Tabs = new List<Tab>();
        public Tab SelectedTab = null!;
        
        protected internal override void Initialize(Game game)
        {
            Eqatec.Send("PHASE SETTINGS");
            int itemHeight = 70;
            int itemHeightGap = 85;
            Tabs.Add(new Tab(G.T("Zobrazení"), (r) =>
            {
                Ux.DrawCheckbox(new Rectangle(r.X, r.Y, r.Width, itemHeight), G.T("Celoobrazovkový režim"), () => Settings.Instance.FullScreenMode == FullScreenMode.Fullscreen,
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
            }));
            Tabs.Add(new Tab(G.T("Zvuk"), (r) =>
            {
                Ux.DrawCheckbox(new Rectangle(r.X, r.Y, r.Width, itemHeight), G.T("Pípat při nenamluveném dialogu"), () => Settings.Instance.BeepUnvoicedLines,
                    () => { Settings.Instance.BeepUnvoicedLines = !Settings.Instance.BeepUnvoicedLines;});
                Ux.DrawCheckbox(new Rectangle(r.X, r.Y+itemHeightGap, r.Width, itemHeight), G.T("Přehrávat namluvený dialog"), () => Settings.Instance.UseVoices,
                    () => { Settings.Instance.UseVoices = !Settings.Instance.UseVoices;});
                Ux.DrawCheckbox(new Rectangle(r.X, r.Y+itemHeightGap*2, r.Width, itemHeight), G.T("Ztlumit hudbu, když je hra na pozadí"), () => Settings.Instance.PauseMusicWhileInactive,
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
                Ux.DrawCheckbox(new Rectangle(r.X, r.Y, r.Width, itemHeight), G.T("Potvrzovací dialog při ukončení hry nebo návratu do menu"), () => Settings.Instance.ConfirmExitGame,
                    () => { Settings.Instance.ConfirmExitGame = !Settings.Instance.ConfirmExitGame;});
                
                Ux.DrawCheckbox(new Rectangle(r.X, r.Y + itemHeightGap, r.Width, itemHeight), G.T("Auto mód (přehrávat dialogy automaticky)"), () => Settings.Instance.AutoMode,
                    () => { Settings.Instance.AutoMode = !Settings.Instance.AutoMode;});
            }));
            SelectedTab = Tabs[0];
            base.Initialize(game);
        }

        private void DrawSlider(Rectangle rectangle, string caption, Func<float> getValue, Action<float> setValue, Action? whenSliderRelease = null)
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
                        pressedSlider = caption;
                        sliderRelease = whenSliderRelease;
                    }

                    if (pressedSlider == caption)
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
            Primitives.FillRectangle(Root.Screen, Color.Black.Alpha(100));
            Rectangle rectWindow = new Rectangle(10,10, Root.Screen.Width-20, Root.Screen.Height -20);
            Primitives.DrawAndFillRoundedRectangle(rectWindow, Theme.WindowBackground, Theme.WindowBorder, 2);
            
            // Title
            Rectangle rectWindowTitle = new Rectangle(rectWindow.X + 20, rectWindow.Y, rectWindow.Width, 80);
            Writer.DrawString(G.T("Nastavení"), rectWindowTitle, Color.Black, BitmapFontGroup.Main40, Writer.TextAlignment.Left);
            
            // Tabs
            Rectangle rectTabs = new Rectangle(rectWindow.X + 5, rectWindowTitle.Bottom + 5, rectWindow.Width - 10, rectWindow.Bottom - rectWindowTitle.Bottom - 5 - 130);

            DrawTabs(rectTabs);
            
            // Button
            Rectangle rectBack = new Rectangle(rectTabs.Right - 400, rectTabs.Bottom + 20, 400, 100);
            Ux.DrawButton(rectBack, G.T("Zpět"), Root.PopFromPhase);
        }

        private void DrawTabs(Rectangle rectTabs)
        {
            int x = 0;
            int captionWidth = 500;
            Rectangle inside = new Rectangle(rectTabs.X + 15, rectTabs.Y + 100, rectTabs.Width - 30, rectTabs.Height - 110);
            foreach (Tab tab in Tabs)
            {
                bool selected = tab == SelectedTab;
                Rectangle rCaption = new Rectangle(rectTabs.X + x, rectTabs.Y, captionWidth, 80);
                bool mo = Root.IsMouseOver(rCaption);
                Primitives.FillRectangle(rCaption, mo ? Color.White : (selected ? Theme.WindowBackground : Color.LightBlue));
                Primitives.DrawLine(new Vector2(rCaption.X, rCaption.Y), new Vector2(rCaption.X, rCaption.Bottom), Color.Black, 2);
                Primitives.DrawLine(new Vector2(rCaption.X, rCaption.Y), new Vector2(rCaption.Right, rCaption.Y), Color.Black, 2);
                Primitives.DrawLine(new Vector2(rCaption.Right, rCaption.Y), new Vector2(rCaption.Right, rCaption.Bottom), Color.Black, 2);
                if (selected)
                {
                    tab.Draw(inside);
                }
                else
                {
                    Primitives.DrawLine(new Vector2(rCaption.X, rCaption.Bottom), new Vector2(rCaption.Right, rCaption.Bottom), Color.Black, 2);
                }
                Writer.DrawString(tab.Caption, rCaption.Extend(-3,-3), Color.Black, BitmapFontGroup.Main32, Writer.TextAlignment.Middle);
                if (mo)
                {
                    Ux.MouseOverAction = () => SelectedTab = tab;
                }
                x += captionWidth;
            }
            Primitives.DrawLine(new Vector2(rectTabs.X + x, rectTabs.Y + 80), new Vector2(rectTabs.Right, rectTabs.Y + 80), Color.Black, 2);
            Primitives.DrawLine(new Vector2(rectTabs.X, rectTabs.Y + 85), new Vector2(rectTabs.X, rectTabs.Bottom), Color.Black, 2);
            Primitives.DrawLine(new Vector2(rectTabs.Right, rectTabs.Y +85), new Vector2(rectTabs.Right, rectTabs.Bottom), Color.Black, 2);
            Primitives.DrawLine(new Vector2(rectTabs.X, rectTabs.Bottom), new Vector2(rectTabs.Right, rectTabs.Bottom), Color.Black, 2);
        }
    }

    public class Tab
    {
        public string Caption { get; }
        public Action<Rectangle> Draw { get; }

        public Tab(string caption, Action<Rectangle> draw)
        {
            Caption = caption;
            Draw = draw;
        }
    }
}