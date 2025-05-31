using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace ProgressionGuide.UI
{
    public class StatDisplay : UIPanel
    {
        private Item _item;
        private string _stat;
        private string _value;


        public StatDisplay(string stat, string value)
        {
            _stat = stat;
            _value = value;
            OnInitialize();
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            Width.Set(0f, 1f);
            Height.Set(100f, 0f);
            Top.Set(0f, 0f);
            Left.Set(0f, 0f);
            PaddingBottom = 3f;
            PaddingLeft = 10f;
            PaddingTop = 3f;
            PaddingRight = 10f;
            BorderColor = new Color(19, 28, 48);
            BackgroundColor = new Color(217, 220, 214);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            float maxWidth = GetDimensions().Width - (PaddingLeft + PaddingRight) - 5f;
            DynamicSpriteFont font = FontAssets.MouseText.Value;
            List<string> wrappedText;
            wrappedText = WrapText($"{_stat}: {_value}", font, maxWidth);


            float fontHeight = FontAssets.MouseText.Value.MeasureString("I").Y;
            Height.Set(wrappedText.Count * fontHeight + 10f, 0f);

            // Gets the area this element occupies on screen
            CalculatedStyle dimensions = GetDimensions();
            Vector2 position = dimensions.Position();

            // Draw/write text

            float vertOffset = (Height.Pixels - (wrappedText.Count * fontHeight)) / 2f;
            foreach (string line in wrappedText)
            {
                DrawStatInfo(spriteBatch, line, new Vector2(position.X + PaddingLeft, position.Y + vertOffset));
                vertOffset += fontHeight;
            }

        }

        private void DrawStatInfo(SpriteBatch spriteBatch, string itemName, Vector2 position)
        {
            // var Mod = ModContent.GetInstance<ProgressionGuide>();
            DynamicSpriteFont font = FontAssets.MouseText.Value;
            spriteBatch.DrawString(font, itemName, position, new Color(58, 124, 165));

        }


        private List<string> WrapText(string text, DynamicSpriteFont font, float lineLength)
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