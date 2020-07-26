using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Auxiliary;
using Nsnbc.Core;

namespace Nsnbc.Stories.Scenes
{
    public class PrisonScene : Scene
    {
        public List<Room> Rooms = new List<Room>();
        public Room ActiveRoom;

        public override void Begin(Session hardSession)
        {
            base.Begin(hardSession);
            Room r1 = new PrisonR1(this);
            ActiveRoom = r1;
        }

        public override void Draw(AirSession airSession)
        {
            // Room
            ActiveRoom.Draw(airSession);
            // Minimap
            Texture2D minimap = Library.Art(ArtName.R1_Minimap);
            Primitives.DrawImage(minimap, new Rectangle(Root.Screen.Width - minimap.Width, Root.Screen.Height - minimap.Height, minimap.Width, minimap.Height), Color.White);
            Primitives.DrawImage(Library.Art(ActiveRoom.YouAreHere), new Rectangle(Root.Screen.Width - minimap.Width, Root.Screen.Height - minimap.Height, minimap.Width, minimap.Height), Color.White);
        }
    }

    public class PrisonR1 : Room
    {
        private readonly PrisonScene parent;

        public PrisonR1(PrisonScene parent)
        {
            this.parent = parent;
        }
        
        public override void Draw(AirSession airSession)
        {
            Primitives.DrawZoomed(Library.Art(ArtName.Guardroom1), parent.CurrentZoom);
            Primitives.DrawZoomed(Library.Art(ArtName.Guardroom1LockedGate), parent.CurrentZoom);
        }
        public override ArtName YouAreHere => ArtName.R1_G1_MinimapIcon;
    }
}