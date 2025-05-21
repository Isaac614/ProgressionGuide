using Terraria;
using Terraria.UI;

namespace ProgressionGuide.UI
{
    public class MainUIState : UIState
    {
        public MainWindow mainWindow; // This should be public


        public override void OnInitialize()
        {
            base.OnInitialize();
            mainWindow = new MainWindow();
            Append(mainWindow);
        }
    }
}