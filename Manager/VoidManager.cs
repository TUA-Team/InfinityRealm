﻿using Terraria;
using ROIPlayer = ROI.Players.ROIPlayer;

namespace ROI.Manager
{
    internal sealed class VoidManager : AbstractManager<VoidManager>
    {

        public override void Initialize()
        {

        }

        public float Percent(ROIPlayer player)
        {
            return player.VoidAffinityAmount * 100f / player.MaxVoidAffinity;
        }

        /// <summary>
        /// Unlock the player tier locally, then will send to the server, it's a global unlock anyway for everyone in the server
        /// </summary>
        public void UnlockTier(ROIPlayer player, byte tier)
        {
            if (player.VoidTier != tier - 1)
            {
                return;
            }
            player.VoidTier = tier;
            switch (tier)
            {
                case 1:
                    player.MaxVoidAffinity = 100;
                    break;
                case 2:
                    player.MaxVoidAffinity = 200;
                    break;
                case 3:
                    player.MaxVoidAffinity = 500;
                    break;
                case 4:
                    player.MaxVoidAffinity = 900;
                    break;
                case 5:
                    player.MaxVoidAffinity = 1200;
                    break;
                case 6:
                    player.MaxVoidAffinity = 2000;
                    break;
            }
        }

        public void RewardAffinity(ROIPlayer player, int amount)
        {
            if (amount >= 50)
            {
                amount = 50;
            }

            player.AddVoidAffinity(amount);
        }

        #region Void Effect
        public void Effect(ROIPlayer target)
        {
            if (target.VoidTier >= 6) //normally impossible but hey, still prevempting potential error
            {
                target.VoidTier = 6;
            }

            if (Main.rand.Next(100) == 0 || VoidPillarRequirement(target))
            {
                if (VoidPillarRequirement(target))
                {
                    target.AddVoidAffinity(target.MaxVoidAffinity, false);
                }
            }
        }

        private static bool VoidPillarRequirement(ROIPlayer target)
        {
            return target.VoidAffinityAmount == target.MaxVoidAffinity && target.VoidTier == 6;
        }

        private bool Tier1Effect(ROIPlayer target)
        {
            return false;
        }

        private bool Tier2Effect(ROIPlayer target)
        {
            return false;
        }

        private bool Tier3Effect(ROIPlayer target)
        {
            return false;
        }

        private bool Tier4Effect(ROIPlayer target)
        {
            return false;
        }

        /// <summary>
        /// Possible effect can range 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool Tier5Effect(ROIPlayer target)
        {
            return false;
        }

        /// <summary>
        /// Void pillar boss fight, will drain all your void affinity if it spawn
        /// </summary>
        /// <param name="target"></param>
        private void Tier6Effect(ROIPlayer target)
        {

        }
        #endregion

    }
}
