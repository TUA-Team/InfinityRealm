﻿using Microsoft.Xna.Framework;
using ROI.Content.Subworlds.Wasteland.WorldBuilding.Items;
using Terraria;
using Terraria.ModLoader;

namespace ROI.Content.Subworlds.Wasteland.WorldBuilding.Tiles
{
    public sealed class UraniumOre : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[Type][ModContent.TileType<WastelandRock>()] = true;
            drop = ModContent.ItemType<UraniumChunk>();
            AddMapEntry(new Color(93, 202, 49));
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            g = 1;
            r *= 0.1f;
            b *= 0.1f;
            base.ModifyLight(i, j, ref r, ref g, ref b);
        }
    }
}
