using Terraria;
using Terraria.ModLoader;
using InspirationPotions.Content.Players;
using ThoriumMod;

namespace InspirationPotions.Content.Buffs;

public class InspirationWeakness : ModBuff
{
    // ������������ ������� (� �����) � 5 ������
    private const int TotalTicks = 60 * 5;
    // ������������ ������� ���������� (25%)
    private const float MaxWeakness = 0.25f;

    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        Main.buffNoSave[Type] = true;
    }

    // ���������� ������� � ���������� ��� ������, � �������� ���� ���� ������
    public override void Update(Player player, ref int buffIndex)
    {
        // �������� ���������� ����� (� �����)
        int timeLeft = player.buffTime[buffIndex];

        // ��������� ���� (0..1)
        float fraction = (float)timeLeft / TotalTicks;

        // ������� ������� ���������� (0..MaxWeakness)
        float percent = MaxWeakness * fraction;

        // ��������� � Bard-�����, ���� ����� DamageClass ����
        player.GetDamage<BardDamage>() -= percent;

        // ��������� ������� � ModPlayer ����� ������ ��� ��� ���������
        var my = player.GetModPlayer<InspirationModPlayer>();
        my.currentWeaknessPercent = percent;
    }

    // ���������� ��������� ��� ��������� ������ ������� � ModBuff
    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        // �������� ���������� ������ � ��� ���������� �������
        float percent = Main.LocalPlayer.GetModPlayer<InspirationModPlayer>().currentWeaknessPercent;

        // ����������� ������ ������ ����������� {0} � ��������� ����� ������� ����������
        // tip ������ ��������� {0} � �����������, �������� "-{0}% ����� �����"
        tip = string.Format(tip, (int)(percent * 100f));
    }
}