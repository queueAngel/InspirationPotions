using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace InspirationPotions.Common;

public enum CrateLoot : byte
{
    Mixed,
    Separate,
    None
}

public sealed class InspirationPotionsConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;
    public static InspirationPotionsConfig Instance;

    [DefaultValue(CrateLoot.Mixed)]
    [ReloadRequired]
    public CrateLoot CrateDrops;
}

public sealed class InspirationPotionsClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;
    public static InspirationPotionsClientConfig Instance;

    [DefaultValue(true)]
    public bool DungeonBottles;
}
