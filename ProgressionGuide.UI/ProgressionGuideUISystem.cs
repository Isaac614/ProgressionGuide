using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;
using Terraria.ID;

namespace ProgressionGuide.UI
{
    [Autoload(Side = ModSide.Client)]

    public class ProgressionGuideUISystem : ModSystem
    {
        internal MainUIState _mainUIState;
        internal UserInterface _userInterface;
        private GameTime _lastUpdateUIGameTime;
        private SearchEngine _searchEngine;
        public bool IsUIVisible { get; set; }

        public override void OnWorldUnload()
        {
            base.OnWorldUnload();

            // Set UI visibility first
            IsUIVisible = false;

            // Safely dispose UI components with null checks
            if (_mainUIState != null)
            {
                _mainUIState.mainWindow?.Unload(); // Safe null check
                _userInterface?.SetState(null); // Clear state before disposal
                
                _mainUIState.Deactivate();
                _mainUIState.Clear();
                _mainUIState = null;
            }

            if (_userInterface != null)
            {
                _userInterface.SetState(null); // Ensure state is cleared
                _userInterface = null;
            }

            // Safely dispose search engine
            if (_searchEngine != null)
            {
                _searchEngine.Clear();
                _searchEngine = null;
            }
        }

        public override void OnWorldLoad()
        {
            base.OnWorldLoad();
            // Ensure clean state (defensive programming)
            if (_userInterface != null || _mainUIState != null)
            {
                OnWorldUnload(); // Clean up any existing state
            }

            _userInterface = new UserInterface();
            _mainUIState = new MainUIState();
            _mainUIState.Activate();
            _userInterface.SetState(_mainUIState);

            _searchEngine = new SearchEngine();
            _searchEngine.PopulateItems();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUIGameTime = gameTime;
            if (IsUIVisible)
            {
                if (_userInterface?.CurrentState == null)
                {
                    _userInterface.SetState(_mainUIState);
                }
                _userInterface.Update(gameTime);
            }
            else
            {
                _userInterface.SetState(null);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ProgressionGuide: Interface",
                    delegate
                    {
                        if (IsUIVisible && _lastUpdateUIGameTime != null && _userInterface?.CurrentState != null)
                        {
                            _userInterface.Draw(Main.spriteBatch, _lastUpdateUIGameTime);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
        }

        public void ToggleUI()
        {
            IsUIVisible = !IsUIVisible;
        }

        public void PopulateItemLookupWindow(Item item)
        {
            _mainUIState.mainWindow.Populate(item);
        }


        public string GetSearchText()
        {
            return _mainUIState?.GetSearchText() ?? "";
        }

        public bool GetSearchBarActive()
        {
            return _mainUIState.GetSearchBarActive();
        }

        public void Search()
        {
            int itemId = _searchEngine.Search(GetSearchText());
            PopulateItemLookupWindow(new Item(itemId));
        }
    }
}