using Microsoft.Xna.Framework;
using Nsnbc.Auxiliary;
using Nsnbc.Auxiliary.Fonts;
using Nsnbc.Core;
using Nsnbc.Stories.Scenes.Xml;
using Nsnbc.Texts;

namespace Nsnbc.Stories.Scenes.Courtyard
{
    public class HathiScene : XmlScene
    {
        public override bool HideInventory => true;

        public override void Draw(AirSession airSession)
        {
            base.Draw(airSession);
            bool budkaVyresena = airSession.Session.Flags.Contains("R2ElectricityOnline");
            bool mecVyresen = airSession.Session.Flags.Contains("R2MecConnected");
            bool dvereOnline = airSession.Session.Flags.Contains("R2DoorsOnline");
            Writer.DrawString((mecVyresen && budkaVyresena) ?
                    
                    (
                        dvereOnline ?
                    G.T("Dveře: {Green}{b}OTEVŘENY{/b}{/green}\nEnergie: {Green}{b}OK{/b}{/Green}\nDoporučená akce: {b}Zahrajte si Hru věrnosti.{/b}")  :
                    G.T("Dveře: {Red}{b}ZAVŘENY{/b}{/red}\nEnergie: {Green}{b}OK{/b}{/Green}\nDoporučená akce: {b}Odemkněte dveře.{/b}")  
                    
                    )
                    :
                    G.T("Dveře: {Red}{b}ZAVŘENY{/b}{/red}\nEnergie: {Red}{b}MINIMÁLNÍ{/b}{/red}\nDoporučená akce: {b}Zapojte Hru věrnosti do elektrické sítě.{/b}"), 
                new Rectangle(160,480,800,176), Color.Black, BitmapFontGroup.Main32, Writer.TextAlignment.TopLeft, true);
            Ux.DrawButton(new Rectangle(1090, 100, 500, 100), G.T("O zámku Duchov"), () =>
            {
                airSession.Enqueue(this.StorageScripts["HathiDuchov"]);
            });   
            Ux.DrawButton(new Rectangle(1090, 200, 500, 100), G.T("O projektu Džungle"), () =>
            {
                airSession.Enqueue(this.StorageScripts["HathiDzungle"]);
            });   
            Ux.DrawButton(new Rectangle(1090, 300, 500, 100), G.T("O Hře věrnosti"), () =>
            {
                airSession.Enqueue(this.StorageScripts["HathiVernost"]);
            });   
            Ux.DrawButton(new Rectangle(1090, 400, 500, 100), G.T("Odemknout dveře"), () =>
            {
                airSession.Enqueue(this.StorageScripts["HathiUnlockDoors"]);
            });   
            Ux.DrawButton(new Rectangle(1090, 500, 500, 100), G.T("Otevřít dveře"), () =>
            {
                airSession.Enqueue(this.StorageScripts["HathiOpenDoors"]);
            });   
        }
    }
}