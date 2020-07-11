using System.Linq;
using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nsnbc.Android;
using Nsnbc.Events;
using Nsnbc.Phases;

namespace Nsnbc
{
    public class MainLoop
    {
        public Session Session = new Session();
#if ANDROID
        public const int Cogsize = 100;
#else
        public const int Cogsize = 64;
#endif

        public void Draw(float elapsedSeconds)
        {
            // Background
            Root.SpriteBatch.Draw(Library.Art(Session.Background), Session.FullResolution, Session.CurrentZoom, Color.White);
            Session.Scene?.DrawBackground(Session);
            Session.Puzzle?.Draw(Session);
            foreach (IDrawableActivity activity in Session.ActiveActivities.OfType<IDrawableActivity>())
            {
                activity.Draw();
            }
            // ADV
            if (Session.SpeakingText != null)
            {
                if (Session.SpeakerLeft != ArtName.Null)
                {
                    Primitives.DrawImage(Library.Art(Session.SpeakerLeft), new Rectangle(0, 0, 552, 801));
                }

                if (Session.SpeakerRight != ArtName.Null)
                {                   
                    Primitives.DrawImage(Library.FlippedArt(Session.SpeakerRight), new Rectangle(Session.FullResolution.Width - 552, 0, 552, 801));
                }
                Primitives.DrawImage(Library.Art(ArtName.ADVBar), Session.FullResolution);
                if (!string.IsNullOrEmpty(Session.SpeakingSpeaker))
                {
                    switch (Session.SpeakerPosition)
                    {
                        case SpeakerPosition.Left:
                            Primitives.DrawImage(Library.Art(ArtName.SpeakerLeft), Session.FullResolution);
                            Writer.DrawString(Session.SpeakingSpeaker, new Rectangle(30, 764, 406, 69), Color.Black, alignment: Writer.TextAlignment.Left);
                            break;
                        case SpeakerPosition.Right:
                            Primitives.DrawImage(Library.Art(ArtName.SpeakerRight), Session.FullResolution);
                            Writer.DrawString(Session.SpeakingSpeaker, new Rectangle(1491, 764, 398, 69), Color.Black, alignment: Writer.TextAlignment.Right);
                            break;
                    }
                }
                Writer.DrawString(Session.SpeakingText, new Rectangle(15, 862, 1648, 207), Color.Black,
                    BitmapFontGroup.ASemi48);
                if (Session.SpeakingAuxiAction != null)
                {
                    Ux.DrawButton(new Rectangle(1500, 820, 400, 240), Session.SpeakingAuxiActionName!, () =>
                    {
                        Session.SpeakingAuxiAction(Session);
                    }, true);
                }
            }
            
            // Inventory
            if (Session.Inventory.Count > 0)
            {
                for (int i = -1; i < Session.Inventory.Count; i++)
                {
                    int xMove = (i + 1) * 128;
                    Rectangle rThis = new Rectangle(xMove,0,128,128);
                    bool isHeld = (i == -1 && Session.HeldItem == null) || (i >= 0 && Session.HeldItem == Session.Inventory[i]); 
                    Primitives.DrawAndFillRectangle(rThis, isHeld ? Color.Yellow : Color.Gainsboro, Color.Black);
                    if (i >= 0)
                    {
                        Primitives.DrawImage(Library.Art(Session.Inventory[i].Art), rThis);
                    }

                    int j = i;
                    if (Root.IsMouseOver(rThis))
                    {
                        Ux.ButtonHasPriority = false;
                        Ux.MouseOverAction = () =>
                        {
                            if (j == -1)
                            {
                                Session.HeldItem = null;
                            }
                            else
                            {
                                if (Session.HeldItem == Session.Inventory[j])
                                {
                                    Session.HeldItem = null;
                                }
                                else
                                {
                                    Session.HeldItem = Session.Inventory[j];
                                }
                            }
                        };
                    }
                }
            }
            
            // Settings
            Rectangle rCog = new Rectangle(Session.FullResolution.Width - Cogsize, 0, Cogsize,Cogsize);
            Primitives.DrawImage(Library.Art(ArtName.Cog64), rCog);
            if (Root.IsMouseOver(rCog))
            {
                Ux.ButtonHasPriority = true;
                Ux.MouseOverAction = () => Root.PushPhase(new InGameOptionsPhase());
            }
            
            // Fast forward
            Rectangle rFast = new Rectangle(Session.FullResolution.Width - Cogsize, Cogsize, Cogsize,Cogsize);
            Session.FastForwarding = Root.IsMouseOver(rFast) && (Root.MouseNewState.LeftButton == ButtonState.Pressed || Root.CurrentTouches.Any());
            Primitives.DrawImage(Session.FastForwarding ? Library.Art(ArtName.FastForward128):Library.Art(ArtName.FastForward128Disabled), rFast);
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
                else if (Session.YouHaveControl &&
                         Session.IncomingEvents.Count == 0 && 
                         Session.Puzzle == null &&
                         Session.ActiveActivities.All(act => !act.Blocking))
                {
                    if (Session.Scene?.Click(Session) ?? false)
                    {
                        Root.WasMouseLeftClick = false;
                        Root.WasTouchReleased = false;
                    }
                }
            }

            // Next, puzzle:
            Session.Puzzle?.Update();
            
            // Next, activities:
            bool searchQueue = true;
            while (searchQueue)
            {
                foreach (IQActivity activity in Session.ActiveActivities)
                {
                    activity.Update(Session, elapsedSeconds);
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

                Session.ActiveActivities.RemoveAll(ac => ac.Dead);
                searchQueue = ConsiderProceedingInQueue();
            }
            
            // Menu
            if (Root.WasKeyPressed(Keys.Escape))
            {
                Root.PushPhase(new InGameOptionsPhase());
            }

        }

        public bool ConsiderProceedingInQueue()
        {
            bool somethingHappened = false;
            while (Session.IncomingEvents.Count > 0 && !Session.ActiveActivities.Any(act => act.Blocking))
            {
                QEvent qEvent = Session.IncomingEvents.Dequeue();
                qEvent.Begin(Session);
                somethingHappened = true;
            }
            return somethingHappened;
        }
    }
}