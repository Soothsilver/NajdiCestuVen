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
    public class LoadGamePhase : TabbedPhase
    {
        private List<SaveLoadGalleryItem> picturesAsField = null!;

        public LoadGamePhase() : base(G.T("Načíst hru"))
        {
        }

        protected internal override void Initialize(Game game)
        {
            base.Initialize(game);
            picturesAsField = SaveGamePhase.CreateSaveLoadItems((j) =>
            {
                if (picturesAsField[j].Empty)
                {
                    // Do nothing. This cannot be loaded.
                    return;
                }
                Session hardSession = SaveLoad.LoadGame(j);
                    
                // Pop everything down except main menu
                foreach (var phase in Root.PhaseStack.Reverse<GamePhase>())
                {
                    if (!phase.ScheduledForElimination && !(phase is MainMenuPhase))
                    {
                        phase.Destruct(Root.Game);
                    }
                }
                    
                Root.PushPhase(new SessionPhase(SessionLoader.LoadFromHardSession(hardSession)));
            });
            
            Tabs.Add(new Tab(G.T("Načíst hru"), (r) =>
            {
                Ux.DrawGallery(r, picturesAsField);
            }));
            SelectedTab = Tabs[0];
        }
    }
}