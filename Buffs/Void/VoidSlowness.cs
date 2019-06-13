﻿using Terraria;

namespace ROI.Buffs.Void
{
    class VoidSlowness : ROIBuff
    {
        protected VoidSlowness(string displayName, string description) : base("Void Slowness", "The void is slowly consuming you\n- 75% movement speed")
        {
        }

        public override void SetDefaults()
        {
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed *= 0.75f;
        }
    }
}
