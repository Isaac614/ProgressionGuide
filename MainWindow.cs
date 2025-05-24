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

            // Left.Set((Main.screenWidth) / 2, 0f);
            // Top.Set((Main.screenHeight) / 2, 0f);

            HAlign = 0.5f;
            VAlign = 0.5f;

            BackgroundColor = new Color(45, 60, 100, 220);
            BorderColor = new Color(80, 110, 180, 255);
        }

        public string GetSearchText()
        {
            return searchBar?.SearchText ?? "";
        }

        public bool IsSearchBarActive()
        {
            return searchBar?.IsActive ?? false;
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
        }


    }
}