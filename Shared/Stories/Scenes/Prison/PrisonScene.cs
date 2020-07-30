using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.PostSharp;
using Nsnbc.Stories.Sets;
using PostSharp.Community.ToString;

namespace Nsnbc.Stories.Scenes.Prison
{
    [JsonObject(MemberSerialization.Fields), Trace]
    public class PrisonScene : Scene
    {
        public PrisonR1 Guardhouse1;
        public PrisonR2 Guardhouse2;
        public PrisonR3 Guardhouse3;

        public PrisonTableScene Table;
        public FridgeScene Fridge;
        public override IEnumerable<Interactible> Interactibles => ActiveRoom!.Items;

        public override void Begin(Session hardSession)
        {
            base.Begin(hardSession);
            Table = new PrisonTableScene(this);
            Fridge = new FridgeScene();
            Guardhouse1 = new PrisonR1()
            {
                Parent = this
            };
            Guardhouse2 = new PrisonR2()
            {
                Parent = this
            };
            Guardhouse3 = new PrisonR3()
            {
                Parent = this
            };
            Table.Initialize(hardSession);
            Fridge.Initialize(hardSession);
            Guardhouse1.Initialize();
            Guardhouse2.Initialize();
            Guardhouse3.Initialize();
            ActiveRoom = Guardhouse1;
        }

        public override void Draw(AirSession airSession)
        {
            // Room
            ActiveRoom!.Draw(airSession);
            // Minimap
            Texture2D minimap = Library.Art(ArtName.R1_Minimap);
            Primitives.DrawImage(minimap, new Rectangle(Root.Screen.Width - minimap.Width, Root.Screen.Height - minimap.Height, minimap.Width, minimap.Height), Color.White);
            Primitives.DrawImage(Library.Art(ActiveRoom.YouAreHere), new Rectangle(Root.Screen.Width - minimap.Width, Root.Screen.Height - minimap.Height, minimap.Width, minimap.Height), Color.White);
        }

        public override Scene? FindExistingScene(SceneName name)
        {
            return name switch
            {
                SceneName.R1_Table => Table,
                SceneName.R1_Fridge => Fridge,
                SceneName.R1_Suplik2 => Table.Drawer,
                _ => null
            };
        }
    }
}