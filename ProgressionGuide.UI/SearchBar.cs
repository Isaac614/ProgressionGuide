using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mono.Cecil.Cil;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.Utilities;

namespace ProgressionGuide.UI
{
    public class SearchBar : UIElement
    {
        private string searchText = "";
        private bool isActive = false;
        private int cursorPosition = 0;
        private bool showCursor = true;
        private float cursorTimer = 0f;

        private KeyboardState previousKeyBoardState;
        private KeyboardState currentKeyboardState;

        private MouseState previousMouseState;
        private MouseState currentMouseState;

        public string SearchText
        {
            get { return searchText; }
            // set { searchText = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
        }

        public void ClearSearch()
        {
            searchText = "";
            cursorPosition = 0;
            showCursor = false;
            cursorTimer = 0f;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            previousKeyBoardState = Keyboard.GetState();
            Width.Set(0, 0.28f);
            Height.Set(30, 0f);
            Left.Set(0, 0.72f);
            Top.Set(0, 0f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            currentMouseState = Mouse.GetState();

            bool wasLeftMouseButtonJustPressed = currentMouseState.LeftButton == ButtonState.Pressed &&
            previousMouseState.LeftButton == ButtonState.Released;

            if (wasLeftMouseButtonJustPressed)
            {
                if (IsMouseHovering)
                {
                    isActive = true;
                }

                else
                {
                    isActive = false;
                }
            }

            previousMouseState = currentMouseState;

            if (isActive)
            {
                HandleTextInput();

                cursorTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (cursorTimer >= 0.5f)
                {
                    showCursor = !showCursor;
                    cursorTimer = 0f;
                }
            }
        }

        public void HandleTextInput()
        {
            previousKeyBoardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            // Handle special keys
            Keys[] pressedKeys = currentKeyboardState.GetPressedKeys();
            Keys[] previousPressedKeys = previousKeyBoardState.GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                // Only process if this key wasn't pressed in the prev frame
                if (!IsKeyInArray(key, previousPressedKeys))
                {
                    switch (key)
                    {
                        case Keys.Back:
                            if (searchText.Length > 0 && cursorPosition > 0)
                            {
                                searchText = searchText.Remove(cursorPosition - 1, 1);
                                cursorPosition--;
                            }
                            break;

                        case Keys.Delete:
                            if (cursorPosition < searchText.Length)
                            {
                                searchText = searchText.Remove(cursorPosition, 1);
                            }
                            break;

                        case Keys.Left:
                            if (cursorPosition > 0)
                            {
                                cursorPosition--;
                            }
                            break;

                        case Keys.Right:
                            if (cursorPosition < searchText.Length)
                            {
                                cursorPosition++;
                            }
                            break;

                        case Keys.Home:
                            cursorPosition = 0;
                            break;

                        case Keys.End:
                            cursorPosition = searchText.Length;
                            break;

                        default:
                            // For regular character input
                            char inputChar = GetCharFromKey(key, currentKeyboardState.IsKeyDown(Keys.LeftShift) ||
                            currentKeyboardState.IsKeyDown(Keys.RightShift));
                            if (inputChar != '\0' && searchText.Length < 50)
                            {
                                searchText = searchText.Insert(cursorPosition, inputChar.ToString());
                                cursorPosition++;
                            }
                            break;
                    }
                }
            }
        }

        private bool IsKeyInArray(Keys key, Keys[] keyArray)
        {
            foreach (Keys k in keyArray)
            {
                if (k == key) return true;
            }
            return false;
        }

        private char GetCharFromKey(Keys key, bool shift)
        {
            // Handle letters
            if (key >= Keys.A && key <= Keys.Z)
            {
                char letter = (char)('a' + (key - Keys.A));
                return shift ? char.ToUpper(letter) : letter;
            }

            // Handles numbers nad special characters

            switch (key)
            {
                case Keys.D0: return shift ? ')' : '0';
                case Keys.D1: return shift ? '!' : '1';
                case Keys.D2: return shift ? '@' : '2';
                case Keys.D3: return shift ? '#' : '3';
                case Keys.D4: return shift ? '$' : '4';
                case Keys.D5: return shift ? '%' : '5';
                case Keys.D6: return shift ? '^' : '6';
                case Keys.D7: return shift ? '&' : '7';
                case Keys.D8: return shift ? '*' : '8';
                case Keys.D9: return shift ? '(' : '9';

                case Keys.NumPad0: return '0';
                case Keys.NumPad1: return '1';
                case Keys.NumPad2: return '2';
                case Keys.NumPad3: return '3';
                case Keys.NumPad4: return '4';
                case Keys.NumPad5: return '5';
                case Keys.NumPad6: return '6';
                case Keys.NumPad7: return '7';
                case Keys.NumPad8: return '8';
                case Keys.NumPad9: return '9';

                case Keys.Space: return ' ';
                case Keys.OemPeriod: return shift ? '>' : '.';
                case Keys.OemComma: return shift ? '<' : ',';
                case Keys.OemQuestion: return shift ? '?' : '/';
                case Keys.OemSemicolon: return shift ? ':' : ';';
                case Keys.OemQuotes: return shift ? '"' : '\'';
                case Keys.OemOpenBrackets: return shift ? '{' : '[';
                case Keys.OemCloseBrackets: return shift ? '}' : ']';
                case Keys.OemPipe: return shift ? '|' : '\\';
                case Keys.OemMinus: return shift ? '_' : '-';
                case Keys.OemPlus: return shift ? '+' : '=';
                case Keys.OemTilde: return shift ? '~' : '`';

                default:
                    return '\0';
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            // Rectangle hitbox = new Rectangle(
            //     (int)dimensions.X,
            //     (int)dimensions.Y,
            //     (int)dimensions.Width,
            //     (int)dimensions.Height
            // );
            Rectangle hitbox = dimensions.ToRectangle();

            Texture2D pixel = TextureAssets.MagicPixel.Value;
            Color backgroundcolor = isActive ? new Color(255, 255, 255, 230) : new Color(200, 200, 200, 200);
            spriteBatch.Draw(pixel, hitbox, backgroundcolor);

            DrawBorder(spriteBatch, hitbox, isActive ? Color.Blue : Color.Gray, 2);

            if (FontAssets.MouseText?.Value != null) 
            {
                DynamicSpriteFont font = FontAssets.MouseText.Value;

                string displayText = string.IsNullOrEmpty(searchText) ? "Search..." : searchText;
                Color textColor = string.IsNullOrEmpty(searchText) ? Color.Gray : Color.Black;

                Vector2 textSize = font.MeasureString(displayText); 
                float paddingX = 8f; 
                float verticalOffset = (dimensions.Height - textSize.Y) / 2f; 
                
                Vector2 textPosition = new Vector2(dimensions.X + paddingX, dimensions.Y + verticalOffset);

                Utils.DrawBorderStringFourWay(spriteBatch, font, displayText, textPosition.X, textPosition.Y, textColor, Color.Black, Vector2.Zero, 1f);
                
                if (isActive && showCursor)
                {
                    Vector2 cursorDrawPos;

                    if (string.IsNullOrEmpty(searchText))
                    {
                        cursorDrawPos = new Vector2(textPosition.X, textPosition.Y);
                    }
                    else
                    {  
                        string textBeforeCursor = searchText.Substring(0, cursorPosition);
                        Vector2 textBeforeCursorSize = font.MeasureString(textBeforeCursor);
                        
                        cursorDrawPos = new Vector2(textPosition.X + textBeforeCursorSize.X, textPosition.Y);
                    }
                    
                    int cursorHeight = (int)(font.MeasureString("A").Y * 0.75);

                    Rectangle cursorRect = new Rectangle(
                        (int)cursorDrawPos.X,
                        (int)cursorDrawPos.Y, 
                        2, 
                        cursorHeight
                    );
                    spriteBatch.Draw(TextureAssets.MagicPixel.Value, cursorRect, Color.Red);
                }
            }
        }

        public void DrawBorder(SpriteBatch spriteBatch, Rectangle hitbox, Color borderColor, int thickness)
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