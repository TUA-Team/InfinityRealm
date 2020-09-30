﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ROI.Effects;
using ROI.NPCs.HeartOfTheWasteland;
using ROI.NPCs.Void.VoidPillar;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InfinityCore.API.Interface;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ROI.Players
{
    /// <summary>
    /// Use this to store the main player data, otherwise create a partial class
    /// </summary>
    public sealed partial class ROIPlayer : ModPlayer, IPlayerExtension
    {
        private Dictionary<string, PlayerDeathReason> deathReasonList = new Dictionary<string, PlayerDeathReason>();

        private short _voidAffinityAmount;
        public bool darkMind;
        public bool grasped;
        public bool horrified;

        //Temp
        private Texture2D chain = Main.chain12Texture;

        public int VoidTier { get; internal set; }
        // private short voidExposure;

        //Radiation in precent
        public float radiationLevel = 0;

        public override void Initialize()
        {
            _voidAffinityAmount = 0;
            darkMind = false;
            // voidExposure = 0;
        }

        public int MaxVoidAffinity { get; internal set; }

        public int VoidHeartHP { get; internal set; }

        /// <summary>
        /// Original cap is 100, after that you need to use upgrade and and stuff
        /// </summary>
	    public int MaxVoidHeartStats { get; internal set; }

        /// <summary>
        /// For modders, use this one if you wanna add extra health
        /// </summary>
	    public int MaxVoidHeartStatsExtra { get; set; }

        public bool ZoneWasteland = false;

        public override TagCompound Save()
        {
            return new TagCompound()
            {
                [nameof(_voidAffinityAmount)] = _voidAffinityAmount,
                [nameof(VoidTier)] = VoidTier,
                [nameof(MaxVoidAffinity)] = MaxVoidAffinity,
                [nameof(voidItemCooldown)] = voidItemCooldown,
                [nameof(VoidHeartHP)] = VoidHeartHP,
                [nameof(MaxVoidHeartStats)] = MaxVoidHeartStats
            };
        }

        public override void ResetEffects()
        {
            MaxVoidHeartStatsExtra = MaxVoidHeartStats;
            ResetArmorEffect();
        }

        public override void Load(TagCompound tag)
        {
            _voidAffinityAmount = tag.GetShort(nameof(_voidAffinityAmount));
            VoidTier = tag.GetAsInt(nameof(VoidTier));
            MaxVoidAffinity = tag.GetAsInt(nameof(MaxVoidAffinity));
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = mod.GetPacket(ushort.MaxValue);
            packet.Write(_voidAffinityAmount);
            packet.Write(VoidTier);
            packet.Write(voidItemCooldown);
            packet.Write(radiationLevel);
            packet.Write(_removeRadiationTimer);
            packet.Write(_radiationTimer);
            packet.Write(irrawoodSet);
            packet.Write(irradiatedSet);
            packet.Write(irradiatedHood);
            packet.Write(irradiatedHornedHelmet);
            packet.Write(irradiatedMask);
            packet.Write(irradiatedHat);
            packet.Write(irradiatedHelmet);
            packet.Send(toWho, fromWho);
        }

        public void ReceiveNetworkData(BinaryReader reader)
        {
            _voidAffinityAmount = reader.ReadInt16();
            VoidTier = reader.ReadByte();
            voidItemCooldown = reader.ReadInt32();
            radiationLevel = reader.ReadSingle();
            _removeRadiationTimer = reader.ReadInt32();
            _radiationTimer = reader.ReadInt32();
            irrawoodSet = reader.ReadBoolean();
            irradiatedSet = reader.ReadBoolean();
            irradiatedHood = reader.ReadBoolean();
            irradiatedHornedHelmet = reader.ReadBoolean();
            irradiatedMask = reader.ReadBoolean();
            irradiatedHat = reader.ReadBoolean();
            irradiatedHelmet = reader.ReadBoolean();
        }

        public override void UpdateBiomes()
        {
            Vector2 pos = player.position / 16;
            ZoneWasteland = pos.Y > Main.maxTilesY - 200 && WorldGen.crimson;
        }

        public override bool CustomBiomesMatch(Player other)
        {
            var otherPlr = other.GetModPlayer<ROIPlayer>();

            return ZoneWasteland == otherPlr.ZoneWasteland;
        }

        public override void CopyCustomBiomesTo(Player other)
        {
            var otherPlr = other.GetModPlayer<ROIPlayer>();

            otherPlr.ZoneWasteland = ZoneWasteland;
        }

        public override void SendCustomBiomes(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = ZoneWasteland;
            writer.Write(flags);
        }

        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            ZoneWasteland = flags[0];
        }

        public override Texture2D GetMapBackgroundImage()
        {
            //TODO: wasteland bg
            return base.GetMapBackgroundImage();
        }

        public override void PreUpdateBuffs()
        {
            bool isHotWAlive = NPC.AnyNPCs(ModContent.NPCType<HeartOfTheWasteland>());
            if (!isHotWAlive)
            {
                //if (player.HasBuff(ModContent.BuffType<Tongued>())) { player.DelBuff(ModContent.BuffType<Tongued>());}
                //if (player.HasBuff(ModContent.BuffType<Horrified>())) { player.DelBuff(ModContent.BuffType<Horrified>());}
            }
        }

        public override void PostUpdate()
        {
            if (voidItemCooldown != 0)
            {
                voidItemCooldown--;
            }

            if (ROI.Worlds.ROIWorld.activeHotWID >= 0)
            {
                if (player.position.Y / 16 > Main.maxTilesY - 250)
                {
                    player.AddBuff(mod.BuffType("Horrified"), 1);
                }

                if (Main.npc[ROI.Worlds.ROIWorld.activeHotWID].ai[0] == 1 &&
                    player.position.X / 16 > Main.npc[ROI.Worlds.ROIWorld.activeHotWID].position.X / 16 - 300 ||
                    player.position.X / 16 < Main.npc[ROI.Worlds.ROIWorld.activeHotWID].position.X / 16 + 300)
                {
                    grasped = true;
                    player.AddBuff(mod.BuffType("Grasped"), 60);
                }
            }

            WastelandUpdate();
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            //player.Hurt(deathReasonList["error"], 0, -1);
            DamageVoidHeart(ref damage);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            //player.Hurt(deathReasonList["error"], 0, -1);
            if(voidHeartBuff) 
                DamageVoidHeart(ref damage);
        }

        public override void UpdateBiomeVisuals()
        {
            Vector2 playerPosition = Main.LocalPlayer.position / 16;
            if (ZoneWasteland)
            {
                float percent = ((playerPosition.Y - Main.maxTilesY + 300) / 300);
                if (!Filters.Scene["ROI:UnderworldFilter"].IsActive())
                {
                    Filters.Scene.Activate("ROI:UnderworldFilter", Main.LocalPlayer.Center).GetShader().UseColor(ColorScreenShader.hell).UseIntensity(percent).UseOpacity(percent);
                }
                Filters.Scene["ROI:UnderworldFilter"].GetShader().UseColor(0f, 0f, 1f).UseIntensity(0.2f).UseOpacity(1f);

            }
            else
            {
                if (Filters.Scene["ROI:UnderworldFilter"].IsActive())
                {
                    Filters.Scene["ROI:UnderworldFilter"].Deactivate();
                }
            }

            bool isPillarPresent = Main.npc.Any(i => i.modNPC is VoidPillar);
            if (isPillarPresent)
            {
                SkyManager.Instance.Activate("ROI:VoidSky", player.position);
            }
            else
            {
                SkyManager.Instance.Deactivate("ROI:VoidSky");
            }
        }

        public bool ModifyBorderMovement(Player player)
        {

            return false;
        }
    }
}
