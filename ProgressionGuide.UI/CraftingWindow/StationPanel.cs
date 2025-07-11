using Terraria;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace ProgressionGuide.UI
{
    public class StationPanel : ScrollablePanel
    {
        public StationPanel(float width, float height, bool bottomAlign,
        bool rightAlign) : base(width, height, bottomAlign, rightAlign)
        { }
        public StationPanel(float width, float height, float top,
        float left) : base(width, height, top, left)
        { }

        public override void OnInitialize()
        {
            base.OnInitialize();
            PaddingBottom = 0f;
            PaddingTop = 0f;
            PaddingLeft = 0f;
            PaddingRight = 0f;
        }

        public void PopulateContent(Recipe recipe)
        {
            foreach (int tileId in recipe.requiredTile)
            {
                int itemId = GetItemThatPlacesTile(tileId);
                ItemDisplay station;
                if (itemId != -1)
                {
                    station = new ItemDisplay(new Item(itemId));
                }
                else
                {
                    station = new ItemDisplay(tileId);
                }

                AddItem(station);
            }
        }

        private int GetItemThatPlacesTile(int tileId)
        {
            for (int i = 1; i < ItemLoader.ItemCount; i++)
            {
                Item item = new Item(i);
                if (item.createTile == tileId)
                {
                    return i; // Return the first item that places this tile
                }
            }
            return -1;
        }
    }
}