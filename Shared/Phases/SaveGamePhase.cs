using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nsnbc.Auxiliary;
using Nsnbc.Core;
using Nsnbc.Phases.Galleries;
using Nsnbc.Services;
using Nsnbc.Texts;

namespace Nsnbc.Phases
{
    public class SaveGamePhase : TabbedPhase
    {
        private readonly MainLoop mainLoop;
        private List<SaveLoadGalleryItem> picturesAsField;

        public SaveGamePhase(MainLoop mainLoop) : base(G.T("Uložit hru"))
        {
            this.mainLoop = mainLoop;
        }

        protected internal override void Initialize(Game game)
        {
            base.Initialize(game);
            picturesAsField = SaveGamePhase.CreateSaveLoadItems((slot) =>
            {
             
                Texture2D screenshot = CreateScreenshot();
                if (picturesAsField[slot].Empty)
                {
                    SaveTo(screenshot, slot);
                }
                else
                {
                    ConfirmationPhase.Confirm(G.T("Přepsat existující uloženou pozici?"), () =>
                    {
                        SaveTo(screenshot, slot);
                    });
                }
            });
            Tabs.Add(new Tab(G.T("Uložit hru"), (r) =>
            {
                Ux.DrawGallery(r, picturesAsField);
            }));
            SelectedTab = Tabs[0];
        }

        private void SaveTo(Texture2D screenshot, int slot)
        {
            var saveTask = Task.Run(() => { SaveLoad.SaveGame(mainLoop.AirSession.Session, screenshot, slot); });
            Root.PushPhase(new SavingInProgressPhase(saveTask, () => Root.PopFromPhase()));
        }

        public static List<SaveLoadGalleryItem> CreateSaveLoadItems(Action<int> withSlot)
        {
            List<SaveLoadGalleryItem> items = new List<SaveLoadGalleryItem>();
            for (int i = 0; i < 20; i++)
            {
                items.Add(null!);
            }
            foreach (SavedGameWithScreenshot savedGameWithScreenshot in SaveLoad.GetSavedGames())
            {
                items[savedGameWithScreenshot.SlotNumber] = new SaveLoadGalleryItem(savedGameWithScreenshot.Screenshot, 
                    GString.Pure(savedGameWithScreenshot.SavedGame.Caption), () => withSlot(savedGameWithScreenshot.SlotNumber), false);
            }
            for (var i = 0; i < items.Count; i++)
            {
                int j = i;
                if (items[i] == null)
                {
                    items[i] = new SaveLoadGalleryItem(Library.Art(ArtName.Pixel), G.T("prázdná pozice"), () => withSlot(j), true);
                }
            }
            return items;
        }

        private Texture2D CreateScreenshot()
        {
            GraphicsDevice gd = Root.Graphics.GraphicsDevice;
            RenderTarget2D renderTarget = new RenderTarget2D(
                gd,
                gd.PresentationParameters.BackBufferWidth,
                gd.PresentationParameters.BackBufferHeight,
                false,
                gd.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24);

            gd.SetRenderTarget(renderTarget);
            ((CommonGame)Root.Game).WrapInSpriteBatch(() => mainLoop.Draw(0));
            gd.SetRenderTarget(null);
            return renderTarget;
        }
    }
}