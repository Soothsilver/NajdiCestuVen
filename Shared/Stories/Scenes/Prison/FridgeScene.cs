using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Events;
using Nsnbc.PostSharp;
using Nsnbc.Texts;

namespace Nsnbc.Stories.Scenes.Prison
{
    [JsonObject(MemberSerialization.Fields), Trace]
    public class FridgeScene : Scene
    {
        public override IEnumerable<Interactible> Interactibles => Items;
        public List<Interactible> Items { get; set; } = new List<Interactible>();
            
        private VisibleInteractible Triska;
        private VisibleInteractible Figurka;
        public VisibleInteractible Led;
        
        public override void Begin(Session hardSession)
        {
            
            base.Begin(hardSession);
        }
        
        public override void Draw(AirSession airSession)
        {
            Primitives.DrawZoomed(Library.Art(ArtName.Lednicka), this.CurrentZoom);
            Triska.Draw(this.CurrentZoom);
            Led.Draw(this.CurrentZoom);
            Figurka.Draw(this.CurrentZoom);
            
            Ux.DrawImageButton(ArtName.TurnAround128Disabled, ArtName.TurnAround128, new Rectangle(Root.Screen.Width / 2 - 64, Root.Screen.Height - 128, 128, 128), () => { airSession.Enqueue(new QPopScene()); });
        }

        public override void AfterPop(AirSession airSession)
        {           
            airSession.Enqueue(new QZoomInto(CommonGame.R1920x1080, 0.1f));
        }

        public void Initialize(Session hardSession)
        {
            
            Script scriptFigurka = new Script();
            Items.Add(Figurka = new VisibleInteractible(new Rectangle(464,0,235,210), ArtName.R1_Led_Figurka, scriptFigurka, G.T("TODO") ));
            Script scriptLed= new Script();
            Items.Add(Led = new VisibleInteractible(new Rectangle(940,398,416,200), ArtName.R1_Led_Led, scriptLed, G.T("TODO") )
            {
                OnItemUse = new KnownCode(KnownCodes.Led)
            });
            Script scriptTriska = new Script();
            Items.Add(Triska = new VisibleInteractible(new Rectangle(474,866,244,182), ArtName.R1_Led_Triska, scriptTriska, G.T("TODO") ));
            scriptFigurka.Events.AddRange(new QEvent[]
            {
                new QSpeak("", "Beru to", ArtName.Null, SpeakerPosition.Left),
                new QMakeInvisible(Figurka),
                new QAddToInventory(ArtName.R1Figurka)
            });  
            scriptLed.Events.AddRange(new QEvent[]
            {
                new QSpeak("", "Beru to??? Asi ne...", ArtName.Null, SpeakerPosition.Left)
            }); 
            scriptTriska.Events.AddRange(new QEvent[]
            {
                new QSpeak("", "Beru to", ArtName.Null, SpeakerPosition.Left),
                new QMakeInvisible(Triska),
                new QAddToInventory(ArtName.R1Triska)
            });
        }
    }
}