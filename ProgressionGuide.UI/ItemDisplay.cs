using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;
using System.Text;
using System.Collections.Generic;


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

            Texture2D pixel = TextureAssets.MagicPixel.Value;
            float iconScale = 0.4f;

            // Gets item's texture/icon
            Main.instance.LoadItem(_item.type);
            Texture2D itemIcon = TextureAssets.Item[_item.type].Value;
            float iconWidth = itemIcon.Width;
            string itemName = _item.Name;

            float maxWidth = GetDimensions().Width - (iconWidth * iconScale) - 10f;
            DynamicSpriteFont font = FontAssets.MouseText.Value;
            List<string> wrappedText = WrapText(itemName, font, maxWidth);
            Height.Set(wrappedText.Count * 25f + 10f, 0f);

            // Gets the area this element occupies on screen
            CalculatedStyle dimensions = GetDimensions();
            Rectangle bounds = dimensions.ToRectangle();
            Vector2 position = dimensions.Position();

            spriteBatch.Draw(pixel, bounds, Color.Red);
            DrawItemIcon(spriteBatch, itemIcon, new Vector2(position.X + 5f, position.Y + 5f), iconScale);
            float textVertOffset = 3.5f;
            foreach (string line in wrappedText)
            {
                DrawItemName(spriteBatch, line, new Vector2(position.X + (iconWidth * iconScale) + 10f, position.Y + textVertOffset));
                textVertOffset += 25f;
            }
            DrawBorder(spriteBatch, bounds, Color.Blue, 2);
        }

        protected void DrawItemIcon(SpriteBatch spriteBatch, Texture2D itemIcon, Vector2 position, float iconScale)
        {
            spriteBatch.Draw(itemIcon, position, null, Color.White, 0f,
            Vector2.Zero, iconScale, SpriteEffects.None, 0f);
        }

        protected void DrawItemName(SpriteBatch spriteBatch, string itemName, Vector2 position)
        {
            // var Mod = ModContent.GetInstance<ProgressionGuide>();
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


        protected List<string> WrapText(string text, DynamicSpriteFont font, float lineLength)
        {
            float strLen = font.MeasureString(text).X;
            bool longer = strLen >= lineLength;
            List<string> wrappedText = new List<string>();
            if (longer)
            {
                string[] words = text.Split(' ');
                StringBuilder currentLine = new StringBuilder();

                for (int i = 0; i < words.Length; i++)
                {
                    string word = words[i];

                    if (currentLine.Length == 0) // ie if current line is empty bcause we on first word
                    {
                        currentLine.Append(word);
                    }
                    else
                    {
                        string potentialLine = $"{currentLine.ToString()} {word}";
                        if (font.MeasureString(potentialLine).X <= lineLength)
                        {
                            currentLine.Append(" ");
                            currentLine.Append(word);
                        }
                        else
                        {
                            wrappedText.Add(currentLine.ToString());
                            currentLine.Clear();
                            currentLine.Append(word);
                        }
                    }
                }

                if (currentLine.Length > 0)
                {
                    wrappedText.Add(currentLine.ToString());
                }
            }
            else
            {
                wrappedText.Add(text);
                return wrappedText;
            }
            return wrappedText;
        }

    }
}