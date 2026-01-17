using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace InspirationPotions.Content.Items;

public sealed class LesserInspirationPotion : InspirationPotionBase
{
    public override int RecoverInspiration => 20;
    public static short PotionType;
    public override void OnCreated(ItemCreationContext context)
    {
        if (context is not InitializationItemCreationContext) return;
        PotionType = (short)Type;
    }
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = ItemRarityID.White;
        Item.value = Item.buyPrice(0, 0, 1, 0); // 1 silver
    }
}