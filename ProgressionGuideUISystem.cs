using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;
using ProgressionGuide;
using System.Diagnostics.Eventing.Reader;
using Terraria.ID;

namespace ProgressionGuide.UI
{
    [Autoload(Side = ModSide.Client)]

    public class ProgressionGuideUISystem : ModSystem
    {
        internal MainUIState _mainUIState;
        internal UserInterface _userInterface;
        private GameTime _lastUpdateUIGameTime;
        public static bool UIIsVisible { get; set; }

        public override void Load()
        {
            _userInterface = new UserInterface();

            _mainUIState = new MainUIState();
            _mainUIState.Activate();
            _userInterface.SetState(_mainUIState);
        }

        public override void Unload()
        {
            _userInterface = null;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUIGameTime = gameTime;
            if (_userInterface?.CurrentState != null && UIIsVisible)
            {
                _userInterface.Update(gameTime);
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
                        if (UIIsVisible && _lastUpdateUIGameTime != null && _userInterface?.CurrentState != null)
                        {
                            _userInterface.Draw(Main.spriteBatch, _lastUpdateUIGameTime);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
        }


        public static void ToggleUI()
        {
            UIIsVisible = !UIIsVisible;

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText($"UI toggled", Color.Yellow);
            }
        }
    }
}