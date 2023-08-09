using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace WorseFavoriteTooltip;

public class WorseFavoriteTooltip : Mod
{
}

internal class TooltipWorsener : GlobalItem
{
    private static readonly string Favorite = Language.GetTextValue("LegacyTooltip.56");
    private static readonly string FavoriteDesc = Language.GetTextValue("LegacyTooltip.57");

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (!item.favorited) return;

        for (int index = 0; index < tooltips.Count; index++)
        {
            Random rand = new(item.type + index);
            int loops = rand.Next(1, 7);

            for (int i = 0; i < loops; i++)
            {
                string text = "";
                for (int t = 1; t <= rand.Next(1, 5); t++)
                {
                    text += $"{(rand.Next(0, 2) % 2 == 0 ? Favorite : FavoriteDesc)}  ";
                }

                TooltipLine newLine = new(Mod, "WorseFavoriteTooltip" + i, text) { OverrideColor = Woke(index + i) };
                tooltips.Insert(index + i + 1, newLine);
            }
            index += loops;
        }
    }

    private static Color Woke(float i = 0)
    {
        Color a = Gay(i);
        Color b = Pronoun((int)i);
        float r = Math.Abs(Main.GlobalTimeWrappedHourly * 0.667f % 2f - 1f);
        return Color.Lerp(a, b, r);
    }

    private static Color Gay(float i = 0)
    {
        float hue = (Main.GlobalTimeWrappedHourly * 0.5f + (i / 30)) % 1f;
        return Main.hslToRgb(hue, 1f, 0.66f);
    }

    private static Color Pronoun(int i = 0)
    {
        return (i % 5) switch
        {
            0 or 4 => blue,
            1 or 3 => pink,
            _ => Color.White,
        };
    }
    private static Color blue = new(65, 236, 255);
    private static Color pink = new(255, 158, 180);
}