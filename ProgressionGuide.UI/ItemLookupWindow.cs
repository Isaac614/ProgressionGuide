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
        private Item? _item;
        private SearchBar _searchBar;
        private CraftingPanel _craftingPanel;
        private BigItem _bigItem;
        private StatsPanel _statPanel;

        public SearchBar SearchBar
        {
            get { return _searchBar; }
        }

        public ItemLookupWindow(Item item)
        {
            Main.instance.LoadItem(item.type);
            _item = item;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            Width.Set(0, 0.7f);
            Height.Set(0, 0.4f);

            HAlign = 0.5f;
            VAlign = 0.5f;

            BackgroundColor = new Color(45, 60, 100, 220);
            BorderColor = new Color(19, 28, 48);

            _searchBar = new SearchBar();
            _statPanel = new StatsPanel(0.28f, 1f, true, false);
            _craftingPanel = new CraftingPanel(0.28f, 0.85f, true, true);
            _bigItem = new BigItem(0.38f, 1f);

            Append(_searchBar);
            Append(_statPanel);
            Append(_craftingPanel);
            Append(_bigItem);
        }

        public string GetSearchText()
        {
            return _searchBar?.SearchText ?? "";
        }

        public bool GetSearchBarActive()
        {
            return _searchBar.IsActive;
        }

        public void Populate(Item item)
        {
            Clear();
            Main.instance.LoadItem(item.type);
            _craftingPanel.PopulateContent(item);
            _statPanel.PopulateStats(item);
            _bigItem.Populate(item);
        }

        public void Clear()
        {
            _searchBar.ClearSearch();
            _statPanel.Clear();
            _craftingPanel.Clear();
            _bigItem.Clear(); 
        }

        public void Unload()
        {
            _item = null;
            _searchBar.ClearSearch();
            _statPanel.Unload();
            _craftingPanel.Unload();
            _bigItem.Unload();
        }

    }
}