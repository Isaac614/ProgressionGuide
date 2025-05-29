using Terraria;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;

namespace ProgressionGuide.UI
{
    public class StationPanel : ScrollablePanel
    {
        public StationPanel(float width, float height, bool bottomAlign,
        bool rightAlign) : base(width, height, bottomAlign, rightAlign)
        { }

        public void PopulateContent(Recipe recipe)
        {
            foreach (int id in recipe.requiredTile)
            {
                ItemDisplay station = new ItemDisplay(new Item(id));
                AddItem(station);
            }
        }
    }
}