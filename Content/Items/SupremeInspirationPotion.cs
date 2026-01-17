using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace InspirationPotions.Content.Items;

public sealed class SupremeInspirationPotion : InspirationPotionBase
{
    public override int RecoverInspiration => 60;
    public static short PotionType;
    public override bool IsLoadingEnabled(Mod mod) => ModLoader.HasMod("CalamityMod");
    public override void OnCreated(ItemCreationContext context)
    {
        if (context is not InitializationItemCreationContext) return;
        PotionType = (short)Type;
    }
    public override void SetDefaults()
    {
        base.SetDefaults();
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.buyPrice(0, 0, 10, 0); // 10 silver
    }
    public override void AddRecipes()
    {
        CreateRecipe(15)
            .AddIngredient<SuperInspirationPotion>(15)
            .AddIngredient(ModLoader.GetMod("CalamityMod"), "Necroplasm")
            .AddTile(TileID.Bottles)
            .Register();
    }
}
