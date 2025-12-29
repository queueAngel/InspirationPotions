using Terraria;
using Terraria.ID;

namespace InspirationPotions.Content.Items;

public sealed class LesserInspirationPotion : InspirationPotionBase
{
    public override int RecoverInspiration => 20;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = ItemRarityID.White;
        Item.value = Item.buyPrice(0, 0, 1, 0); // 1 silver
    }
}