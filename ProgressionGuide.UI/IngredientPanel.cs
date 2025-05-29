using System.Security.Cryptography.Xml;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.UI;

namespace ProgressionGuide.UI
{
    public class IngredientPanel : ScrollablePanel
    {
        public IngredientPanel(float width, float height, bool bottomAlign,
        bool rightAlign) : base(width, height, bottomAlign, rightAlign)
        { }

        public void PopulateContent(Recipe recipe)
        {
            for (int i = 0; i < recipe.requiredItem.Count; i++)
            {
                IngredientDisplay ingredient = new IngredientDisplay(recipe.requiredItem[i]);
                AddItem(ingredient);
            }
        }
    }
}