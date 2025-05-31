using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace ProgressionGuide.UI
{
    public class ItemLookupWindow : UIPanel
    {
        private Item _item;
        private SearchBar _searchBar;
        private CraftingPanel _craftingPanel;
        private UIPanel _bigItem;
        private StatsPanel _infoPanel;

        public SearchBar SearchBar
        {
            get { return _searchBar; }
        }

        public ItemLookupWindow(Item item)
        {
            _item = item;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            Main.instance.LoadItem(_item.type);

            Width.Set(0, 0.7f);
            Height.Set(0, 0.4f);

            HAlign = 0.5f;
            VAlign = 0.5f;

            BackgroundColor = new Color(45, 60, 100, 220);
            BorderColor = new Color(19, 28, 48);

            _searchBar = new SearchBar();
            _infoPanel = new StatsPanel(0.28f, 1f, true, false, _item);
            _craftingPanel = new CraftingPanel(0.28f, 0.85f, true, true, _item);
            _bigItem = new BigItem(_item.Name, TextureAssets.Item[_item.type].Value, 0.38f, 1f);

            Append(_searchBar);
            Append(_infoPanel);
            Append(_craftingPanel);
            Append(_bigItem);
        }


        public string GetSearchText()
        {
            return _searchBar?.SearchText ?? "";
        }

        public void PopulateCraftingInfo()
        {
            _craftingPanel.PopulateContent();
        }

        public void PopulateStatsWindow()
        {
            _infoPanel.PopulateStats(new Item(ItemID.TerraBlade));
        }

    }
}