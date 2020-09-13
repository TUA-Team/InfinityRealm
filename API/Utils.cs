﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace API
{
    public static class Utils
    {
        // TODO: (low prio) cache these
        public static T GetField<T>(this object parent, string name,
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance)
        {
            return (T)parent.GetType().GetField(name, flags).GetValue(parent);
        }

        /*
         * this got totally screwed over by tML 1.4 reworks
        public static void GenerateLocalization(Mod mod)
        {
            var list = new List<string>();

            list.AddRange(mod.GetField<IDictionary<string, ModItem>>("items")
                .Select(x => $"{x.Value.DisplayName.Key}={x.Value.DisplayName.GetTranslation(Language.ActiveCulture)}"));

            list.AddRange(mod.GetField<IDictionary<string, ModItem>>("items")
                .Select(x => $"{x.Value.Tooltip.Key}={x.Value.Tooltip.GetTranslation(Language.ActiveCulture)}"));

            list.AddRange(mod.GetField<IDictionary<string, ModNPC>>("npcs")
                .Select(x => $"{x.Value.DisplayName.Key}={x.Value.DisplayName.GetTranslation(Language.ActiveCulture)}"));

            list.AddRange(mod.GetField<IDictionary<string, ModBuff>>("buffs")
                .Select(x => $"{x.Value.DisplayName.Key}={x.Value.DisplayName.GetTranslation(Language.ActiveCulture)}"));

            list.AddRange(mod.GetField<IDictionary<string, ModBuff>>("buffs")
                .Select(x => $"{x.Value.Description.Key}={x.Value.Description.GetTranslation(Language.ActiveCulture)}"));

            list.AddRange(mod.GetField<IDictionary<string, ModProjectile>>("projectiles")
                .Select(x => $"{x.Value.DisplayName.Key}={x.Value.DisplayName.GetTranslation(Language.ActiveCulture)}"));

            int index = $"Mods.ROI.".Length;
            System.IO.File.WriteAllText(Path.Combine(ModLoader.ModPath, "localized.txt"), string.Join("\n", list.Select(x => x.Remove(0, index))));
        }
        */

        public static T GetOrAdd<T>(this ICollection<T> col, Func<T, bool> predicate, Func<T> factory)
        {
            var val = col.FirstOrDefault(predicate);
            if (val != null) return val;
            val = factory();
            col.Add(val);
            return val;
        }
    }
}