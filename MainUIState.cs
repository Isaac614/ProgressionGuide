using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;

namespace ProgressionGuide.UI
{
    public class MainUIState : UIState
    {
        public MainWindow mainWindow;


        public override void OnInitialize()
        {
            mainWindow = new MainWindow();
            Append(mainWindow);
        }

        public string GetSearchText()
        {
            return mainWindow?.GetSearchText() ?? "";
        }

        public bool IsSearchBarActive()
        {
            return mainWindow?.IsSearchBarActive() ?? false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (ContainsPoint(Main.MouseScreen)) // for cursor disappearing
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }
    }
}