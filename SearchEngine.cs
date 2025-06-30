using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProgressionGuide
{
    public class SearchEngine
    {
        private List<ItemData> _allItems = new List<ItemData>();

        public void PopulateItems()
        {
            _allItems = new List<ItemData>();
            for (int i = 1; i < ItemLoader.ItemCount; i++)
            {
                Item item = new Item();
                try
                {
                    item.SetDefaults(i);
                }
                catch
                {
                    continue;
                }

                // skips invalid and empty items
                if (item.type <= ItemID.None || string.IsNullOrEmpty(item.Name))
                    continue;

                _allItems.Add(new ItemData(item.type, item.Name));
            }
        }

        public void Clear()
        {
            _allItems = null;
        }

        public int Search(string searchText)
        {
            foreach (ItemData itemData in _allItems)
            {
                Main.NewText(itemData.Name);
                if (itemData.Name.ToLower().Contains(searchText.ToLower()))
                {
                    // Main.NewText(itemData.Name);
                    return itemData.Id;
                }
            }
            return -1;
        }
    }
}