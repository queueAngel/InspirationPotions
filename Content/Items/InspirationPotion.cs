using Terraria;
using Terraria.ID;

namespace InspirationPotions.Content.Items;

public sealed class InspirationPotion : InspirationPotionBase
{
    public override int RecoverInspiration => 30;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.buyPrice(0, 0, 2, 50); // 2 silver 50 Copper
    }
}