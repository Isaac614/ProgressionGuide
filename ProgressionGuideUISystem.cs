using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;
using ProgressionGuide;
using System.Diagnostics.Eventing.Reader;
using Terraria.ID;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ProgressionGuide.UI
{
    [Autoload(Side = ModSide.Client)]

    public class ProgressionGuideUISystem : ModSystem
    {
        internal MainUIState _mainUIState;
        internal UserInterface _userInterface;
        private GameTime _lastUpdateUIGameTime;
        public static bool IsUIVisible { get; set; }

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
            if (_userInterface?.CurrentState != null && IsUIVisible)
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
                        if (IsUIVisible && _lastUpdateUIGameTime != null && _userInterface?.CurrentState != null)
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
            IsUIVisible = !IsUIVisible;

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText($"UI toggled", Color.Blue);
            }
        }
    }
}