using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using InspirationPotions.Content.Tiles;

namespace InspirationPotions.Content.Items;

public abstract class InspirationPotionBase : ModItem
{
    private LocalizedText _tooltip;
    public sealed override LocalizedText Tooltip => _tooltip;
    public override void Load()
    {
        Mod.AddContent(new DecorativePotionItem(this));
    }
    public override void SetStaticDefaults()
    {
        _tooltip = Mod.GetLocalization("PotionTooltip").WithFormatArgs(RecoverInspiration);
    }
    public override void SetDefaults()
    {
        Item.width = 20;
        Item.height = 26;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.useTime = 17;
        Item.useAnimation = 17;
        Item.useTurn = true;
        Item.consumable = true;
        Item.maxStack = 9999;
    }
    public sealed override bool? UseItem(Player player)
    {
        InspirationFlower.UsePotion(player, this);
        return true;
    }
    public abstract int RecoverInspiration { get; }
}

[Autoload(false)]
public sealed class DecorativePotionItem : ModItem
{
    public DecorativePotionItem() { }
    public DecorativePotionItem(InspirationPotionBase parent) => _parent = parent;
    private readonly InspirationPotionBase _parent;
    public InspirationPotionBase Parent => _parent ?? ((DecorativePotionItem)ItemLoader.GetItem(Type))._parent;
    public override string Name => "Decorative" + Parent.Name;
    public override LocalizedText Tooltip => LocalizedText.Empty;
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<InspirationBottle>(), Parent switch
        {
            LesserInspirationPotion => 0,
            InspirationPotion => 1,
            GreaterInspirationPotion => 2,
            SuperInspirationPotion => 3,
            SupremeInspirationPotion => 4,
            _ => throw new System.Exception("You shouldn't be seeing this. Please report to the developer of Inspiration Potions")
        });
        Item.SetShopValues(ItemRarityID.White, Item.sellPrice(silver: 1));

        Item.maxStack = Item.CommonMaxStack;
        Item.width = Item.height = 20;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(Parent)
            .AddTile(TileID.HeavyWorkBench)
            .Register();
    }
}
