﻿using Terraria;
using Terraria.ModLoader;
using ROIPlayer = ROI.Players.ROIPlayer;

namespace ROI.Recipes
{
    class VoidRecipe : ModRecipe
    {
        /// <summary>
        /// Set this value to the amount of void affinity point it need
        /// </summary>
        private int _voidValue = 0;

        private int _voidTier = 0;

        public VoidRecipe(Mod mod) : base(mod)
        {
        }

        public override bool RecipeAvailable()
        {
            ROIPlayer player = Main.LocalPlayer.GetModPlayer<ROIPlayer>();
            if (player.VoidAffinityAmount >= _voidValue && player.VoidTier >= _voidTier)
            {
                return true;
            }
            return false;
        }

        public override void OnCraft(Item item)
        {
            ROIPlayer player = Main.LocalPlayer.GetModPlayer<ROIPlayer>();
            player.AddVoidAffinity(-_voidValue);
        }

        public void SetVoidTier(int tier)
        {
            if (tier >= ROIConst.ALPHA_TIER && tier <= ROIConst.ZETA_TIER)
            {
                this._voidTier = tier;
            }
        }

        public void SetVoidAmount(int amount)
        {
            this._voidValue = amount;
        }
    }
}
