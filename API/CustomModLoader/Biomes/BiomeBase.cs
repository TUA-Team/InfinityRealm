﻿using API.CustomModLoader.EntityComponents;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace API.CustomModLoader.Biomes
{
    // TODO: scrap this
    public abstract class BiomeBase : EntityHook, IHaveId, ICloneable
    {
        public void Load(Mod mod)
        {
        }

        public sealed override void Update()
        {
            var prev = Active;
            Active = IsBiomeActive();

            if (!prev && Active)
                OnEnter();
            else if (!Active)
                OnLeave();
        }


        public virtual void CopyCustomBiomesTo(Player other)
        {
            var modPlayer = other.GetModPlayer<BiomePlayer>();
            var registry = modPlayer.BiomeRegistry;
            var biome = registry.GetHook(GetType());
            biome.Active = Active;
        }

        // a little dumb but since ModPlayer had it, I assume it's necessary
        public void ReceiveBiome(bool flag)
        {
            Active = flag;
        }


        protected virtual bool IsBiomeActive() => false;

        public virtual void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            //var nesteds = GetType().GenerateNestedTypes();
        }

        protected virtual void OnEnter()
        {
        }

        protected virtual void OnLeave()
        {
        }


        public bool Active { get; private set; }


        public int MyId { get; private set; }


        public void Unload()
        {
        }

        public virtual object Clone() => MemberwiseClone();
    }
}