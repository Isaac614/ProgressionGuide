using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;
using ProgressionGuide;

namespace ProgressionGuide.UI
{
    [Autoload(Side = ModSide.Client)]

    public class MainWindowSystem : ModSystem
    {
        internal MainUIState _mainUIState;
        private UserInterface _userInterface;


        public override void Load()
        {

            _mainUIState = new MainUIState();
            _userInterface = new UserInterface();
            _userInterface.SetState(_mainUIState);
        }

        public override void Unload()
        {
            _mainUIState = null;
            _userInterface = null;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _userInterface.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Progression Guide UI",
                    delegate
                    {
                        if (_userInterface?.CurrentState != null)
                        {
                            _userInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI
                ));
            }
        }

        public void ToggleUI()
        {
            if (_userInterface.CurrentState == null)
            {
                _userInterface.SetState(_mainUIState);
            }
            else
            {
                _userInterface.SetState(null);
            }
        }
    }
}