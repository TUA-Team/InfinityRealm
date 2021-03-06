﻿/*using ROI.API;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader.IO;

namespace ROI.Players
{
    partial class ROIPlayer
    {
        public const int AFFINITY_STANDARD_LIMIT = 50;


        public bool voidCollector;


        private void InitializeVoid()
        {
            VoidAffinity = 0;
            MaxVoidAffinity = 100;

            //VoidExposure = 0;

            //VoidEffectAttemptCooldown = 60 * Constants.TICKS_PER_SECOND;
            VoidItemCooldown = 300 * Constants.TICKS_PER_SECOND;
        }

        private void ResetEffectsVoid()
        {
            DebuffDurationMultiplier = 1f;

            //MaxVoidHearts2 = MaxVoidHearts;

            voidCollector = false;
        }


        private void PostUpdateVoid()
        {
            if (VoidItemCooldown > 0)
                VoidItemCooldown--;

            // VoidExposure += VoidAffinity; // What
        }


        private void ModifyHitByNPCVoid(NPC npc, ref int damage, ref bool crit) =>
            AttemptDamageVoidHeart(ref damage);

        private void ModifyHitByProjectileVoid(Projectile projectile, ref int damage, ref bool crit) =>
            AttemptDamageVoidHeart(ref damage);


        private TagCompound SaveVoid()
        {
            return new TagCompound
            {
                [nameof(VoidItemCooldown)] = VoidItemCooldown,
                //[nameof(VoidHeartHP)] = VoidHeartHP,
                //[nameof(MaxVoidHearts)] = MaxVoidHearts
            };
        }

        private void LoadVoid(TagCompound tag)
        {
            VoidItemCooldown = tag.GetAsInt(nameof(VoidItemCooldown));
            //VoidHeartHP = tag.GetAsInt(nameof(VoidHeartHP));
        }


        *//*        public void UnlockVoidTier(VoidTier tier)
        {
            if (VoidTier != tier - 1) // Player cannot skip tiers.
                return;

            VoidTier = tier;
            MaxVoidAffinity = (short)VoidMath.GetMaxVoidAffinity(VoidTier);
        }*//*

        public void RewardAffinity(ushort amount, ushort limit = AFFINITY_STANDARD_LIMIT) =>
            AddVoidAffinity(amount > limit ? limit : amount);

        public void RewardAffinity(NPC npc, ushort limit = AFFINITY_STANDARD_LIMIT) =>
            RewardAffinity((ushort)(npc.value / Item.buyPrice(gold: 1)), limit);

        public ushort AddVoidAffinity(ushort amount) => (ushort)(VoidAffinity += (short)amount);

        public void AttemptDamageVoidHeart(ref int damage)
        {
            int postNullificationDamage = damage*//* - VoidHeartHP*//*;

            if (postNullificationDamage <= 0)
            {
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.Black, damage, true, true);

                //VoidHeartHP -= damage;
            }

            if (postNullificationDamage > 0)
                damage = postNullificationDamage;
            else
                damage = 0;
        }


        public float DebuffDurationMultiplier { get; set; }


        public short VoidAffinity { get; public set; }

        public short MaxVoidAffinity { get; public set; }

        //public VoidTier VoidTier { get; public set; }


        public uint VoidExposure { get; private set; }

        //public int VoidEffectAttemptCooldown { get; public set; }

        public int VoidItemCooldown { get; public set; }


        *//*        public int VoidHeartHP { get; set; }

                /// <summary>Just like player.statLifeMax, you shouldn't change this property with potions and buffs. Use <see cref="MaxVoidHearts2"/>.</summary>
                public int MaxVoidHearts { get; set; }

                /// <summary>Just like player.statLifeMax2. Resets every frame via <see cref="ResetEffects"/>.</summary>
                public int MaxVoidHearts2 { get; set; }*//*
    }
}
*/