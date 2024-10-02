﻿using HarmonyLib;
using System.Reflection;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace ConnectedStairs;

public static class Block_GetCollisionBoxes_Patch
{
    public static MethodBase TargetMethod() => AccessTools.Method(typeof(Block), nameof(Block.GetCollisionBoxes));
    public static MethodInfo GetPrefix() => typeof(Block_GetCollisionBoxes_Patch).GetMethod(nameof(Prefix));

    public static bool Prefix(Block __instance, ref Cuboidf[] __result, IBlockAccessor blockAccessor, BlockPos pos)
    {
        if (__instance is BlockStairs)
        {
            return __instance.GetConnectedStairsBoxes(ref __result, blockAccessor, pos);
        }
        return true;
    }
}