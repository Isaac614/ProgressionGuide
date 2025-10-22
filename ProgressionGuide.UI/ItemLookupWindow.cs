using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using System.Collections.Generic;

namespace ProgressionGuide.UI
{
    public class ItemLookupWindow : UIPanel
    {
        private Item? _item;
        private SearchBar _searchBar;
        private Dropdown _dropdown;
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
            _dropdown = new Dropdown(0.28f, 0.3f);

            // Position dropdown below search bar
            _dropdown.Left.Set(0, 0.72f);
            _dropdown.Top.Set(35, 0f);

            // Subscribe to dropdown item selection
            _dropdown.OnItemSelected += OnDropdownItemSelected;
            
            // Subscribe to search text changes for real-time dropdown updates
            _searchBar.OnSearchTextChanged += OnSearchTextChanged;

            Append(_searchBar);
            Append(_statPanel);
            Append(_craftingPanel);
            Append(_bigItem);
            Append(_dropdown);
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
            _dropdown.Hide();
        }

        private void OnDropdownItemSelected(ItemData itemData)
        {
            // Create item from the selected item data and populate the window
            Item selectedItem = new Item(itemData.Id);
            Populate(selectedItem);
        }

        private void OnSearchTextChanged(string newText)
        {
            // Notify the UI system to update the dropdown
            ProgressionGuideUISystem uiSystem = ModContent.GetInstance<ProgressionGuideUISystem>();
            uiSystem.UpdateSearchDropdown();
        }

        public void UpdateDropdown(List<ItemData> searchResults)
        {
            _dropdown.PopulateItems(searchResults);
        }

        public void HideDropdown()
        {
            _dropdown.Hide();
        }

    }
}