﻿using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace ROI.Commands
{
    class SetEntityValue : ModCommand
    {
        public override bool Autoload(ref string name) => ROIMod.debug;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            switch (args[0])
            {
                case "Entity":
                    break;
                case "Mod":
                    if (!ModLoader.Mods.Any(i => i.Name == args[1]))
                    {
                        Main.NewText("");
                    }
                    Mod mod = ModLoader.GetMod(args[1]);
                    break;
            }
        }

        public override string Command => "SetEntityValue";
        public override CommandType Type => CommandType.Chat;
    }
}
