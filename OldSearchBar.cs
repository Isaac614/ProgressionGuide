using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace ProgressionGuide.UI
{
    public class OldSearchBar : UISearchBar
    {

        public OldSearchBar() : base(Language.GetText("Mods.ProgressionGuide.SearchPlaceholder"), 1f)
        {
            Width.Set(200f, 0f); // 200 pixels wide
            Height.Set(30f, 0f); // 30 pixels tall
            HAlign = 0.5f;
            VAlign = 0.3f;
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
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

            Main.NewText($"SearchBar.Draw called! Dims: X={dimensions.X}, Y={dimensions.Width}, H={dimensions.Height}");

            Texture2D pixel = ModContent.Request<Texture2D>("Images/Misc/Pixel").Value;
            // Color customBgColor = new Color(65, 80, 120, 220);
            spriteBatch.Draw(pixel, hitbox, Color.Red);
            // base.Draw(spriteBatch);

            // DrawBorder(spriteBatch, hitbox, new Color(120, 180, 100, 255), 2);
        }

        private void DrawBorder(SpriteBatch spriteBatch, Rectangle hitbox, Color borderColor, int thickness)
        {
            Texture2D pixel = ModContent.Request<Texture2D>("Images/Misc/Pixel").Value;


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