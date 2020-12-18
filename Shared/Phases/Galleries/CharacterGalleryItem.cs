using Microsoft.Xna.Framework;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Stories.Scenes.Xml;
using Nsnbc.Texts;
using Nsnbc.Util;

namespace Nsnbc.Phases.Galleries
{
    public class CharacterGalleryItem : GalleryItem
    {
        public CharacterGalleryItem(string characterName) : base(XmlCharacters.FindArt(characterName, Pose.Speaking),
            G.T(characterName),
            () =>
            {
                Root.PushPhase(new CharacterPosesPhase(characterName));
            })
        {
            Rectangle relevantRectangle = ArtManipulation.GetRelevantRectangle(Library.Art(XmlCharacters.FindArt(characterName, Pose.Speaking)));
            this.SourceRectangle = new Rectangle(relevantRectangle.X, relevantRectangle.Y - 10, relevantRectangle.Width, relevantRectangle.Height + 10);
        }
    }
}