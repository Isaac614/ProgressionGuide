using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;

namespace ProgressionGuide.UI
{
    public class SearchBar : UIElement
    {
        private string searchText = "";
        private bool isActive = false;

        public SearchBar()
        {
            Width.Set(300, 0f);
            Height.Set(30, 0f);

            Left.Set(50, 0f);
            Top.Set(50, 0f);
        }

        public override void OnInitialize()
        {
            base.OnInitialize();

            if (Main.netMode == Terraria.ID.NetmodeID.SinglePlayer) // for debugging 
            {
                Main.NewText("SearchBar initialized!", Color.Cyan);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsMouseHovering && Main.mouseLeft && Main.mouseLeftRelease)
            {
                isActive = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            Rectangle hitbox = new Rectangle(
                (int)dimensions.X,
                (int)dimensions.Y,
                (int)dimensions.Width,
                (int)dimensions.Height
            );

            Texture2D pixel = TextureAssets.MagicPixel.Value;
            Color backgroundcolor = isActive ? new Color(255, 255, 255, 230) : new Color(200, 200, 200, 200);
            spriteBatch.Draw(pixel, hitbox, backgroundcolor);

            DrawBorder(spriteBatch, hitbox, isActive ? Color.Blue : Color.Gray, 2);

            // var font = FontAssets.MouseText.Value;
            // string displayText = string.IsNullOrEmpty(searchText) ? "Search..." : searchText;
            // Color textColor = string.IsNullOrEmpty(searchText) ? Color.Gray : Color.Black;

            // Vector2 textPosition = new Vector2(dimensions.X + 5, dimensions.Y + 5);
            // spriteBatch.DrawString(font, displayText, textPosition, textColor);

            // string debugtext = $"SearchBar: {dimensions.Width}x{dimensions.Height} Active: {isActive}";
            // Vector2 debugPos = new Vector2(dimensions.X, dimensions.Y - 20);
            // spriteBatch.DrawString(font, debugText, debugPos, Color.Yellow);
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