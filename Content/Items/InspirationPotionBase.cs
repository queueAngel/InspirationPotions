using InspirationPotions.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using ThoriumMod.Utilities;
using Terraria.Localization;

namespace InspirationPotions.Content.Items;

public abstract class InspirationPotionBase : ModItem
{
    private LocalizedText _tooltip;
    public sealed override LocalizedText Tooltip => _tooltip;
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
