﻿using System;
using System.Collections.Generic;
using System.IO;
using log4net;
using Microsoft.Xna.Framework;
using ROI.Buffs.Void;
using ROI.NPCs.Interfaces;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ROI.Worlds
{
    partial class ROIWorld : ModWorld
    {
        public bool StrangePresenceDebuff { get; internal set; }
        private int pillarSpawningTimer;
        
        /// <summary>
        /// This version string gonna be really important as we'll use it to distinguish an old world with a new one
        /// </summary>
        internal Version version = new Version(0, 0, 0, 0);

        private bool popout = false;

        public override TagCompound Save()
        {
            return new TagCompound()
            {
                ["modNPCData"] = SaveModNPCData(),
                [nameof(StrangePresenceDebuff)] = StrangePresenceDebuff,
                [nameof(version)] = version,
            };
        }

        private static List<TagCompound> SaveModNPCData()
        {
            List<TagCompound> npcList = new List<TagCompound>();
            foreach (NPC i in Main.npc)
            {
                if (i.modNPC is ISavableEntity entity)
                {
                    
                    TagCompound currentEntityTag = entity.Save();
                    currentEntityTag["position"] = i.position;
                    currentEntityTag["name"] = i.modNPC.Name;
                    currentEntityTag["mod"] = i.modNPC.mod.Name;
                    if (entity.SaveHP)
                    {
                        currentEntityTag["Health"] = i.life;
                    }
                    npcList.Add(currentEntityTag);
                }
            }

            return npcList;
        }

        public override void Load(TagCompound tag)
        {
            LoadModNPCData(tag);
            StrangePresenceDebuff = tag.GetBool(nameof(StrangePresenceDebuff));
            if (tag.ContainsKey(nameof(version)))
            {
                version = tag.Get<Version>(nameof(version));
            }
            else
            {
                version = new Version(0, 0, 0, 0);
            }
        }

        private static void LoadModNPCData(TagCompound tag)
        {
            List<TagCompound> modNPCData = tag.Get<List<TagCompound>>("modNPCData");
            foreach (TagCompound tagCompound in modNPCData)
            {
                Vector2 position = tagCompound.Get<Vector2>("position");
                int instanceNPCID = NPC.NewNPC((int) position.X, (int) position.Y, ModLoader.GetMod(tagCompound.GetString("mod")).NPCType(tagCompound.GetString("name")));
                if (Main.npc[instanceNPCID].modNPC is ISavableEntity entity)
                {
                    entity.Load(tagCompound);
                }
                LogManager.GetLogger("I actually care").Info(tagCompound.GetString("name") + " : " + tagCompound.GetString("mod"));
                if (tag.ContainsKey("Health"))
                {
                    Main.npc[instanceNPCID].life = tag.GetAsInt("Health");
                }
            }
        }

        public override void PreUpdate()
        {
            Main.bottomWorld = (Main.maxTilesY * 16) + 400;
            Main.topWorld = 0;
            Main.leftWorld = 0;
            Main.rightWorld = Main.maxTilesX * 16;

            if (StrangePresenceDebuff)
            {
                for (int i = 0; i < Main.player.Length; i++)
                {
                    Main.player[i].AddBuff(mod.BuffType<PillarPresence>(), 1, true);
                }

                if (--pillarSpawningTimer == 0)
                {
                    pillarSpawningTimer = 216000;
                    StrangePresenceDebuff = false;
                }
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(StrangePresenceDebuff);
        }

        public override void NetReceive(BinaryReader reader)
        {
            StrangePresenceDebuff = reader.ReadBoolean();
        }

        public override void PostWorldGen()
        {
            version = ROIMod.instance.Version;
        }
    }
}
