using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace ProgressionGuide.UI
{
    public class Dropdown : UIPanel
    {
        private float _width;
        private float _height;
        private List<DropdownItem> _items;
        private UIScrollbar _scrollbar;
        private UIList _itemList;
        private bool _isVisible;
        private int _maxVisibleItems = 8;
        private float _itemHeight = 30f;
        
        public delegate void ItemSelectedHandler(ItemData itemData);
        public event ItemSelectedHandler OnItemSelected;
        
        public Dropdown(float width, float height)
        {
            _width = width;
            _height = height;
            _items = new List<DropdownItem>();
            _isVisible = false;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            Width.Set(0, _width);
            Height.Set(0, _height);
            
            BackgroundColor = new Color(45, 60, 100, 240);
            BorderColor = new Color(19, 28, 48);
            
            // Add padding to the panel
            PaddingLeft = 2f;
            PaddingRight = 2f;
            PaddingTop = 2f;
            PaddingBottom = 2f;
            
            // Create item list first
            _itemList = new UIList();
            _itemList.Width.Set(0, 1f);
            _itemList.Height.Set(0, 1f);
            _itemList.ListPadding = 2f;
            _itemList.PaddingTop = 2f;
            _itemList.PaddingBottom = 2f;
            _itemList.PaddingLeft = 2f;
            _itemList.PaddingRight = 2f;
            Append(_itemList);
            
            // Create scrollbar
            _scrollbar = new UIScrollbar();
            _scrollbar.Width.Set(20f, 0f);
            _scrollbar.Height.Set(0, 1f);
            _scrollbar.Left.Set(-20f, 1f);
            _scrollbar.VAlign = 0.5f;
            _scrollbar.PaddingLeft = 0;
            _scrollbar.PaddingRight = 0;
            _scrollbar.PaddingTop = 2f;
            _scrollbar.PaddingBottom = 2f;
            
            // Link scrollbar to the list
            _itemList.SetScrollbar(_scrollbar);
            
            // Initially hidden
            _isVisible = false;
        }

        public void Show()
        {
            _isVisible = true;
        }

        public void Hide()
        {
            _isVisible = false;
        }

        public void PopulateItems(List<ItemData> itemDataList)
        {
            _itemList.Clear();
            _items.Clear();
            
            if (itemDataList == null || itemDataList.Count == 0)
            {
                Hide();
                return;
            }
            
            Show();
            
            // Debug output
            Main.NewText($"Populating dropdown with {itemDataList.Count} items");
            
            foreach (ItemData itemData in itemDataList)
            {
                DropdownItem dropdownItem = new DropdownItem(itemData);
                dropdownItem.OnClick += (item) => {
                    OnItemSelected?.Invoke(item);
                    Hide();
                };
                _items.Add(dropdownItem);
                _itemList.Add(dropdownItem);
            }
            
            // Adjust height based on number of items
            int visibleItems = System.Math.Min(itemDataList.Count, _maxVisibleItems);
            Height.Set(visibleItems * _itemHeight + 6, 0f); // Add padding
            
            // Update scrollbar visibility
            UpdateScrollbar();
            
            Main.NewText($"Dropdown populated with {_items.Count} items, height set to {visibleItems * _itemHeight + 4}");
        }

        private void UpdateScrollbar()
        {
            if (_itemList != null)
            {
                Recalculate();
                float listHeight = _itemList.GetTotalHeight();
                float panelHeight = GetDimensions().Height - 4; // Account for padding
                
                bool needsScrollbar = listHeight > panelHeight;
                
                if (needsScrollbar)
                {
                    if (!HasChild(_scrollbar))
                    {
                        _itemList.Width.Set(-25f, 1f);
                        Append(_scrollbar);
                    }
                }
                else
                {
                    if (HasChild(_scrollbar))
                    {
                        RemoveChild(_scrollbar);
                        _itemList.Width.Set(0, 1f);
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if (!_isVisible)
                return;
                
            // Update scrollbar visibility
            UpdateScrollbar();
                
            // Hide dropdown if clicking outside
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (!ContainsPoint(Main.MouseScreen))
                {
                    Hide();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!_isVisible)
                return;
                
            base.Draw(spriteBatch);
        }
    }

    public class DropdownItem : UIElement
    {
        private ItemData _itemData;
        private bool _isHovered;
        
        public delegate void ItemClickHandler(ItemData itemData);
        public event ItemClickHandler OnClick;
        
        public DropdownItem(ItemData itemData)
        {
            _itemData = itemData;
            Height.Set(30f, 0f);
            Width.Set(0, 1f);
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            Height.Set(30f, 0f);
            Width.Set(0, 1f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            _isHovered = IsMouseHovering;
            
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed && _isHovered)
            {
                OnClick?.Invoke(_itemData);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            Rectangle hitbox = dimensions.ToRectangle();
            
            // Background color based on hover state
            Color backgroundColor = _isHovered ? new Color(70, 90, 130, 200) : new Color(45, 60, 100, 180);
            
            Texture2D pixel = TextureAssets.MagicPixel.Value;
            spriteBatch.Draw(pixel, hitbox, backgroundColor);
            
            // Debug: Draw a border to see the item bounds
            spriteBatch.Draw(pixel, new Rectangle(hitbox.X, hitbox.Y, hitbox.Width, 1), Color.White);
            spriteBatch.Draw(pixel, new Rectangle(hitbox.X, hitbox.Y + hitbox.Height - 1, hitbox.Width, 1), Color.White);
            spriteBatch.Draw(pixel, new Rectangle(hitbox.X, hitbox.Y, 1, hitbox.Height), Color.White);
            spriteBatch.Draw(pixel, new Rectangle(hitbox.X + hitbox.Width - 1, hitbox.Y, 1, hitbox.Height), Color.White);
            
            // Draw item icon if available
            if (_itemData.Id > 0)
            {
                try
                {
                    Main.instance.LoadItem(_itemData.Id);
                    Texture2D itemTexture = TextureAssets.Item[_itemData.Id].Value;
                    Rectangle sourceRect = new Rectangle(0, 0, itemTexture.Width, itemTexture.Height);
                    Rectangle iconRect = new Rectangle((int)dimensions.X + 5, (int)dimensions.Y + 5, 20, 20);
                    spriteBatch.Draw(itemTexture, iconRect, sourceRect, Color.White);
                }
                catch
                {
                    // If item texture fails to load, just continue without icon
                }
            }
            
            // Draw item name
            if (FontAssets.MouseText?.Value != null)
            {
                DynamicSpriteFont font = FontAssets.MouseText.Value;
                Vector2 textPosition = new Vector2(dimensions.X + 30, dimensions.Y + 5);
                Color textColor = _isHovered ? Color.White : Color.LightGray;
                
                // Truncate text if too long
                string displayText = _itemData.Name;
                Vector2 textSize = font.MeasureString(displayText);
                if (textSize.X > dimensions.Width - 35)
                {
                    while (textSize.X > dimensions.Width - 35 && displayText.Length > 0)
                    {
                        displayText = displayText.Substring(0, displayText.Length - 1);
                        textSize = font.MeasureString(displayText + "...");
                    }
                    displayText += "...";
                }
                
                spriteBatch.DrawString(font, displayText, textPosition, textColor);
            }
        }
    }
}