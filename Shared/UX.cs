using System;
using Auxiliary;
using Nsnbc.Auxiliary;
using Origin.Display;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Nsnbc
{
    internal static class Ux
    {
        public static Action MouseOverAction = null;
        public static bool ButtonHasPriority = false;

        public static void Clear()
        {
            MouseOverAction = null;
            ButtonHasPriority = false;
        }

        public static void DrawButton(Rectangle rectangle, string caption, Action action, bool priority = false)
        {
            bool isMouseOverThis = Root.IsMouseOver(rectangle);
            bool pressed = false; // isMouseOverThis && Root.Mouse_NewState.LeftButton == ButtonState.Pressed;
            Color outerBorderColor = Skin.OuterBorderColorMouseOver;
            Color innerBorderColor = pressed ? Skin.InnerBorderColorMousePressed : (isMouseOverThis  ? Skin.InnerBorderColorMouseOver : Skin.InnerBorderColor);
            Color innerButtonColor = isMouseOverThis ? Skin.GreyBackgroundColorMouseOver: Skin.GreyBackgroundColor;
            Color textColor = isMouseOverThis ? Skin.TextColorMouseOver : Skin.TextColor;
            Primitives.FillRectangle(rectangle, innerBorderColor);
            Primitives.DrawRectangle(rectangle, outerBorderColor, Skin.OuterBorderThickness);
            Primitives.DrawAndFillRectangle(rectangle.Extend(-7,-7), innerButtonColor, outerBorderColor, Skin.OuterBorderThickness);
            Writer.DrawString(caption, rectangle.Extend(-7,-7), textColor, BitmapFontGroup.ASemi48, Writer.TextAlignment.Middle);
            if (isMouseOverThis)
            {
                ButtonHasPriority = priority;
                MouseOverAction = action;
            }
        }
    }

    internal class Skin
    {
        public static Color OuterBorderColorMouseOver { get; set; } = Color.Black;
        public static Color TextColorMouseOver { get; set; } = Color.Black;
        public static Color GreyBackgroundColorMouseOver { get; set; } = Color.LightCyan;
        public static Color InnerBorderColorMousePressed { get; set; } = Color.DarkBlue;
        public static Color InnerBorderColorMouseOver { get; set; } = Color.Aqua;
        public static Color InnerBorderColor { get; set; } = Color.Aqua;
        public static Color GreyBackgroundColor { get; set; } = Color.LightBlue;
        public static Color TextColor { get; set; } = Color.Black;
        public static int OuterBorderThickness { get; set; } = 3;
    }
}