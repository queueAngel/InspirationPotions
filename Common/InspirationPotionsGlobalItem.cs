using InspirationPotions.Content.Items;
using System;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace InspirationPotions.Common;

public sealed class InspirationPotionsGlobalItem : GlobalItem
{
    public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
    {
        var style = InspirationPotionsConfig.Instance.CrateDrops;
        var rules = itemLoot.Get(false);
        var added = item.type switch
        {
            ItemID.WoodenCrate or ItemID.WoodenCrateHard => AddLoot(LesserInspirationPotion.PotionType, ItemID.LesserHealingPotion, ItemID.LesserManaPotion),
            ItemID.IronCrate or ItemID.IronCrateHard => AddLoot(InspirationPotion.PotionType, ItemID.HealingPotion, ItemID.ManaPotion),
            ItemID.GoldenCrate or ItemID.GoldenCrateHard => AddLoot(InspirationPotion.PotionType, ItemID.HealingPotion, ItemID.ManaPotion, 20),

            ItemID.JungleFishingCrate or ItemID.JungleFishingCrateHard or ItemID.FloatingIslandFishingCrate or ItemID.FloatingIslandFishingCrateHard or
            ItemID.CorruptFishingCrate or ItemID.CorruptFishingCrateHard or ItemID.CrimsonFishingCrate or ItemID.CrimsonFishingCrateHard or
            ItemID.HallowedFishingCrate or ItemID.HallowedFishingCrateHard or ItemID.DungeonFishingCrate or ItemID.DungeonFishingCrateHard or
            ItemID.FrozenCrate or ItemID.FrozenCrateHard or ItemID.OasisCrate or ItemID.OasisCrateHard or ItemID.LavaCrate or ItemID.LavaCrateHard
            or ItemID.OceanCrate or ItemID.OceanCrateHard => AddLoot(InspirationPotion.PotionType, ItemID.HealingPotion, ItemID.ManaPotion, 17),
            _ => false
        };
        if (added)
            Mod.Logger.Info($"Successfully added inspiration potions into {item.Name} loot pool");

        bool AddLoot(int lootType, int healTier, int manaTier, int maxDropped = 15)
        {
            switch (style)
            {
                case CrateLoot.Mixed:
                    foreach (var rule in CollectionsMarshal.AsSpan(rules))
                    {
                        if (rule is not OneFromRulesRule ofr)
                            continue;
                        ref var o = ref ofr.options;
                        if (o.Length < 2)
                            continue;
                        var restPotCount = 0;
                        for (int i = 0; i < o.Length; i++)
                            if (o[i] is CommonDropNotScalingWithLuck c && (c.itemId == healTier || c.itemId == manaTier))
                                restPotCount++;
                        if (restPotCount < 2)
                            break;
                        Array.Resize(ref o, o.Length + 1);
                        o[^1] = ItemDropRule.NotScalingWithLuck(lootType, 1, 5, maxDropped);
                        return true;
                    }
                    return false;
                case CrateLoot.Separate:
                    itemLoot.Add(ItemDropRule.NotScalingWithLuck(lootType, 8, 5, maxDropped));
                    return true;
                default:
                    return false;
            }
        }
    }
}
