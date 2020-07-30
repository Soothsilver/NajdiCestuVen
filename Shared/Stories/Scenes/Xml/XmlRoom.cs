using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.Core;

namespace Nsnbc.Stories.Scenes.Xml
{
    [JsonObject(MemberSerialization.Fields)]
    public class XmlRoom : Room
    {
        public override void Draw(AirSession airSession)
        {
            foreach (ArtName background in Backgrounds)
            {
                Primitives.DrawZoomed(Library.Art(background), Parent.CurrentZoom);
            }

            DrawDirectionButton(ArtName.Left128Disabled, ArtName.Left128,
                new Rectangle(Root.Screen.Width/2-64-128, Root.Screen.Height-128,128,128), Directions.Left, airSession);
            DrawDirectionButton(ArtName.TurnAround128Disabled, ArtName.TurnAround128,
                new Rectangle(Root.Screen.Width / 2 - 64, Root.Screen.Height - 128, 128, 128), Directions.Turnaround, airSession);
            DrawDirectionButton(ArtName.Right128Disabled, ArtName.Right128,
                new Rectangle(Root.Screen.Width/2-64+128, Root.Screen.Height-128,128,128), Directions.Right, airSession);
        }

        private void DrawDirectionButton(ArtName noMouseOver, ArtName mouseOver, Rectangle rectangle, DirectionButton directionButton, AirSession airSession)
        {
            if (directionButton != null)
            {
                Ux.DrawImageButton(noMouseOver, mouseOver, rectangle, () => airSession.Enqueue(directionButton.Script));
            }
        }

        public ArtName MinimapLocal { get; set; }
        public override ArtName YouAreHere => MinimapLocal;
        public List<ArtName> Backgrounds { get; set; } = new List<ArtName>();
        public Directions Directions { get; set; } = new Directions();
    }
}