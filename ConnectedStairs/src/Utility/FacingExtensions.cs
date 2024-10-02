using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace ConnectedStairs;

public static class FacingExtensions
{
    public static Block GetBlockAtFace(this IBlockAccessor blockAccessor, BlockPos pos, BlockFacing facing)
    {
        return blockAccessor.GetBlock(pos.AddCopy(facing));
    }

    public static BlockFacing GetVariantAtFace(this IBlockAccessor blockAccessor, BlockPos pos, BlockFacing facing, string variant)
    {
        return BlockFacing.FromCode(blockAccessor.GetBlockAtFace(pos, facing).Variant[variant]);
    }
}
