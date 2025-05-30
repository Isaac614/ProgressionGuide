using Terraria;
using System.Collections.Generic;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Terraria.UI;

namespace ProgressionGuide.UI
{
    public class StationPanel : ScrollablePanel
    {
        public StationPanel(float width, float height, bool bottomAlign,
        bool rightAlign) : base(width, height, bottomAlign, rightAlign)
        { }

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

                // TileDisplay station = new TileDisplay(tileId);
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