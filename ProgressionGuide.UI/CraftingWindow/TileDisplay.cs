using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;

namespace ProgressionGuide.UI
{
    public class TileDisplay : ItemDisplay
    {
        public TileDisplay(int tileId) : base(tileId)
        { }
    }
}