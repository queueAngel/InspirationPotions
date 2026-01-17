using InspirationPotions.Content.Items;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace InspirationPotions.Common;

public sealed class InspirationPotionsGlobalNPC : GlobalNPC
{
    public override void ModifyShop(NPCShop npcShop)
    {
        // ������ Merchant
        if (npcShop.NpcType != NPCID.Merchant)
            return;

        npcShop
            .Add<LesserInspirationPotion>() // Tier 1 � ������
            .Add<InspirationPotion>() // Tier 2 � ������
            .Add<GreaterInspirationPotion>(Condition.Hardmode) // Tier 3 � ������ Hardmode
            .Add<SuperInspirationPotion>(Condition.DownedPlantera); // Tier 4 � ����� Plantera
    }
    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        if (npc.type is NPCID.BigMimicCorruption or NPCID.BigMimicCrimson or NPCID.BigMimicHallow)
            npcLoot.Add(ItemDropRule.Common(GreaterInspirationPotion.PotionType, 1, 5, 15));
    }
}