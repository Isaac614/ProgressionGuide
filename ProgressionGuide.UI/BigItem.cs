using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace ProgressionGuide.UI
{
    public class BigItem : UIPanel
    {
        private float _width;
        private float _height;
        private string? _name;
        private Texture2D? _sprite;


        public BigItem(float width, float height)
        {
            _width = width;
            _height = height;
        }

        public void Populate(Item item)
        {
            _name = item.Name;
            _sprite = TextureAssets.Item[1521].Value;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            Width.Set(0f, _width);
            Height.Set(0f, _height);
            Top.Set(0f, 0f);
            Left.Set(0f, 0.3f);
            PaddingBottom = 10f;
            PaddingLeft = 10f;
            PaddingTop = 10f;
            PaddingRight = 10f;
            BorderColor = new Color(19, 28, 48);
            BackgroundColor = new Color(217, 220, 214);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            if (_name != null)
            {
                float fontScale = 2.0f;
                float fontHeight = FontAssets.MouseText.Value.MeasureString("_name").Y * fontScale;

                // Gets the area this element occupies on screen
                CalculatedStyle dimensions = GetDimensions();
                Vector2 position = dimensions.Position();

                float maxWidth = dimensions.Width - (PaddingLeft + PaddingRight) - 5f;
                DynamicSpriteFont font = FontAssets.MouseText.Value;
                List<string> wrappedText;
                wrappedText = WrapText(_name, font, maxWidth, fontScale);

                // Draw/write text
                float textVertOffset = PaddingTop;
                for (int i = 0; i < wrappedText.Count; i++)
                {
                    float textHorzOffset = (dimensions.Width - FontAssets.MouseText.Value.MeasureString(wrappedText[i]).X * fontScale) / 2f;
                    DrawItemName(spriteBatch, wrappedText[i], new Vector2(position.X + textHorzOffset, position.Y + textVertOffset),
                    new Color(58, 124, 165), fontScale);
                    textVertOffset += fontHeight;
                }

                float iconScale = (dimensions.Height - PaddingTop - (wrappedText.Count * fontHeight)) / _sprite.Height * 0.8f;
                float iconVertOffset = PaddingTop + (wrappedText.Count * fontHeight);
                float iconHorzOffset = (dimensions.Width - _sprite.Width * iconScale) / 2f;
                // Draw icon
                DrawItemIcon(spriteBatch, _sprite, new Vector2(position.X + iconHorzOffset, position.Y + iconVertOffset), null, iconScale);
            }

        }

        private void DrawItemIcon(SpriteBatch spriteBatch, Texture2D itemIcon, Vector2 position, Rectangle? sourceRectangle, float iconScale)
        {

            spriteBatch.Draw(itemIcon, position, sourceRectangle, Color.White, 0f,
            Vector2.Zero, iconScale, SpriteEffects.None, 0f);
        }

        private void DrawItemName(SpriteBatch spriteBatch, string itemName, Vector2 position, Color color, float scale)
        {
            // var Mod = ModContent.GetInstance<ProgressionGuide>();
            DynamicSpriteFont font = FontAssets.MouseText.Value;
            spriteBatch.DrawString(
                font,
                itemName,
                position,
                color,
                0f,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        private List<string> WrapText(string text, DynamicSpriteFont font, float lineLength, float scale)
        {
            float strLen = font.MeasureString(text).X * scale;
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
                        if (font.MeasureString(potentialLine).X * scale <= lineLength)
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

        public void Clear()
        {
            _name = null;
            _sprite = null;
        }

        public void Unload()
        {
            _sprite = null;
        }
    }
}