// using System.Collections.Generic;
// namespace ProgressionGuide
// {
//     public class SearchEngine
//     {
//         private List<ItemData> _allItems = new List<ItemData>();
//         private bool _isInitialized = false;

//         public void Initialize()
//         {
//             // if (_isInitialized) return;

//             // PopulateItems();
//             // _isInitialized = true;
//         }

//         public void PopulateItems()
//         {
//             // _allItems = new List<ItemData>();
//             // for (int i = 1; i < ItemLoader.ItemCount; i++)
//             // {
//             //     Item item = new Item();
//             //     try
//             //     {
//             //         item.SetDefaults(i);
//             //     }
//             //     catch
//             //     {
//             //         continue;
//             //     }

//             //     // skips invalid and empty items
//             //     if (item.type <= ItemID.None || string.IsNullOrEmpty(item.Name))
//             //         continue;

//             //     _allItems.Add(new ItemData(item.type, item.Name));
//             // }
//         }

//         public void Clear()
//         {
//             // _allItems = null;
//             return;
//         }

//         public int Search(string searchText)
//         {
//             // foreach (ItemData itemData in _allItems)
//             // {
//             //     if (itemData.Name.Contains(searchText))
//             //     {
//             //         Main.NewText(itemData.Name);
//             //         return itemData.Id;
//             //     }
//             // }
//             return -1;
//         }
//     }
// }