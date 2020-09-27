using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ROI.Content.Walls.Wasteland
{
    internal class WastelandRockWall : ModWall
    {
        public override void SetDefaults()
        {
            AddMapEntry(new Color(34, 27, 43));
        }
    }

    internal class WastelandRockWallSafe : ModWall
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "ROI/Content/Walls/Wasteland/WastelandRockWall";
            return true;
        }

        public override void SetDefaults()
        {
            drop = ModContent.ItemType<Items.Placeables.Wasteland.WastelandRockWall>();
            Main.wallHouse[Type] = true;
            AddMapEntry(new Color(34, 27, 43));
        }
    }
}