using HarmonyLib;
using System.Reflection;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace ConnectedStairs;

public static class Block_OnJsonTesselation_Patch
{
    public static MethodBase TargetMethod() => AccessTools.Method(typeof(Block), nameof(Block.OnJsonTesselation));
    public static MethodInfo GetPostfix() => typeof(Block_OnJsonTesselation_Patch).GetMethod(nameof(Postfix));

    public static void Postfix(Block __instance, ref MeshData sourceMesh, ref int[] lightRgbsByCorner, BlockPos pos, Block[] chunkExtBlocks, int extIndex3d, ICoreAPI ___api)
    {
        if (__instance is BlockStairs)
        {
            __instance.ConnectStairs(ref sourceMesh, pos, ___api);
        }
    }
}