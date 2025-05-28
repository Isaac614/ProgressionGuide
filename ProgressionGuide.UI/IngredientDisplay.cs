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

        public IngredientDisplay(Item item) : base(item)
        {
            Width.Set(0f, 1f);
            Height.Set(30f, 0);

            _ingredient = item.Name;
            _amount = item.stack;
            Main.instance.LoadItem(item.type);
            _icon = TextureAssets.Item[item.type].Value;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
        }
    }
}