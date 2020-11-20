﻿using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ROI.Content.Subworlds.Wasteland.Furniture.Walls
{
    public sealed class WastestoneBrickWall : ModWall
    {
        public override void SetDefaults()
        {
            drop = ModContent.ItemType<Items.WastestoneBrickWall>();
            ModTranslation wall = CreateMapEntryName();
            wall.SetDefault("Wastestone Brick Wall");
            AddMapEntry(new Color(173, 255, 47), wall);
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.01f;
            g = 0.8f;
            b = 0.2f;
        }
    }
}
