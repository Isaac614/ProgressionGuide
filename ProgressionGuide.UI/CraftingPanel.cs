using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace ProgressionGuide.UI
{

    public class CraftingPanel : UIPanel
    {

        private float _width;
        private float _height;
        private bool _bottomAlign;
        private bool _rightAlign;
        private Item _itemToCraft;
        private IngredientPanel _ingredientPanel;
        private StationPanel _stationPanel;


        public CraftingPanel(float width, float height, bool bottomAlign,
        bool rightAlign, Item item)
        {
            _width = width;
            _height = height;
            _bottomAlign = bottomAlign;
            _rightAlign = rightAlign;
            _itemToCraft = item;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            _ingredientPanel = new(1f, 0.70f, true, false);
            _stationPanel = new(1f, 0.20f, false, false);

            Width.Set(0, _width);
            Height.Set(0, _height);
            PaddingLeft = 15f;
            PaddingRight = 15f;
            PaddingTop = 15f;
            PaddingBottom = 15f;

            if (_bottomAlign)
            {
                Top.Set(0, 1.0f - Height.Percent);
            }
            else
            {
                Top.Set(0, 0.0f);
            }

            if (_rightAlign)
            {
                Left.Set(0, 1.0f - Width.Percent);
            }
            else
            {
                Left.Set(0, 0.0f);
            }

            Append(_ingredientPanel);
            Append(_stationPanel);
        }


        public void PopulateContent()
        {
            Recipe recipe = null;
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                recipe = Main.recipe[i];
                if (recipe.createItem.type == _itemToCraft.type)
                {
                    break;
                }
            }

            if (recipe != null)
            {
                _ingredientPanel.PopulateContent(recipe);
                _stationPanel.PopulateContent(recipe);
            }
            else
            {
                // TODO - account for null recipe
            }
        }
    }
}