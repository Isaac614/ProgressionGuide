using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;
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


        public IngredientDisplay(string name, int amount, Texture2D? icon)
        {
            Width.Set(0f, 1f);
            Height.Set(30f, 0);

            _ingredient = name;
            _amount = amount;
            _icon = icon;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            // Gets the area this element occupies on screen
            CalculatedStyle dimensions = GetDimensions();
            Rectangle bounds = dimensions.ToRectangle();
            Vector2 position = dimensions.Position();

            float iconWidth = _icon.Width;

            Texture2D pixel = TextureAssets.MagicPixel.Value;
            float iconScale = 1f;

            spriteBatch.Draw(pixel, bounds, Color.Red);
            DrawItemIcon(spriteBatch, _icon, position, iconScale);
            DrawItemName(spriteBatch, _ingredient, new Vector2(position.X + iconWidth, position.Y));
            DrawBorder(spriteBatch, bounds, Color.Blue, 2);
        }

        private void DrawItemIcon(SpriteBatch spriteBatch, Texture2D itemIcon, Vector2 position, float iconScale)
        {
            Vector2 iconPosition = position; //+ new Vector2(5f, 5f);

            spriteBatch.Draw(itemIcon, iconPosition, null, Color.White, 0f,
            Vector2.Zero, iconScale, SpriteEffects.None, 0f);
        }

        private void DrawItemName(SpriteBatch spriteBatch, string itemName, Vector2 position)
        {
            DynamicSpriteFont font = FontAssets.MouseText.Value;
            spriteBatch.DrawString(font, itemName, position, Color.White);
        }

        private void DrawBorder(SpriteBatch spriteBatch, Rectangle hitbox, Color borderColor, int thickness)
        {
            Texture2D pixel = TextureAssets.MagicPixel.Value;

            // Top border
            spriteBatch.Draw(pixel, new Rectangle(hitbox.X, hitbox.Y, hitbox.Width, thickness), borderColor);
            // Bottom border
            spriteBatch.Draw(pixel, new Rectangle(hitbox.X, hitbox.Y + hitbox.Height - thickness, hitbox.Width, thickness), borderColor);
            // Left border
            spriteBatch.Draw(pixel, new Rectangle(hitbox.X, hitbox.Y + thickness, thickness, hitbox.Height - 2 * thickness), borderColor);
            // Right border
            spriteBatch.Draw(pixel, new Rectangle(hitbox.X + hitbox.Width - thickness, hitbox.Y + thickness, thickness, hitbox.Height - 2 * thickness), borderColor);
        }
    }
}