using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProgressionGuide
{
    public class SearchEngine
    {

        public SearchEngine()
        {
            _allItems = new();
        }

        // This needs to be type object; if you make it type ItemData it crashes on build and reload. 
        private List<object> _allItems;

        public void PopulateItems()
        {
            _allItems.Clear();
            _allItems = new();
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

        public List<ItemData> SearchMultiple(string searchText, int maxResults = 10)
        {
            List<ItemData> results = new List<ItemData>();
            
            if (string.IsNullOrEmpty(searchText))
                return results;

            string lowerSearchText = searchText.ToLower();
            
            foreach (ItemData itemData in _allItems)
            {
                if (itemData.Name.ToLower().Contains(lowerSearchText))
                {
                    results.Add(itemData);
                    if (results.Count >= maxResults)
                        break;
                }
            }
            
            return results;
        }
    }
}