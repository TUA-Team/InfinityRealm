﻿using ROI.Content.Buffs;
using Terraria;

namespace ROI.Content.Subworlds.Wasteland.HeartOfTheWasteland.Debuff
{
    public sealed class Grasped : ROIBuff
    {
        public Grasped() : base("Grasped", "The heart of the wasteland is pulling you",
            hideTime: true, persistent: true, canBeCleared: false, debuff: true, longerExpertDebuff: true)
        {
        }


        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<WastelandPlayer>().grasped = true;
            player.buffTime[buffIndex] = 2;
        }
    }
}
