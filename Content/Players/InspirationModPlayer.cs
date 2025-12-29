using Terraria.ModLoader;
using Microsoft.Xna.Framework.Input;
using Terraria.GameInput;
using InspirationPotions.Content.Items;
using ThoriumMod.Utilities;

namespace InspirationPotions.Content.Players;

public sealed class InspirationModPlayer : ModPlayer
{
    // cooldown ����� ����������� (60 = 1 �������)
    public int inspirationAutoCooldown = 0;

    // ������� ������� ����������, ������� ����� ������������ � ������� (0..0.25)
    public float currentWeaknessPercent = 0f;
    public static ModKeybind QuickInspiration;
    public override void Load()
    {
        QuickInspiration = KeybindLoader.RegisterKeybind(Mod, nameof(QuickInspiration), Keys.K);
    }
    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (!QuickInspiration.JustPressed)
            return;
        var tPlayer = Player.GetThoriumPlayer();
        if (tPlayer.bardResource >= tPlayer.bardResourceMax)
            return;
        InspirationFlower.QuickInspiration(Player);
    }
    public override void ResetEffects()
    {
        if (inspirationAutoCooldown != 0)
            inspirationAutoCooldown--;

        // ���������� �������� � ����� ����������� � ModBuff.Update
        currentWeaknessPercent = 0f;
    }
}
