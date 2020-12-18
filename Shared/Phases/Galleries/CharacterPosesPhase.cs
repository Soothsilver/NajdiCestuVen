using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Stories.Scenes.Xml;
using Nsnbc.Texts;
using Nsnbc.Util;

namespace Nsnbc.Phases.Galleries
{
    public class CharacterPosesPhase : GamePhase
    {
        private readonly List<GalleryItem> pictures;
        public string CharacterName { get; }

        public CharacterPosesPhase(string characterName)
        {
            CharacterName = characterName;
            pictures = new List<GalleryItem>();
            foreach (var pose in (Pose[])Enum.GetValues(typeof(Pose)))
            {
                ArtName artName = XmlCharacters.FindArt(CharacterName, pose);
                if (pose != Pose.Speaking && artName == XmlCharacters.FindArt(CharacterName, Pose.Speaking))
                {
                    artName = ArtName.Null;
                }
                Rectangle relevantRectangle = ArtManipulation.GetRelevantRectangle(Library.Art(artName));
                CGGalleryItem cgGalleryItem = new CGGalleryItem(
                    pose.ToGString(),
                    artName)
                {
                    SourceRectangle = new Rectangle(relevantRectangle.X, relevantRectangle.Y - 10, relevantRectangle.Width, relevantRectangle.Height + 10)
                };
                pictures.Add(cgGalleryItem);
            }
        }

        protected internal override void Draw(SpriteBatch sb, Game game, float elapsedSeconds)
        {
            Primitives.FillRectangle(Root.Screen, Color.Black.Alpha(50));
            Rectangle rect = new Rectangle(20, 20, Root.Screen.Width - 40, Root.Screen.Height - 40);
            Primitives.FillRectangle(rect, Color.LightBlue);
        
            Ux.DrawGallery(rect.Extend(-10, -10), pictures, 3);
            
            Rectangle rectBack = new Rectangle(rect.Right - 400, rect.Bottom -105, 400, 100);
            Ux.DrawButton(rectBack, G.T("Zpět"), Root.PopFromPhase);
        }

        protected internal override void Update(Game game, float elapsedSeconds)
        {
            if (Root.WasMouseRightClick)
            {
                Root.WasMouseRightClick = false;
                Root.PopFromPhase();
                return;
            }
            base.Update(game, elapsedSeconds);
        }
    }
}