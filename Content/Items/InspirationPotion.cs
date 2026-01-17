using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace InspirationPotions.Content.Items;

public sealed class InspirationPotion : InspirationPotionBase
{
    public override int RecoverInspiration => 30;
    public static short PotionType;
    public override void OnCreated(ItemCreationContext context)
    {
        if (context is not InitializationItemCreationContext) return;
        PotionType = (short)Type;
    }
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.buyPrice(0, 0, 2, 50); // 2 silver 50 Copper
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient<LesserInspirationPotion>(2)
            .AddIngredient(ItemID.GlowingMushroom)
            .AddTile(TileID.Bottles)
            .Register();
    }
}