using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace InspirationPotions.Content.Items;

public sealed class GreaterInspirationPotion : InspirationPotionBase
{
    public override int RecoverInspiration => 40;
    public static short PotionType;
    public override void OnCreated(ItemCreationContext context)
    {
        if (context is not InitializationItemCreationContext) return;
        PotionType = (short)Type;
    }
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.buyPrice(0, 0, 5, 0); // 5 silver
    }

    // drop handled in InspirationPotionsGlobalNPC
}