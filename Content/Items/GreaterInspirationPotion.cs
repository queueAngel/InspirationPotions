using Terraria;
using Terraria.ID;

namespace InspirationPotions.Content.Items;

public sealed class GreaterInspirationPotion : InspirationPotionBase
{
    public override int RecoverInspiration => 40;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.buyPrice(0, 0, 5, 0); // 5 silver
    }
}