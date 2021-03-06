﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ROI.Content.Subworlds.Wasteland.HeartOfTheWasteland
{
    internal sealed class TongueDrawingPatch : ILoadable
    {
        public void Load(Mod mod)
        {
            On.Terraria.Main.DrawWoF += DrawBossTongues;
        }

        public static void DrawBossTongues(On.Terraria.Main.orig_DrawWoF orig, Main instance)
        {
            if (WastelandWorld.activeHotW >= 0)
            {
                // TO DO: Document this
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    if (!(Main.player[i].active &&
                        Main.player[i].GetModPlayer<WastelandPlayer>().grasped) ||
                        Main.player[i].dead)
                        continue;


                    float npcCenterX = Main.npc[WastelandWorld.activeHotW].position.X + (float)(Main.npc[WastelandWorld.activeHotW].width / 2);
                    float npcCenterY = Main.npc[WastelandWorld.activeHotW].position.Y + (float)(Main.npc[WastelandWorld.activeHotW].height / 2);
                    Vector2 vector = new Vector2(Main.player[i].position.X + (float)Main.player[i].width * 0.5f, Main.player[i].position.Y + (float)Main.player[i].height * 0.5f);
                    float num3 = npcCenterX - vector.X;
                    float num4 = npcCenterY - vector.Y;
                    float rotation = (float)Math.Atan2(num4, num3) - 1.57f;
                    bool drawing = true;
                    while (drawing)
                    {
                        float chainTextureLength = (float)Math.Sqrt(num3 * num3 + num4 * num4);
                        if (chainTextureLength < 40f)
                        {
                            drawing = false;
                            continue;
                        }

                        chainTextureLength = (float)Main.chain12Texture.Height / chainTextureLength;
                        num3 *= chainTextureLength;
                        num4 *= chainTextureLength;
                        vector.X += num3;
                        vector.Y += num4;
                        num3 = npcCenterX - vector.X;
                        num4 = npcCenterY - vector.Y;
                        //Color color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f));
                        Main.spriteBatch.Draw(Main.chain12Texture,
                            vector - Main.screenPosition,
                            new Rectangle(0, 0, Main.chain12Texture.Width, Main.chain12Texture.Height),
                            Color.ForestGreen,
                            rotation,
                            Main.chain12Texture.Size() * 0.5f,
                            1f, SpriteEffects.None, 0f);
                    }
                }
            }


            orig(Main.instance);
        }

        void ILoadable.Unload() { }
    }
}
