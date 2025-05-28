using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;

// using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.UI;

namespace ProgressionGuide.UI
{
    public class ItemDisplay : UIElement
    {
        private Item _item;

        public ItemDisplay()
        {
            Width.Set(0f, 1f); // Full width of container
            Height.Set(50f, 0f);
        }


        public ItemDisplay(Item item)
        {

            _item = item;

            Width.Set(0f, 1f); // Full width of container
            Height.Set(30f, 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            // Gets the area this element occupies on screen
            CalculatedStyle dimensions = GetDimensions();
            Rectangle bounds = dimensions.ToRectangle();
            Vector2 position = dimensions.Position();

            Texture2D pixel = TextureAssets.MagicPixel.Value;
            float iconScale = 0.4f;

            // Gets item's texture/icon
            Main.instance.LoadItem(_item.type);
            Texture2D itemIcon = TextureAssets.Item[_item.type].Value;
            float iconWidth = itemIcon.Width;
            string itemName = _item.Name;

            spriteBatch.Draw(pixel, bounds, Color.Red);
            DrawItemIcon(spriteBatch, itemIcon, position, iconScale);
            DrawItemName(spriteBatch, itemName, new Vector2(position.X + iconWidth, position.Y + 3.5f));
            DrawBorder(spriteBatch, bounds, Color.Blue, 2);
        }

        protected void DrawItemIcon(SpriteBatch spriteBatch, Texture2D itemIcon, Vector2 position, float iconScale)
        {
            Vector2 iconPosition = position + new Vector2(5f, 3f);

            spriteBatch.Draw(itemIcon, iconPosition, null, Color.White, 0f,
            Vector2.Zero, iconScale, SpriteEffects.None, 0f);
        }

        protected void DrawItemName(SpriteBatch spriteBatch, string itemName, Vector2 position)
        {
            DynamicSpriteFont font = FontAssets.MouseText.Value;
            spriteBatch.DrawString(font, itemName, position, Color.White);
        }

        protected void DrawBorder(SpriteBatch spriteBatch, Rectangle hitbox, Color borderColor, int thickness)
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