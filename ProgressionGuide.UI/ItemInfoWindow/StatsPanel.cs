using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;

namespace ProgressionGuide.UI
{
    public class StatsPanel : ScrollablePanel
    {
        private Item _item;
        public StatsPanel(float width, float height, bool bottomAlign,
        bool rightAlign, Item item) : base(width, height, bottomAlign, rightAlign)
        {
            _item = item;
        }
        public StatsPanel( float width, float height, float top,
        float left, Item item) : base(width, height, top, left)
        {
            _item = item;
        }

        public void PopulateStats(Item item)
        {
            StatDisplay damage = new StatDisplay("Damage", item.damage.ToString());
            StatDisplay knockback = new StatDisplay("Knockback", item.knockBack.ToString());
            StatDisplay critChance = new StatDisplay("Crit Chance", item.crit.ToString());
            StatDisplay useTime = new StatDisplay("Use Time", item.useTime.ToString());

            AddItem(damage);
            AddItem(knockback);
            AddItem(critChance);
            AddItem(useTime);
        }
    }
}