﻿using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace ROI.Content.Subworlds.Wasteland.WorldBuilding.Helpers
{
    /// <summary>
    /// Welcome to the Terraria cave update, oh wait, wrong game :/
    /// </summary>
    abstract class WastelandCave : TagSerializable
    {
        public int x, y, width, height;

        public static readonly Func<TagCompound, WastelandCave> DESERIALIZER = Load;

        public abstract string caveTypeName { get; }

        public Rectangle CaveBound => new Rectangle(x, y, width, height);

        public abstract void Generate(GenerationProgress progress);

        public WastelandCave(Rectangle rectangle)
        {
            x = rectangle.X;
            y = rectangle.Y;
            width = rectangle.Width;
            height = rectangle.Height;
        }

        /// <summary>
        /// Ran only on client side, allow fancy visual effect
        /// </summary>
        public virtual void ClientSideVisualEffect(Player player)
        {

        }

        //This is only synced once, which is upon world entering
        public void NetSyncSend(BinaryWriter writer)
        {
            if (Main.netMode != NetmodeID.Server)
                return;

            writer.Write(x);
            writer.Write(y);
            writer.Write(width);
            writer.Write(height);
        }

        //This is only synced once, which is upon world entering
        public void NetSyncReceive(BinaryReader reader)
        {
            x = reader.ReadInt32();
            y = reader.ReadInt32();
            width = reader.ReadInt32();
            height = reader.ReadInt32();
        }

        public TagCompound SerializeData()
        {
            return new TagCompound()
            {
                ["bound"] = CaveBound,
                ["caveType"] = caveTypeName
            };
        }

        public static WastelandCave Load(TagCompound tag)
        {
            string caveType = tag.GetString("caveType");
            switch (caveType)
            {
                case "Lost_Wood":
                    return new WastelandLostCave(tag.Get<Rectangle>("bound"));
            }
            throw new Exception("Invalid cave type");
        }
    }
}