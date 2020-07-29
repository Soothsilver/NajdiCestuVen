﻿using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nsnbc.Auxiliary;
using Nsnbc.Auxiliary.Fonts;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.Phases;
using Nsnbc.PostSharp;
using Nsnbc.SerializableCode;
using Nsnbc.Services;
using Nsnbc.Sounds;
using Nsnbc.Util;
using PostSharp.Patterns.Diagnostics;

namespace Nsnbc
{
    public class MainLoop
    {
        private static LogSource logSource = LogSource.Get();
        private readonly int Cogsize = PlatformServices.Platform == Platform.Android ? 100 : 64;
        public AirSession AirSession { get; }

        public MainLoop(AirSession airSession)
        {
            this.AirSession = airSession;
        }

        public void Draw(float elapsedSeconds)
        {
            // Background
            AirSession.Session.ActiveScene?.Draw(AirSession);
            foreach (IDrawableActivity activity in AirSession.ActiveActivities.OfType<IDrawableActivity>())
            {
                activity.Draw();
            }
            // ADV
            var fullResolution = CommonGame.R1920x1080;
            VisualNovelLine currentLine = AirSession.Session.CurrentLine;
            if (currentLine.SpeakingText != null)
            {
                if (currentLine.SpeakerLeft != ArtName.Null)
                {
                    Primitives.DrawImage(Library.Art(currentLine.SpeakerLeft), new Rectangle(0, 0, 552, 801));
                }

                if (currentLine.SpeakerRight != ArtName.Null)
                {                   
                    Primitives.DrawImage(Library.FlippedArt(currentLine.SpeakerRight), new Rectangle(fullResolution.Width - 552, 0, 552, 801));
                }
                Primitives.DrawImage(Library.Art(ArtName.ADVBar), fullResolution, Color.White.Alpha(Settings.Instance.Opacity255));
                if ((currentLine.SpeakingSpeaker?.ToString()).IsNonBlank())
                {
                    switch (currentLine.SpeakerPosition)
                    {
                        case SpeakerPosition.Left:
                            Primitives.DrawImage(Library.Art(ArtName.SpeakerLeft), fullResolution, Color.White);
                            Writer.DrawString(currentLine.SpeakingSpeaker, new Rectangle(30, 764, 406, 69), Color.Black, alignment: Writer.TextAlignment.Left);
                            break;
                        case SpeakerPosition.Right:
                            Primitives.DrawImage(Library.Art(ArtName.SpeakerRight), fullResolution, Color.White);
                            Writer.DrawString(currentLine.SpeakingSpeaker, new Rectangle(1491, 764, 398, 69), Color.Black, alignment: Writer.TextAlignment.Right);
                            break;
                    }
                }
                Writer.DrawString(currentLine.SpeakingText, new Rectangle(15, 862, 1648, 207), Color.Black,
                    BitmapFontGroup.Main40);
                if (currentLine.SpeakingAuxiAction != null)
                {
                    Ux.DrawButton(new Rectangle(1500, 820, 400, 240), currentLine.SpeakingAuxiAction.Caption, () =>
                    {
                        currentLine.SpeakingAuxiAction.Effect.Execute(new CodeInput() { HardSession = AirSession.Session }, AirSession);
                    }, true, alignment: Writer.TextAlignment.Middle);
                }
            }
            
            // Inventory
            if (AirSession.Session.Inventory.Count > 0)
            {
                for (int i = -1; i < AirSession.Session.Inventory.Count; i++)
                {
                    int xMove = (i + 1) * 128;
                    Rectangle rThis = new Rectangle(xMove,0,128,128);
                    bool isHeld = (i == -1 && AirSession.Session.HeldItem == null) || (i >= 0 && AirSession.Session.HeldItem == AirSession.Session.Inventory[i]); 
                    Primitives.DrawAndFillRectangle(rThis, isHeld ? Color.Yellow : Color.Gainsboro, Color.Black);
                    if (i >= 0)
                    {
                        Primitives.DrawImage(Library.Art(AirSession.Session.Inventory[i].Art), rThis);
                    }

                    int j = i;
                    if (Root.IsMouseOver(rThis))
                    {
                        Ux.ButtonHasPriority = false;
                        Ux.MouseOverAction = () =>
                        {
                            if (j == -1)
                            {
                                AirSession.Session.HeldItem = null;
                            }
                            else
                            {
                                if (AirSession.Session.HeldItem == AirSession.Session.Inventory[j])
                                {
                                    AirSession.Session.HeldItem = null;
                                }
                                else
                                {
                                    AirSession.Session.HeldItem = AirSession.Session.Inventory[j];
                                }
                            }
                        };
                    }
                }
            }
            
            // Settings
            Rectangle rCog = new Rectangle(fullResolution.Width - Cogsize, 0, Cogsize,Cogsize);
            Primitives.DrawImage(Library.Art(ArtName.Cog64), rCog);
            if (Root.IsMouseOver(rCog))
            {
                Ux.ButtonHasPriority = true;
                Ux.MouseOverAction = () => Root.PushPhase(new InGameOptionsPhase(this));
            }
            
            // Fast forward
            Rectangle rFast = new Rectangle(fullResolution.Width - Cogsize, Cogsize, Cogsize,Cogsize);
            AirSession.FastForwarding = Root.IsMouseOver(rFast) && (Root.MouseNewState.LeftButton == ButtonState.Pressed || Root.CurrentTouches.Any());
            Primitives.DrawImage(AirSession.FastForwarding ? Library.Art(ArtName.FastForward128):Library.Art(ArtName.FastForward128Disabled), rFast);
            
            // Auto
            Rectangle rAuto = new Rectangle(fullResolution.Width - Cogsize, 2* Cogsize, Cogsize,Cogsize);
            Primitives.DrawImage(Settings.Instance.AutoMode ? Library.Art(ArtName.Auto128) : Library.Art(ArtName.Auto128Disabled), rAuto);
            if (Root.IsMouseOver(rAuto))
            {
                Ux.ButtonHasPriority = true;
                Ux.MouseOverAction = () => Settings.Instance.AutoMode = !Settings.Instance.AutoMode;
            }
        }
        
        public void Update(float elapsedSeconds)
        {
            Sfxs.Update();
            // First, priority buttons:
            if ((Root.WasMouseLeftClick || Root.WasTouchReleased))
            {
                if (Ux.MouseOverAction != null && Ux.ButtonHasPriority)
                {
                    Ux.MouseOverAction();
                    Root.WasMouseLeftClick = false;
                    Root.WasTouchReleased = false;
                }
                else if (AirSession.Session.YouHaveControl &&
                         AirSession.Session.IncomingEvents.Count == 0 && 
                         AirSession.ActiveActivities.All(act => !act.Blocking))
                {
                    if (AirSession.ActiveScene?.Click(AirSession) ?? false)
                    {
                        Root.WasMouseLeftClick = false;
                        Root.WasTouchReleased = false;
                    }
                }
            }
            
            // Next, activities:
            bool searchQueue = true;
            while (searchQueue)
            {
                foreach (IQActivity activity in AirSession.ActiveActivities)
                {
                    activity.Update(AirSession, elapsedSeconds);
                }
                
                // Next, other buttons:
                if (Root.WasMouseLeftClick || Root.WasTouchReleased)
                {
                    if (Ux.MouseOverAction != null && !Ux.ButtonHasPriority)
                    {
                        Ux.MouseOverAction();
                        Root.WasMouseLeftClick = false;
                        Root.WasTouchReleased = false;
                    }
                }

                AirSession.ActiveActivities.RemoveAll(ac => ac.Dead);
                searchQueue = ConsiderProceedingInQueue();
            }
            
            // Menu
            if (Root.WasKeyPressed(Keys.Escape))
            {
                Root.PushPhase(new InGameOptionsPhase(this));
            }

            if (Root.WasKeyPressed(Keys.F2))
            {
                logSource.Info.Write(FormattedMessageBuilder.Formatted("F2 Debugging hotkey!"));
                SaveLoad.SaveGame(this.AirSession.Session, Library.Art(ArtName.SlotQuestion), 7);
                Session loadedSession = SaveLoad.LoadGame(7);
                Root.PopFromPhase();
                Root.PushPhase(new SessionPhase(SessionLoader.LoadFromHardSession(loadedSession)));
            }
 
        }

        public bool ConsiderProceedingInQueue()
        {
            bool somethingHappened = false;
            while (AirSession.Session.IncomingEvents.Count > 0 && !AirSession.ActiveActivities.Any(act => act.Blocking))
            {
                QEvent qEvent = AirSession.Session.IncomingEvents.Dequeue();
                qEvent.Begin(AirSession);
                somethingHappened = true;
            }
            return somethingHappened;
        }

        [Trace]
        public void FastForwardTo(int targetIndex)
        {
            int currentIndex = 0;
            while (AirSession.Session.IncomingEvents.Count > 0)
            {
                if (currentIndex >= targetIndex)
                {
                    return;
                }
                QEvent qEvent = AirSession.Session.IncomingEvents.Dequeue();
                qEvent.FastForward(AirSession);
                currentIndex++;
            }
        }
    }
}