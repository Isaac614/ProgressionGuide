using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace ProgressionGuide.UI
{
    public class ItemLookupWindow : UIPanel
    {
        private SearchBar searchBar;
        private ScrollablePanel infoPanel;
        private CraftingPanel craftingPanel;

        public SearchBar SearchBar
        {
            get { return searchBar; }
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            Width.Set(0, 0.7f);
            Height.Set(0, 0.4f);

            HAlign = 0.5f;
            VAlign = 0.5f;

            BackgroundColor = new Color(45, 60, 100, 220);
            BorderColor = new Color(80, 110, 180, 255);

            searchBar = new SearchBar();
            infoPanel = new ScrollablePanel(0.3f, 0.85f, true, false);
            craftingPanel = new CraftingPanel(0.3f, 0.85f, true, true, new Item(ItemID.TerraBlade));


            Append(searchBar);
            Append(infoPanel);
            Append(craftingPanel);
        }


        public string GetSearchText()
        {
            return searchBar?.SearchText ?? "";
        }

        public void PopulateCraftingInfo()
        {
            craftingPanel.PopulateContent(new Item(ItemID.TerraBlade));
        }

    }
}