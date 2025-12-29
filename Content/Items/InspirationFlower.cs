using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using ThoriumMod.Utilities;
using InspirationPotions.Content.Players;
using InspirationPotions.Content.Buffs;
using ThoriumMod;
using ThoriumMod.Items.BardItems;
using Microsoft.Xna.Framework;

namespace InspirationPotions.Content.Items;

public sealed class InspirationFlower : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 28;
        Item.accessory = true;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.sellPrice(gold: 1);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        // +8% ���� Bard � ���������� ��� ������������ ���������� thorium
        player.GetDamage<BardDamage>() += 0.08f;

        if (player.GetThoriumPlayer().bardResource > 0)
            return;

        // ���� ������ ���� ����������� ����� ������
        QuickInspiration(player);
    }

    public static void QuickInspiration(Player p)
    {
        var cooldown = p.GetModPlayer<InspirationModPlayer>().inspirationAutoCooldown;

        // cooldown ����� ����������� (1 �������)
        if (cooldown != 0) return;

        bool useVoidBag = false;
        for (int i = 0; i < Main.InventorySlotsTotal; i++)
        {
            var item = p.inventory[i];
            if (item.IsAir)
                continue;
            if (item.ModItem is not InspirationPotionBase potion)
            {
                if (!useVoidBag) useVoidBag = item.type is ItemID.VoidLens;
                continue;
            }
            UsePotion(p, potion, true);
            return;
        }
        if (useVoidBag)
        {
            var bag = p.bank4.item;
            for (int i = 0; i < bag.Length; i++)
            {
                var item = bag[i];
                if (item.IsAir || item.ModItem is not InspirationPotionBase potion)
                    continue;
                UsePotion(p, potion, true);
                return;
            }
        }
    }

    public static void UsePotion(Player p, InspirationPotionBase potion, bool decrement = false)
    {
        var tPlayer = p.GetThoriumPlayer();

        var rec = potion.RecoverInspiration;
        var newVal = tPlayer.bardResource + rec;
        var max = tPlayer.bardResourceMax;

        if (newVal > max) newVal = max;
        tPlayer.bardResource = newVal;

        // ������
        p.AddBuff(ModContent.BuffType<InspirationWeakness>(), 60 * 5);

        // ��������� ����
        if (decrement)
            potion.Item.stack--;

        if (Main.dedServ)
            return;

        SoundEngine.PlaySound(in SoundID.Item3, p.position);
        CombatText.NewText(p.Hitbox, Color.Aquamarine, rec);
    }

    public override void AddRecipes()
    {
        // ������� ������
        CreateRecipe()
            .AddIngredient<HighQualityReed>() // 1 High Quality Reed
            .AddIngredient<InspirationPotion>() // 1 ������� ����� �����������
            .AddTile(TileID.TinkerersWorkbench) // ��� ������ ����
            .Register();
    }
}