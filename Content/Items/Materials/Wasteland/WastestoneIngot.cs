using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ROI.Content.Items.Materials.Wasteland
{
    internal class WastestoneIngot : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.useStyle = ItemUseStyleID.Swing;
            item.value = Item.sellPrice(0, 0, 99, 0);
            item.maxStack = 999;
            item.createTile = ModContent.TileType<Content.Tiles.Wasteland.WastestoneIngot>();
            item.consumable = true;
        }

        public override void AddRecipes() => CreateRecipe()
                .AddIngredient<WastestoneOre>(3)
                .Register();
    }
}