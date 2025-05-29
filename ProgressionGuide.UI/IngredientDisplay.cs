using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;
using System.Collections.Generic;
namespace ProgressionGuide.UI
{
    public class IngredientDisplay : ItemDisplay
    {
        private Texture2D _icon;
        private string _ingredient;
        private int _amount;


        public string Name
        {
            get { return _ingredient; }
        }

        public int Amount
        {
            get { return _amount; }
        }

        public IngredientDisplay(Item item) : base(item)
        {
            Width.Set(0f, 1f);
            Height.Set(30f, 0);

            _ingredient = item.Name;
            _amount = item.stack;
            // Gets item's texture/icon
            Main.instance.LoadItem(item.type);
            _icon = TextureAssets.Item[item.type].Value;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            Texture2D pixel = TextureAssets.MagicPixel.Value;
            float iconScale = 0.4f;

            
            float iconWidth = _icon.Width;

            float maxWidth = GetDimensions().Width - (iconWidth * iconScale) - 10f;
            DynamicSpriteFont font = FontAssets.MouseText.Value;
            List<string> wrappedText = WrapText($"{_ingredient} x {_amount}", font, maxWidth);
            Height.Set(wrappedText.Count * 25f + 10f, 0f);

            // Gets the area this element occupies on screen
            CalculatedStyle dimensions = GetDimensions();
            Rectangle bounds = dimensions.ToRectangle();
            Vector2 position = dimensions.Position();

            spriteBatch.Draw(pixel, bounds, Color.Red);
            DrawItemIcon(spriteBatch, _icon, new Vector2(position.X + 5f, position.Y + 5f), iconScale);
            float textVertOffset = 3.5f;
            foreach (string line in wrappedText)
            {
                DrawItemName(spriteBatch, line, new Vector2(position.X + (iconWidth * iconScale) + 10f, position.Y + textVertOffset));
                textVertOffset += 25f;
            }
            DrawBorder(spriteBatch, bounds, Color.Blue, 2);
        }

    }
}