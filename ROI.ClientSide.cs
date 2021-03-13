﻿using Microsoft.Xna.Framework;
using ROI.Content.Configs;
using ROI.Content.Items;
using ROI.Content.Players;
using ROI.Content.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace ROI
{
    public sealed partial class ROIMod
    {
        //public MusicConfig MusicConfig;
        public DebugConfig DebugConfig;

        private void LoadClient()
        {
            //MusicConfig = ModContent.GetInstance<MusicConfig>();
            DebugConfig = ModContent.GetInstance<DebugConfig>();

            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Terra"),
                ModContent.ItemType<TerraMusicBox>(),
                ModContent.TileType<Content.Tiles.TerraMusicBox>());

            if (DebugConfig.Nightly)
            {
                CheckForNightly();
            }
        }

        private void UnloadClient()
        {
        }


        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            // TODO: (low prio) this was in EM, but it might be redundant
            if (Main.myPlayer == -1 || Main.gameMenu || !Main.LocalPlayer.active)
                return;

            var plr = ROIPlayer.Get();

            // Highest to lowest priority here, just return if a condition is validated

            if (plr.ZoneWasteland)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/WastelandDepth");
                priority = MusicPriority.Environment;
                return;
            }
        }


        public override void UpdateUI(GameTime gameTime) => InterfaceLoader.Instance.UpdateUI(gameTime);

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) => InterfaceLoader.Instance.ModifyInterfaceLayers(layers);
    }
}
