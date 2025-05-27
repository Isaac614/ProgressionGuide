using Terraria;

namespace ProgressionGuide
{
    public class ItemData
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Tooltip { get; set; }
        public int Rarity { get; set; }
        public int Damage { get; set; }
        public string Category { get; set; }
        public bool IsWeapon { get; set; }
        public bool IsAccessory { get; set; }

        public Item CreateItem()
        {
            var item = new Item();
            item.SetDefaults(ItemId);
            return item;
        }
    }
}