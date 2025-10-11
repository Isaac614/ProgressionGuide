using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;

namespace ProgressionGuide.UI
{
    public class StatsPanel : ScrollablePanel
    {
        public StatsPanel(float width, float height, bool bottomAlign,
        bool rightAlign) : base(width, height, bottomAlign, rightAlign)
        { }
        public StatsPanel(float width, float height, float top,
        float left) : base(width, height, top, left)
        { }

        public void PopulateStats(Item item)
        {
            StatDisplay damage = new StatDisplay("Damage", item.damage.ToString());
            StatDisplay knockback = new StatDisplay("Knockback", item.knockBack.ToString());
            StatDisplay critChance = new StatDisplay("Crit Chance", item.crit.ToString());
            StatDisplay useTime = new StatDisplay("Use Time", item.useTime.ToString());

            if (item.damage != -1)
            {
                AddItem(damage);
                AddItem(knockback);
                AddItem(critChance);
            }
            AddItem(useTime);
        }
    }
}