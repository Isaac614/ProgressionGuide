using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;

namespace ProgressionGuide.UI
{
    public class MainUIState : UIState
    {
        public MainWindow mainWindow; // This should be public


        public override void OnInitialize()
        {
            mainWindow = new MainWindow();
            Append(mainWindow);
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