using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProgressionGuide
{
    public class SearchEngine
    {
        private List<ItemData> _allItems;

        public void PopulateItems()
        {
            // _allItems.Clear();
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
            _allItems.Clear();
            _allItems = null;
        }

        public int Search(string searchText)
        {
            foreach (ItemData itemData in _allItems)
            {
                if (itemData.Name.ToLower().Contains(searchText.ToLower()))
                {
                    return itemData.Id;
                }
            }
            return -1;
        }
    }
}