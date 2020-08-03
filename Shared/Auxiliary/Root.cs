using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Nsnbc.PostSharp;

namespace Nsnbc.Auxiliary
{
    public interface IInputMatrices
    {
        Vector2 InputTranslate { get; }
        Matrix InputScale { get; }
    }

    public static class Root
    {
        public static IInputMatrices InputScaling { get; set; } = null!;
        public static SpriteBatch SpriteBatch { get; private set; } = null!;
        public static Game Game { get; private set; } = null!;
        public static GraphicsDeviceManager Graphics { get; private set; } = null!;
        public static Rectangle Screen { get; set; } = new Rectangle(0,0,1920,1080);
        /// <summary>
        /// Mouse state in the previous Update() cycle.
        /// </summary>
        public static MouseState MouseOldState = Mouse.GetState();
        /// <summary>
        /// Mouse state in the current Update() cycle.
        /// </summary>
        public static MouseState MouseNewState = Mouse.GetState();

        public static List<Vector2> CurrentTouches = new List<Vector2>();
        public static List<Vector2> TemporaryTouches = new List<Vector2>();
        public static bool WasTouchReleased;
       
        
        /// <summary>
        /// Keyboard state in the previous Update() cycle.
        /// </summary>
        public static KeyboardState KeyboardOldState = Keyboard.GetState();
        /// <summary>
        /// Keyboard state in the current Update() cycle.
        /// </summary>
        public static KeyboardState KeyboardNewState = Keyboard.GetState();
        /// <summary>
        /// Returns true only if a key was just pressed down and released.
        /// </summary>
        /// <param name="key">We test whether this key was pressed and released</param>
        /// <param name="modifiersPressed">This combination of keys must have been pressed at the time of release</param>
        /// <returns></returns>
        public static bool WasKeyPressed(Keys key, params ModifierKey[] modifiersPressed)
        {
            if (KeyboardNewState.IsKeyDown(key) || KeyboardOldState.IsKeyUp(key)) return false;
           
            foreach(ModifierKey mk in modifiersPressed)
            {
                Keys mkKey = Keys.A;
                Keys mkKey2 = Keys.B;
                if (mk == ModifierKey.Alt) { mkKey = Keys.LeftAlt; mkKey2 = Keys.RightAlt; }
                if (mk == ModifierKey.Ctrl) { mkKey = Keys.LeftControl; mkKey2 = Keys.RightControl; }
                if (mk == ModifierKey.Shift) { mkKey = Keys.LeftShift; mkKey2 = Keys.RightShift; }
                if (mk == ModifierKey.Windows) { mkKey = Keys.LeftWindows; mkKey2 = Keys.RightWindows; }
                if (KeyboardOldState.IsKeyUp(mkKey) && KeyboardNewState.IsKeyUp(mkKey) &&
                    KeyboardOldState.IsKeyUp(mkKey2) && KeyboardNewState.IsKeyUp(mkKey2)
                ) return false;
            }
            
            return true;
        }
        private static void UpdateTouchState()
        {
            TouchCollection touchCollection = TouchPanel.GetState();
            WasTouchReleased = false;
            TemporaryTouches.Clear();
            if (touchCollection.Count > 0)
            {
                CurrentTouches.Clear();
            }
            foreach (TouchLocation location in touchCollection)
            {
                Vector2 rawPosition = location.Position;
                Vector2 p = rawPosition - InputScaling.InputTranslate;
                p = Vector2.Transform(p, InputScaling.InputScale);

                if (location.State == TouchLocationState.Moved || location.State == TouchLocationState.Pressed)
                {
                    CurrentTouches.Add(p);
                }

                if (location.State == TouchLocationState.Released)
                {
                    TemporaryTouches.Add(p);
                    WasTouchReleased = true;
                }
            }
        }
        /// <summary>
        /// The topmost GamePhase can be interacted with. All phases on the stack are drawn (beneath).
        /// </summary>
        public static readonly ImprovedStack<GamePhase> PhaseStack = new ImprovedStack<GamePhase>();
        /// <summary>
        /// Gets the game phase at the top of the stack or pushes a new game phase to the top of the stack.
        /// </summary>
        [DisallowNull, PublicAPI]
        [Trace]
        public static GamePhase? CurrentPhase
        {
            get
            {
                if (PhaseStack.Count > 0) return PhaseStack.Peek();
                return null;
            }
            set
            {
                PhaseStack.Push(value!);
            }
        }
        /// <summary>
        /// Calls the "Destruct" method of the phase, which should, by default, set the ScheduledForElimination flag.
        /// The phase will be popped from stack only later, not immediately.
        /// </summary>
        [Trace]
        public static void PopFromPhase()
        {
            foreach (var phase in PhaseStack.Reverse<GamePhase>())
            {
                if (!phase.ScheduledForElimination)
                {
                    phase.Destruct(Game);
                    return;
                }
            }
        }
        /// <summary>
        /// Draws all phases on the stack using the Root spritebatch, in stack order.
        /// </summary>
        /// <param name="gameTime">gameTime parameter from the Game.Draw() method.</param>
        public static void DrawPhase(GameTime gameTime)
        {
            GamePhase? lastPhase = PhaseStack.Peek();
            foreach (GamePhase gp in PhaseStack)
            {
                Ux.Clear();
                Ux.CanNonPriorityButtonsBePressed = gp == lastPhase;
                gp.Draw(SpriteBatch, Game, (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }
        [Trace]
        public static void Init(SpriteBatch spriteBatch, Game game, GraphicsDeviceManager graphics)
        {
            Game = game;
            SpriteBatch = spriteBatch;
            Graphics = graphics;
        } 
        /// <summary>
        /// Adds new game on top of the stack and initializes it.
        /// </summary>
        /// <param name="phase">The phase to put on stack.</param>
        [Trace]
        public static void PushPhase(GamePhase phase)
        {
            CurrentPhase = phase;
            phase.Initialize(Game);
        }

        public static bool IsMouseOver(Rectangle rectangle)
        {
            return IsRealMouseOver(rectangle) || IsTouchOver(rectangle);
        }

        private static bool IsTouchOver(Rectangle rectangle)
        {
            return CurrentTouches.Any(tch => tch.X >= rectangle.X &&
                                             tch.Y >= rectangle.Y &&
                                             tch.X < rectangle.Right &&
                                             tch.Y < rectangle.Bottom) ||
                   TemporaryTouches.Any(tch => tch.X >= rectangle.X &&
                                             tch.Y >= rectangle.Y &&
                                             tch.X < rectangle.Right &&
                                             tch.Y < rectangle.Bottom);
        }

        private static bool IsRealMouseOver(Rectangle rectangle)
        {
            return MouseNewState.X >= rectangle.X &&
                   MouseNewState.Y >= rectangle.Y    && 
                   MouseNewState.X < rectangle.Right && 
                   MouseNewState.Y < rectangle.Bottom;
        }

        public static void Update(GameTime gameTime)
        {
            if (!Game.IsActive)
            {
                return;
            }
            KeyboardOldState = KeyboardNewState;
            KeyboardNewState = Keyboard.GetState();
            MouseOldState = MouseNewState;
            MouseNewState = Mouse.GetState();

            if (InputScaling != null)
            {
                Vector2 rawPosition = MouseNewState.Position.ToVector2();
                Vector2 p = rawPosition - InputScaling.InputTranslate;
                p = Vector2.Transform(p, InputScaling.InputScale);
                MouseNewState = new MouseState((int) p.X, (int) p.Y, MouseNewState.ScrollWheelValue, MouseNewState.LeftButton, MouseNewState.MiddleButton, MouseNewState.RightButton, MouseNewState.XButton1, MouseNewState.XButton2,
                    MouseNewState.HorizontalScrollWheelValue);
            }

            WasMouseLeftClick = MouseNewState.LeftButton == ButtonState.Released && MouseOldState.LeftButton == ButtonState.Pressed;
            WasMouseMiddleClick = MouseNewState.MiddleButton == ButtonState.Released && MouseOldState.MiddleButton == ButtonState.Pressed;
            WasMouseRightClick = MouseNewState.RightButton == ButtonState.Released && MouseOldState.RightButton == ButtonState.Pressed;
            
            
            PhaseStack.Peek()?.Update(Game, (float)gameTime.ElapsedGameTime.TotalSeconds);
            
            for (int i = PhaseStack.Count - 1; i >= 0; i--)
            {
                GamePhase ph = PhaseStack[i];
                if (ph.ScheduledForElimination)
                    PhaseStack.RemoveAt(i);
            }
        }
        /// <summary>
        /// Gets or sets. This is set to true or false depending on whether a left mouse click occured since last calling Root.Update().
        /// </summary>
        [PublicAPI]
        public static bool WasMouseLeftClick { get; set; }
        /// <summary>
        /// Gets or sets. This is set to true or false depending on whether a middle mouse click occured since last calling Root.Update().
        /// </summary>
        [PublicAPI]
        public static bool WasMouseMiddleClick { get; set; }
        /// <summary>
        /// Gets or sets. This is set to true or false depending on whether a right mouse click occured since last calling Root.Update().
        /// </summary>
        [PublicAPI]
        public static bool WasMouseRightClick { get; set; }

        public static void UpdateTouch()
        {
            UpdateTouchState();
        }
    }
    /// <summary>
    /// A meta-key pressed alongside another key.
    /// </summary>
    public enum ModifierKey
    {
        /// <summary>
        /// Any Control key.
        /// </summary>
        Ctrl,
        /// <summary>
        /// Any Shift key.
        /// </summary>
        Shift,
        /// <summary>
        /// Any Alt key.
        /// </summary>
        Alt,
        /// <summary>
        /// Any Windows key.
        /// </summary>
        Windows
    }
}