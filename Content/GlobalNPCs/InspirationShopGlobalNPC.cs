using InspirationPotions.Content.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace InspirationPotions.Content.GlobalNPCs;

public sealed class InspirationShopGlobalNPC : GlobalNPC
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
}