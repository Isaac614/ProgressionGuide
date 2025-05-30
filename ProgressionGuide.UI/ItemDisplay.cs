using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;
using System.Text;
using System.Collections.Generic;
using Terraria.Map;
using Terraria.ID;
using Terraria.ObjectData;


namespace ProgressionGuide.UI
{
    public class ItemDisplay : UIElement
    {
        private Item? _item;
        private string _name;
        protected Texture2D _sprite;
        private int _stack;
        private int _desiredWidth;
        private bool _includeStack = false;
        private Rectangle? _sourceRectangle = null;


        public ItemDisplay(Item item)
        {
            _item = item;
            Main.instance.LoadItem(item.type);
            _name = item.Name;
            _sprite = TextureAssets.Item[item.type].Value;
            _stack = item.stack;
            _desiredWidth = 16;
            OnInitialize();
        }
        public ItemDisplay(Item item, int desiredWidth)
        {
            _item = item;
            Main.instance.LoadItem(item.type);
            _name = item.Name;
            _sprite = TextureAssets.Item[item.type].Value;
            _stack = item.stack;
            _desiredWidth = desiredWidth;
            OnInitialize();
        }
        public ItemDisplay(Item item, bool includeStack)
        {
            _item = item;
            Main.instance.LoadItem(item.type);
            _name = item.Name;
            _sprite = TextureAssets.Item[item.type].Value;
            _stack = item.stack;
            _desiredWidth = 16;
            _includeStack = includeStack;
            OnInitialize();
        }
        public ItemDisplay(Item item, int desiredWidth, bool includeStack)
        {
            _item = item;
            Main.instance.LoadItem(item.type);
            _name = item.Name;
            _sprite = TextureAssets.Item[item.type].Value;
            _stack = item.stack;
            _desiredWidth = desiredWidth;
            _includeStack = includeStack;
            OnInitialize();
        }
        public ItemDisplay(int tileId)
        {
            var tileInfo = GetTileDisplayInfo(tileId);
            _name = tileInfo.Item1;
            _sprite = tileInfo.Item2;
            _sourceRectangle = tileInfo.Item3;
            _desiredWidth = 16;
            OnInitialize();
        }
        public ItemDisplay(int tileId, int desiredWidth)
        {
            var tileInfo = GetTileDisplayInfo(tileId);
            _name = tileInfo.Item1;
            _sprite = tileInfo.Item2;
            _desiredWidth = desiredWidth;
            OnInitialize();
        }


        public override void OnInitialize()
        {
            base.OnInitialize();
            Width.Set(0f, 1f);
            Height.Set(50f, 0f);
        }

        

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            Texture2D pixel = TextureAssets.MagicPixel.Value;

            float iconWidth = _sprite.Width;
            float iconScale = _desiredWidth / iconWidth;

            float maxWidth = GetDimensions().Width - (iconWidth * iconScale) - 10f;
            DynamicSpriteFont font = FontAssets.MouseText.Value;
            List<string> wrappedText;
            if (!_includeStack)
            {
                wrappedText = WrapText(_name, font, maxWidth);
            }
            else
            {
                wrappedText = WrapText($"{_name} x {_stack}", font, maxWidth);
            }
            Height.Set(wrappedText.Count * 25f + 10f, 0f);

            // Gets the area this element occupies on screen
            CalculatedStyle dimensions = GetDimensions();
            Rectangle bounds = dimensions.ToRectangle();
            Vector2 position = dimensions.Position();

            spriteBatch.Draw(pixel, bounds, Color.Red);
            DrawItemIcon(spriteBatch, _sprite, new Vector2(position.X + 5f, position.Y + 5f), _sourceRectangle, iconScale);
            float textVertOffset = 3.5f;
            foreach (string line in wrappedText)
            {
                DrawItemName(spriteBatch, line, new Vector2(position.X + (iconWidth * iconScale) + 10f, position.Y + textVertOffset));
                textVertOffset += 25f;
            }
            DrawBorder(spriteBatch, bounds, Color.Blue, 2);
        }

        private void DrawItemIcon(SpriteBatch spriteBatch, Texture2D itemIcon, Vector2 position, Rectangle? sourceRectangle, float iconScale)
        {
            spriteBatch.Draw(itemIcon, position, sourceRectangle, Color.White, 0f,
            Vector2.Zero, iconScale, SpriteEffects.None, 0f);
        }

        private void DrawItemName(SpriteBatch spriteBatch, string itemName, Vector2 position)
        {
            // var Mod = ModContent.GetInstance<ProgressionGuide>();
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

        private (string name, Texture2D texture, Rectangle sourceRectangle) GetTileDisplayInfo(int tileId)
        {
            TileObjectData tileData = TileObjectData.GetTileData(tileId, 0);
            string name = Lang.GetMapObjectName(MapHelper.TileToLookup(tileId, 0));
            Texture2D texture = TextureAssets.Tile[tileId].Value;

            int width = (tileData?.Width ?? 1) * 16;
            int height = (tileData?.Height ?? 1) * 16;
            Rectangle sourceRectangle = new Rectangle(0, 0, width, height);


            return (name, texture, sourceRectangle);
        }

        private Texture2D CropTexture(Texture2D originalTexture, Rectangle sourceRect)
        {
            // Get the pixel data from the original texture
            Color[] originalData = new Color[originalTexture.Width * originalTexture.Height];
            originalTexture.GetData(originalData);
            
            // Create array for cropped data
            Color[] croppedData = new Color[sourceRect.Width * sourceRect.Height];
            
            // Copy the cropped portion
            for (int y = 0; y < sourceRect.Height; y++)
            {
                for (int x = 0; x < sourceRect.Width; x++)
                {
                    int originalIndex = (sourceRect.Y + y) * originalTexture.Width + (sourceRect.X + x);
                    int croppedIndex = y * sourceRect.Width + x;
                    croppedData[croppedIndex] = originalData[originalIndex];
                }
            }
            
            // Create new texture with cropped data
            Texture2D croppedTexture = new Texture2D(Main.graphics.GraphicsDevice, sourceRect.Width, sourceRect.Height);
            croppedTexture.SetData(croppedData);
            
            return croppedTexture;
        }
        
    }
}