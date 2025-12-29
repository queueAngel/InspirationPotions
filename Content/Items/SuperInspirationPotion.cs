using Terraria;
using Terraria.ID;

namespace InspirationPotions.Content.Items;

public sealed class SuperInspirationPotion : InspirationPotionBase
{
    public override int RecoverInspiration => 50;
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.buyPrice(0, 0, 10, 0); // 10 silver
    }
}