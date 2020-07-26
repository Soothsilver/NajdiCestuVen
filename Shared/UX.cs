﻿using System;
using System.Collections.Generic;
using Auxiliary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nsnbc.Auxiliary.Fonts;
using Nsnbc.Phases.Galleries;
using Nsnbc.Services;

namespace Nsnbc
{
    internal static class Ux
    {
        public static Action? MouseOverAction;
        public static bool ButtonHasPriority;

        public static void Clear()
        {
            MouseOverAction = null;
            ButtonHasPriority = false;
        }

        public static void DrawButton(Rectangle rectangle, GString caption, Action action, bool priority = false, Writer.TextAlignment alignment = Writer.TextAlignment.Left)
        {
            DrawButton(rectangle, caption.ToString(), action, priority, alignment);
        }

        public static void DrawButton(Rectangle rectangle, string caption, Action action, bool priority = false, Writer.TextAlignment alignment = Writer.TextAlignment.Left)
        {
            bool isMouseOverThis = Root.IsMouseOver(rectangle);
            bool pressed = isMouseOverThis && Root.MouseNewState.LeftButton == ButtonState.Pressed;
            Color outerBorderColor = Skin.OuterBorderColorMouseOver;
            Color innerBorderColor = pressed ? Skin.InnerBorderColorMousePressed : (isMouseOverThis  ? Skin.InnerBorderColorMouseOver : Skin.InnerBorderColor);
            Color innerButtonColor = isMouseOverThis ? Skin.GreyBackgroundColorMouseOver: Skin.GreyBackgroundColor;
            Color textColor = isMouseOverThis ? Skin.TextColorMouseOver : Skin.TextColor;
            Primitives.FillRectangle(rectangle, innerBorderColor);
            Primitives.DrawRectangle(rectangle, outerBorderColor, Skin.OuterBorderThickness);
            int inflation = -4;
            Primitives.DrawAndFillRectangle(rectangle.Extend(inflation,inflation), innerButtonColor, outerBorderColor, Skin.OuterBorderThickness);
            Writer.DrawString(caption, rectangle.Extend(inflation,inflation).MoveToRight(12), textColor, BitmapFontGroup.Main40, alignment);
            if (isMouseOverThis)
            {
                ButtonHasPriority = priority;
                MouseOverAction = action;
            }
        }

        public static void DrawCheckbox(Rectangle rectangle, GString caption, Func<bool> isChecked, Action onClick)
        {         
            bool isMouseOverThis = Root.IsMouseOver(rectangle);
            int h = rectangle.Height;
            Rectangle rectCheckbox = new Rectangle(rectangle.X, rectangle.Y, rectangle.Height, rectangle.Height);
            Rectangle rectCaption = new Rectangle(rectangle.X + rectangle.Height + 5, rectangle.Y, rectangle.Width - rectangle.Height - 5, rectangle.Height);
            Primitives.DrawAndFillRectangle(rectCheckbox, isMouseOverThis ? Color.Yellow : Color.Gainsboro, Color.Black, 2);
            Writer.DrawString(caption, rectCaption, Color.Black, BitmapFontGroup.Main40, alignment: Writer.TextAlignment.Left);
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

        public static void DrawFlag(Rectangle rectangle, ArtName flag, string caption, Language language)
        {
            Rectangle extended = rectangle.Extend(6, 6);
            if (GetText.CurrentLanguage == language)
            {
                Primitives.FillRectangle(extended, Color.LightSkyBlue.Alpha(150));
            }
            Primitives.DrawImage(Library.Art(flag), new Rectangle(rectangle.X, rectangle.Y, rectangle.Height, rectangle.Height));
            Writer.DrawString(caption, rectangle.MoveToRight(rectangle.Height + 10), Color.Black, BitmapFontGroup.Main40, Writer.TextAlignment.Left);
            if (Root.IsMouseOver(extended))
            {
                Primitives.DrawRectangle(extended, Color.Black, 3);
                MouseOverAction = () =>
                {
                    Settings.Instance.Language = language;
                    GetText.OnLanguageToggled();
                };
            }
        }

        public static void DrawLanguageSelector(Rectangle rectangle)
        {
            int flagHeight = PlatformServices.Platform == Platform.Android ? 120 : 60;
            int flagGapHeight = flagHeight + 20;
            DrawFlag(new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, flagHeight), ArtName.FlagCz,
                "Čeština", Language.Czech);
            DrawFlag(new Rectangle(rectangle.X, rectangle.Y + flagGapHeight, rectangle.Width, flagHeight), ArtName.FlagEn,
                "English", Language.English);
        }

        public static void DrawGallery(Rectangle r, List<GalleryItem> pictures)
        {
            int startAt = 0;
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (startAt >= pictures.Count)
                    {
                        return;
                    }
                    DrawGalleryPicture(pictures[startAt], new Rectangle(r.X + x * 260, r.Y + y * 200, 250, 140)); 
                    startAt++;
                }
            }
        }
        

        private static void DrawGalleryPicture(GalleryItem picture, Rectangle rect)
        {
            Primitives.DrawImage(Library.Art(picture.Art), rect);
            Writer.DrawString(picture.Caption, new Rectangle(rect.X, rect.Bottom, rect.Width, 150), Color.Black, BitmapFontGroup.Main24, Writer.TextAlignment.Top);
            bool mo = Root.IsMouseOver(rect);
            Primitives.DrawRectangle(rect, Color.Black, mo ? 3 : 1);
            if (mo)
            {
                MouseOverAction = picture.ClickAction;
            }
        }
    }

    internal static class Skin
    {
        public static Color OuterBorderColorMouseOver { get; } = Color.Black;
        public static Color TextColorMouseOver { get; } = Color.Black;
        public static Color GreyBackgroundColorMouseOver { get; } = Color.LightCyan;
        public static Color InnerBorderColorMousePressed { get; } = Color.DarkBlue;
        public static Color InnerBorderColorMouseOver { get; } = Color.Aqua;
        public static Color InnerBorderColor { get; } = Color.Aqua;
        public static Color GreyBackgroundColor { get; } = Color.LightSkyBlue;
        public static Color TextColor { get; } = Color.Black;
        public static int OuterBorderThickness { get; } = 3;
    }
}