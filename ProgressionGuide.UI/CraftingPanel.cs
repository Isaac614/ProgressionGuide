using Microsoft.Xna.Framework;
using Terraria;

namespace ProgressionGuide.UI
{
    public class CraftingPanel : ScrollablePanel
    {
        public CraftingPanel(float width, float height, bool bottomAlign,
        bool rightAlign, Item item) : base(width, height, bottomAlign, rightAlign)
        {
            PopulateContent(item);
        }

        public void PopulateContent(Item item)
        {
            Recipe recipe = null;
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                recipe = Main.recipe[i];
                if (recipe.createItem.type == item.type)
                {
                    break;
                }
            }

            for (int i = 0; i < recipe.requiredItem.Count; i++)
            {
                IngredientDisplay ingredient = new IngredientDisplay(recipe.requiredItem[i]);
                AddItem(ingredient);
            }

            DisplayContent();
        }

        public void DisplayContent()
        {
            foreach (IngredientDisplay element in _contentList)
            {
                Main.NewText($"{element.Name} x {element.Amount}", Color.White);
            }
        }

    }
}