// using System.ComponentModel;
// using Terraria;
// using Terraria.GameContent.Creative;
// using Terraria.ModLoader;

// namespace ProgressionGuide
// {
//     public class SearchEngine
//     {
//         private List<ItemData> _allItems = new List<ItemData>();
//         private bool _isInitialized = false;

//         // public void Initialize()
//         // {
//         //     if (_isInitialized) return;

//         //     PopulateItems();
//         //     _isInitialized = true;
//         // }

//         public void PopulateItems()
//         {
//             for (int i = 1; i < ItemLoader.ItemCount; i++)
//             {
//                 Item item = new Item();
//                 item.SetDefaults();

//                 // skips invalid and empty items
//                 if (item.type <= 0 || string.IsNullOrEmpty(item.Name))
//                     continue;

//                 _allItems.Add(new ItemData
//                 {
//                     ItemId = item.type,
//                     Name = item.Name,
//                     Tooltip = item.ToolTip.ToString(), // TODO - make sure this works
//                     Rarity = item.rare,
//                     Damage = item.damage,
//                     // Category = GetItemCategory(item), TODO - fix this; this func doessn't exist
//                     IsWeapon = item.damage > 0,
//                     IsAccessory = item.accessory,
//                 });
//             }
//         }
//     }
// }