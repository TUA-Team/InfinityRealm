﻿using Terraria.ModLoader;

namespace ROI.Commands
{
    internal sealed class LootTest : ModCommand
    {
        public override bool Autoload(ref string name) => ROIMod.DEBUG;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            switch (args[0])
            {
            }
        }

        public override string Command => "loottest";
        public override CommandType Type => CommandType.World;
    }
}
