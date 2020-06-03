using System;
using Auxiliary;
using Microsoft.Xna.Framework;
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

        public static void DrawCheckbox(Rectangle rectangle, string caption, Func<bool> isChecked, Action onClick)
        {         
            bool isMouseOverThis = Root.IsMouseOver(rectangle);
            int h = rectangle.Height;
            Rectangle rectCheckbox = new Rectangle(rectangle.X, rectangle.Y, rectangle.Height, rectangle.Height);
            Rectangle rectCaption = new Rectangle(rectangle.X + rectangle.Height + 5, rectangle.Y, rectangle.Width - rectangle.Height - 5, rectangle.Height);
            Primitives.DrawAndFillRectangle(rectCheckbox, isMouseOverThis ? Color.Yellow : Color.Gainsboro, Color.Black, 2);
            Writer.DrawString(caption, rectCaption, Color.Black, BitmapFontGroup.ASemi48, alignment: Writer.TextAlignment.Left);
            if (isChecked())
            {
                Vector2 topLeft = new Vector2(rectCheckbox.X + h / 4, rectCheckbox.Y + h/2);
                Vector2 bottom = new Vector2(rectCheckbox.X + h / 2, rectCheckbox.Y + h*3/4);
                Vector2 topRight = new Vector2(rectCheckbox.X + 3*h/4, rectCheckbox.Y + h/4);
                Primitives.DrawLine(topLeft, bottom, Color.Black, 4);
                Primitives.DrawLine(topRight, bottom, Color.Black, 4);
            }
            if (isMouseOverThis)
            {
                MouseOverAction = onClick;
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