using InspirationPotions.Content.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace InspirationPotions.Content.Tiles;

public sealed class InspirationBottle : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = Main.tileNoAttach[Type] = true;

        AdjTiles = [TileID.Bottles];
        DustType = DustID.Glass;

        TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
        TileObjectData.addTile(Type);

        AddMapEntry(Color.White, Lang.GetItemName(ItemID.Bottle));
    }
    public override bool CanKillTile(int i, int j, ref bool blockDamaged)
    {
        if (Main.tile[i, j].TileFrameY == 72 && !ModLoader.HasMod("CalamityMod"))
            return false;
        return true;
    }
    public override IEnumerable<Item> GetItemDrops(int i, int j)
    {
        yield return new Item(PotionType(i, j));
    }
    public override void MouseOver(int i, int j)
    {
        var p = Main.LocalPlayer;
        p.noThrow = 2;
        p.cursorItemIconEnabled = true;
        p.cursorItemIconID = PotionType(i, j);
    }
    public override bool RightClick(int i, int j)
    {
        WorldGen.KillTile(i, j);
        if (Main.netMode != NetmodeID.SinglePlayer)
            NetMessage.SendTileSquare(Main.myPlayer, i, j);
        return true;
    }
    public static short PotionType(int i, int j) => Main.tile[i, j].TileFrameY switch
    {
        0 => LesserInspirationPotion.PotionType,
        18 => InspirationPotion.PotionType,
        36 => GreaterInspirationPotion.PotionType,
        54 => SuperInspirationPotion.PotionType,
        72 => SupremeInspirationPotion.PotionType,
        _ => throw new Exception($"Inspiration bottle with invalid tile framing at X: {i}, Y: {j}")
    };
}
