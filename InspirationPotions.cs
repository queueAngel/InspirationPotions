using InspirationPotions.Common;
using InspirationPotions.Content.Tiles;
using InspirationPotions.Utilities;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace InspirationPotions;

public sealed class InspirationPotions : Mod
{
    public override void Load()
    {
        // IL edit to generate inspiration potions in the dungeon 1/3 of the time
        IL_WorldGen.MakeDungeon += static il =>
        {
            var c = new ILCursor(il);
            int iVar = 0;
            int jVar = 0;
            // find i and j coordinates for bottle tile
            c.GotoNext(
                i => i.MatchLdloc(out iVar),
                i => i.MatchLdloc(out jVar),
                i => i.MatchLdloc(out _),
                i => i.MatchLdcI4(1));
            // change the Main.rand.Next(2) call to Main.rand.Next(3)
            c.GotoNext(i => i.MatchLdcI4(2));

            // we do a little conditional expression
            var config = typeof(InspirationPotionsClientConfig);
            c.EmitLdsfld(config.GetField("Instance"));
            c.EmitLdfld(config.GetField(nameof(InspirationPotionsClientConfig.DungeonBottles)));
            // we want to jump to 3 if dungeon bottles is true
            var randThree = il.DefineLabel();
            c.EmitBrtrue(randThree);
            // go past the two load
            c.Index++;
            // if we got to two, jump over three
            var pastThree = il.DefineLabel();
            c.EmitBr(pastThree);
            c.MarkLabel(randThree);
            c.Emit(OpCodes.Ldc_I4_3);
            c.MarkLabel(pastThree);

            // avoid the vanilla branch skip by inserting all our code before it
            c.Index++; // puts us before the brtrue instruction
            c.EmitLdloc(iVar);
            c.EmitLdloc(jVar);
            c.EmitDelegate(static (int rand, int i, int j) =>
            {
                var t = Main.tile[i, j];
                if (rand == 0)
                    t.TileFrameX = 18;
                else if (rand == 1)
                    t.TileFrameX = 36;
                else
                    t.TileType = (ushort)ModContent.TileType<InspirationBottle>();
            });
            // skip after our code runs
            var skip = il.DefineLabel();
            c.EmitBr(skip);
            // find appropriate place for label (after vanilla tile frame setting code)
            c.GotoNext(
                i => i.MatchLdloc(out _),
                i => i.MatchLdloc(out _),
                i => i.MatchBle(out _));
            c.MarkLabel(skip);
            ILHelper.UpdateInstructionOffsets(c);
            MonoModHooks.DumpIL(ModContent.GetInstance<InspirationPotions>(), il);
        };
    }
}
