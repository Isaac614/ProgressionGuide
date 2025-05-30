using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections.Generic;

namespace ProgressionGuide.UI
{
    public class TileDisplay : ItemDisplay
    {
        public TileDisplay(int tileId) : base(tileId, 32, false)
        { }

        // private (string name, Texture2D texture, Rectangle sourceRectangle) GetTileDisplayInfo(int tileId)
        // {
        //     TileObjectData tileData = TileObjectData.GetTileData(tileId, 0);
        //     string name = Lang.GetMapObjectName(MapHelper.TileToLookup(tileId, 0));
        //     Texture2D texture = TextureAssets.Tile[tileId].Value;

        //     int width = (tileData?.Width ?? 1) * 16;
        //     int height = (tileData?.Height ?? 1) * 16;
        //     Rectangle sourceRectangle = new Rectangle(0, 0, width, height);

        //     return (name, texture, sourceRectangle);
        // }
    }
}