using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
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
        private IngredientPanel _ingredientPanel;
        private StationPanel _stationPanel;


        public CraftingPanel(float width, float height, bool bottomAlign,
        bool rightAlign)
        {
            _width = width;
            _height = height;
            _bottomAlign = bottomAlign;
            _rightAlign = rightAlign;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            _stationPanel = new(1f, 0.2f, 0.1f, 0f);
            _ingredientPanel = new(1f, 0.60f, true, false);

            UIPanel stationLabel = new();
            UIPanel ingredientLabel = new();

            stationLabel.Height.Set(0f, 0.1f);
            stationLabel.Width.Set(0f, 1f);
            stationLabel.PaddingTop = 0;
            stationLabel.PaddingBottom = 0;
            stationLabel.BorderColor = new Color(0, 0, 0, 0);

            ingredientLabel.Height.Set(0f, 0.1f);
            ingredientLabel.Width.Set(0f, 1f);
            ingredientLabel.Top.Set(0f, 0.3f);
            ingredientLabel.Top.Set(0f, 0.3f);
            ingredientLabel.PaddingTop = 0;
            ingredientLabel.PaddingBottom = 0;
            ingredientLabel.BorderColor = new Color(0, 0, 0, 0);

            UIText stationLabelText = new UIText("Crafting Station", 1.0f);
            stationLabelText.VAlign = 0.5f;
            stationLabelText.HAlign = 0.5f;

            UIText ingredientLabelText = new UIText("Ingredients", 1.0f);
            ingredientLabelText.VAlign = 0.5f;
            ingredientLabelText.HAlign = 0.5f;

            stationLabel.Append(stationLabelText);
            ingredientLabel.Append(ingredientLabelText);


            Width.Set(0, _width);
            Height.Set(0, _height);
            PaddingLeft = 15f;
            PaddingRight = 15f;
            PaddingTop = 3f;
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

            Append(_stationPanel);
            Append(_ingredientPanel);
            Append(stationLabel);
            Append(ingredientLabel);
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

        public void Clear()
        {
            _ingredientPanel.Clear();
            _stationPanel.Clear();
        }

        public void Unload()
        {
            _ingredientPanel.Unload();
            _stationPanel.Unload();
        }
    }
}