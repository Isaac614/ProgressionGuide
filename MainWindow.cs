using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;


namespace ProgressionGuide.UI
{
    public class MainWindow : UIPanel
    {
        public SearchBar searchBar;

        public MainWindow()
        {
            Width.Set(400f, 0f);
            Height.Set(200f, 0f);

            HAlign = 0.5f;
            VAlign = 0.5f;

            BackgroundColor = new Color(45, 60, 100, 220);
            BorderColor = new Color(80, 110, 180, 255);
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            // UIText titleText = new UIText("My UI", 1.2f, true);
            // titleText.HAlign = 0.5f;
            // titleText.Top.Set(10, 0);
            // Append(titleText);

            searchBar = new SearchBar();
            Append(searchBar);
            Main.NewText("MainWindow: SearchBar instance created and appended!");
        }
    }
}