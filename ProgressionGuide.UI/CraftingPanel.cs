using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.UI;

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
                string ingredientName = recipe.requiredItem[i].Name;
                int ingredientAmount = recipe.requiredItem[i].stack;
                Texture2D itemIcon = TextureAssets.Item[item.type].Value;
                AddItem(new ItemDisplay(new Item(ItemID.TerraBlade)));
                // AddItem(new IngredientDisplay(ingredientName, ingredientAmount, itemIcon));
            }

            // DisplayContent();
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