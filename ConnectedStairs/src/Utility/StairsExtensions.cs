using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace ConnectedStairs;

public static class StairsExtensions
{
    public static Shape GetOutsideShape(this ICoreAPI api) => api.Assets.TryGet(AssetLocation.Create("shapes/block/basic/stairs/outside.json"))?.ToObject<Shape>();
    public static Shape GetInsideShape(this ICoreAPI api) => api.Assets.TryGet(AssetLocation.Create("shapes/block/basic/stairs/inside.json"))?.ToObject<Shape>();

    public static Cuboidf UpBottomBox => new Cuboidf(x1: 0, y1: 0, z1: 0, x2: 1, y2: 0.5, z2: 1);
    public static Cuboidf UpMiddleBox => new Cuboidf(x1: 0, y1: 0.5, z1: 0, x2: 1, y2: 1, z2: 0.5);
    public static Cuboidf UpTopBox => new Cuboidf(x1: 0, y1: 0.5, z1: 0.5, x2: 0.5, y2: 1, z2: 1);

    public static Cuboidf DownBottomBox => new Cuboidf(x1: 0, y1: 0.5, z1: 0, x2: 1, y2: 1, z2: 1);
    public static Cuboidf DownMiddleBox => new Cuboidf(x1: 0, y1: 0, z1: 0.5, x2: 1, y2: 0.5, z2: 1);
    public static Cuboidf DownTopBox => new Cuboidf(x1: 0.5, y1: 0, z1: 0, x2: 1, y2: 0.5, z2: 0.5);

    public static Vec3d RotationOrigin => new Vec3d(0.5f, 0.5f, 0.5f);

    public static void ConnectStairs(this Block block, ref MeshData sourceMesh, BlockPos pos, ICoreAPI api)
    {
        ICoreClientAPI capi = api as ICoreClientAPI;
        BlockFacing horVariantNorth = capi.World.BlockAccessor.GetVariantAtFace(pos, BlockFacing.NORTH, "horizontalorientation");
        BlockFacing horVariantEast = capi.World.BlockAccessor.GetVariantAtFace(pos, BlockFacing.EAST, "horizontalorientation");
        BlockFacing horVariantSouth = capi.World.BlockAccessor.GetVariantAtFace(pos, BlockFacing.SOUTH, "horizontalorientation");
        BlockFacing horVariantWest = capi.World.BlockAccessor.GetVariantAtFace(pos, BlockFacing.WEST, "horizontalorientation");

        BlockFacing hor = BlockFacing.FromCode(block.Variant["horizontalorientation"]);
        BlockFacing ver = BlockFacing.FromCode(block.Variant["verticalorientation"]);

        Shape shape = null;
        Vec3f rotation = new Vec3f(0, 0, 0);

        if (ver == BlockFacing.DOWN)
        {
            if (hor == BlockFacing.WEST && horVariantEast == BlockFacing.NORTH || hor == BlockFacing.NORTH && horVariantSouth == BlockFacing.WEST)
            {
                shape = GetInsideShape(capi);
                rotation = new Vec3f(0, 90, 180);
            }
            else if (hor == BlockFacing.SOUTH && horVariantNorth == BlockFacing.WEST || hor == BlockFacing.WEST && horVariantEast == BlockFacing.SOUTH)
            {
                shape = GetInsideShape(capi);
                rotation = new Vec3f(0, 180, 180);
            }
            else if (hor == BlockFacing.SOUTH && horVariantNorth == BlockFacing.EAST || hor == BlockFacing.EAST && horVariantWest == BlockFacing.SOUTH)
            {
                shape = GetInsideShape(capi);
                rotation = new Vec3f(0, 270, 180);
            }
            else if (hor == BlockFacing.EAST && horVariantWest == BlockFacing.NORTH || hor == BlockFacing.NORTH && horVariantSouth == BlockFacing.EAST)
            {
                shape = GetInsideShape(capi);
                rotation = new Vec3f(0, 0, 180);
            }
            else if (hor == BlockFacing.EAST && horVariantEast == BlockFacing.NORTH || hor == BlockFacing.NORTH && horVariantNorth == BlockFacing.EAST)
            {
                shape = GetOutsideShape(capi);
                rotation = new Vec3f(0, 270, 180);
            }
            else if (hor == BlockFacing.WEST && horVariantWest == BlockFacing.NORTH || hor == BlockFacing.NORTH && horVariantNorth == BlockFacing.WEST)
            {
                shape = GetOutsideShape(capi);
                rotation = new Vec3f(0, 0, 180);
            }
            else if (hor == BlockFacing.WEST && horVariantWest == BlockFacing.SOUTH || hor == BlockFacing.SOUTH && horVariantSouth == BlockFacing.WEST)
            {
                shape = GetOutsideShape(capi);
                rotation = new Vec3f(0, 90, 180);
            }
            else if (hor == BlockFacing.EAST && horVariantEast == BlockFacing.SOUTH || hor == BlockFacing.SOUTH && horVariantSouth == BlockFacing.EAST)
            {
                shape = GetOutsideShape(capi);
                rotation = new Vec3f(0, 180, 180);
            }
        }
        else
        {
            if (hor == BlockFacing.WEST && horVariantEast == BlockFacing.NORTH || hor == BlockFacing.NORTH && horVariantSouth == BlockFacing.WEST)
            {
                shape = GetInsideShape(capi);
                rotation = new Vec3f(0, 0, 0);
            }
            else if (hor == BlockFacing.SOUTH && horVariantNorth == BlockFacing.WEST || hor == BlockFacing.WEST && horVariantEast == BlockFacing.SOUTH)
            {
                shape = GetInsideShape(capi);
                rotation = new Vec3f(0, 90, 0);
            }
            else if (hor == BlockFacing.SOUTH && horVariantNorth == BlockFacing.EAST || hor == BlockFacing.EAST && horVariantWest == BlockFacing.SOUTH)
            {
                shape = GetInsideShape(capi);
                rotation = new Vec3f(0, 180, 0);
            }
            else if (hor == BlockFacing.EAST && horVariantWest == BlockFacing.NORTH || hor == BlockFacing.NORTH && horVariantSouth == BlockFacing.EAST)
            {
                shape = GetInsideShape(capi);
                rotation = new Vec3f(0, 270, 0);
            }
            else if (hor == BlockFacing.EAST && horVariantEast == BlockFacing.NORTH || hor == BlockFacing.NORTH && horVariantNorth == BlockFacing.EAST)
            {
                shape = GetOutsideShape(capi);
                rotation = new Vec3f(0, 0, 0);
            }
            else if (hor == BlockFacing.WEST && horVariantWest == BlockFacing.NORTH || hor == BlockFacing.NORTH && horVariantNorth == BlockFacing.WEST)
            {
                shape = GetOutsideShape(capi);
                rotation = new Vec3f(0, 90, 0);
            }
            else if (hor == BlockFacing.WEST && horVariantWest == BlockFacing.SOUTH || hor == BlockFacing.SOUTH && horVariantSouth == BlockFacing.WEST)
            {
                shape = GetOutsideShape(capi);
                rotation = new Vec3f(0, 180, 0);
            }
            else if (hor == BlockFacing.EAST && horVariantEast == BlockFacing.SOUTH || hor == BlockFacing.SOUTH && horVariantSouth == BlockFacing.EAST)
            {
                shape = GetOutsideShape(capi);
                rotation = new Vec3f(0, 270, 0);
            }
        }

        if (shape == null)
        {
            return;
        }

        capi.Tesselator.TesselateShape(block, shape, out MeshData mesh, rotation);

        if (mesh != null)
        {
            sourceMesh = mesh;
        }
    }

    public static bool GetConnectedStairsBoxes(this Block block, ref Cuboidf[] result, IBlockAccessor blockAccessor, BlockPos pos)
    {
        BlockFacing horVariantNorth = blockAccessor.GetVariantAtFace(pos, BlockFacing.NORTH, "horizontalorientation");
        BlockFacing horVariantEast = blockAccessor.GetVariantAtFace(pos, BlockFacing.EAST, "horizontalorientation");
        BlockFacing horVariantSouth = blockAccessor.GetVariantAtFace(pos, BlockFacing.SOUTH, "horizontalorientation");
        BlockFacing horVariantWest = blockAccessor.GetVariantAtFace(pos, BlockFacing.WEST, "horizontalorientation");

        BlockFacing hor = BlockFacing.FromCode(block.Variant["horizontalorientation"]);
        BlockFacing ver = BlockFacing.FromCode(block.Variant["verticalorientation"]);

        if (ver == BlockFacing.DOWN)
        {
            if (hor == BlockFacing.WEST && horVariantEast == BlockFacing.NORTH || hor == BlockFacing.NORTH && horVariantSouth == BlockFacing.WEST)
            {
                result = new Cuboidf[]
                {
                        DownBottomBox,
                        DownMiddleBox.RotatedCopy(new Vec3f(0, 180, 0), RotationOrigin),
                        DownTopBox.RotatedCopy(new Vec3f(0, 180, 0), RotationOrigin),
                };
                return false;
            }
            else if (hor == BlockFacing.SOUTH && horVariantNorth == BlockFacing.WEST || hor == BlockFacing.WEST && horVariantEast == BlockFacing.SOUTH)
            {
                result = new Cuboidf[]
                {
                        DownBottomBox,
                        DownMiddleBox.RotatedCopy(new Vec3f(0, 270, 0), RotationOrigin),
                        DownTopBox.RotatedCopy(new Vec3f(0, 270, 0), RotationOrigin),
                };
                return false;
            }
            else if (hor == BlockFacing.SOUTH && horVariantNorth == BlockFacing.EAST || hor == BlockFacing.EAST && horVariantWest == BlockFacing.SOUTH)
            {
                result = new Cuboidf[]
                {
                        DownBottomBox,
                        DownMiddleBox,
                        DownTopBox
                };
                return false;
            }
            else if (hor == BlockFacing.EAST && horVariantWest == BlockFacing.NORTH || hor == BlockFacing.NORTH && horVariantSouth == BlockFacing.EAST)
            {
                result = new Cuboidf[]
                {
                        DownBottomBox,
                        DownMiddleBox.RotatedCopy(new Vec3f(0, 90, 0), RotationOrigin),
                        DownTopBox.RotatedCopy(new Vec3f(0, 90, 0), RotationOrigin),
                };
                return false;
            }
            else if (hor == BlockFacing.EAST && horVariantEast == BlockFacing.NORTH || hor == BlockFacing.NORTH && horVariantNorth == BlockFacing.EAST)
            {
                result = new Cuboidf[]
                {
                        DownBottomBox,
                        DownTopBox,
                };
                return false;
            }
            else if (hor == BlockFacing.WEST && horVariantWest == BlockFacing.NORTH || hor == BlockFacing.NORTH && horVariantNorth == BlockFacing.WEST)
            {
                result = new Cuboidf[]
                {
                        DownBottomBox.RotatedCopy(new Vec3f(0, 90, 0), RotationOrigin),
                        DownTopBox.RotatedCopy(new Vec3f(0, 90, 0), RotationOrigin),
                };
                return false;
            }
            else if (hor == BlockFacing.WEST && horVariantWest == BlockFacing.SOUTH || hor == BlockFacing.SOUTH && horVariantSouth == BlockFacing.WEST)
            {
                result = new Cuboidf[]
                {
                        DownBottomBox.RotatedCopy(new Vec3f(0, 180, 0), RotationOrigin),
                        DownTopBox.RotatedCopy(new Vec3f(0, 180, 0), RotationOrigin),
                };
                return false;
            }
            else if (hor == BlockFacing.EAST && horVariantEast == BlockFacing.SOUTH || hor == BlockFacing.SOUTH && horVariantSouth == BlockFacing.EAST)
            {
                result = new Cuboidf[]
                {
                        DownBottomBox.RotatedCopy(new Vec3f(0, 270, 0), RotationOrigin),
                        DownTopBox.RotatedCopy(new Vec3f(0, 270, 0), RotationOrigin),
                };
                return false;
            }
        }
        else
        {
            if (hor == BlockFacing.WEST && horVariantEast == BlockFacing.NORTH || hor == BlockFacing.NORTH && horVariantSouth == BlockFacing.WEST)
            {
                result = new Cuboidf[]
                {
                        UpBottomBox,
                        UpMiddleBox,
                        UpTopBox,
                };
                return false;
            }
            else if (hor == BlockFacing.SOUTH && horVariantNorth == BlockFacing.WEST || hor == BlockFacing.WEST && horVariantEast == BlockFacing.SOUTH)
            {
                result = new Cuboidf[]
                {
                        UpBottomBox,
                        UpMiddleBox.RotatedCopy(new Vec3f(0, 90, 0), RotationOrigin),
                        UpTopBox.RotatedCopy(new Vec3f(0, 90, 0), RotationOrigin),
                };
                return false;
            }
            else if (hor == BlockFacing.SOUTH && horVariantNorth == BlockFacing.EAST || hor == BlockFacing.EAST && horVariantWest == BlockFacing.SOUTH)
            {
                result = new Cuboidf[]
                {
                        UpBottomBox,
                        UpMiddleBox.RotatedCopy(new Vec3f(0, 180, 0), RotationOrigin),
                        UpTopBox.RotatedCopy(new Vec3f(0, 180, 0), RotationOrigin),
                };
                return false;
            }
            else if (hor == BlockFacing.EAST && horVariantWest == BlockFacing.NORTH || hor == BlockFacing.NORTH && horVariantSouth == BlockFacing.EAST)
            {
                result = new Cuboidf[]
                {
                        UpBottomBox,
                        UpMiddleBox.RotatedCopy(new Vec3f(0, 270, 0), RotationOrigin),
                        UpTopBox.RotatedCopy(new Vec3f(0, 270, 0), RotationOrigin),
                };
                return false;
            }
            else if (hor == BlockFacing.EAST && horVariantEast == BlockFacing.NORTH || hor == BlockFacing.NORTH && horVariantNorth == BlockFacing.EAST)
            {
                result = new Cuboidf[]
                {
                        UpBottomBox,
                        UpTopBox.RotatedCopy(new Vec3f(0, 180, 0), RotationOrigin),
                };
                return false;
            }
            else if (hor == BlockFacing.WEST && horVariantWest == BlockFacing.NORTH || hor == BlockFacing.NORTH && horVariantNorth == BlockFacing.WEST)
            {
                result = new Cuboidf[]
                {
                        UpBottomBox,
                        UpTopBox.RotatedCopy(new Vec3f(0, 270, 0), RotationOrigin),
                };
                return false;
            }
            else if (hor == BlockFacing.WEST && horVariantWest == BlockFacing.SOUTH || hor == BlockFacing.SOUTH && horVariantSouth == BlockFacing.WEST)
            {
                result = new Cuboidf[]
                {
                        UpBottomBox,
                        UpTopBox
                };
                return false;
            }
            else if (hor == BlockFacing.EAST && horVariantEast == BlockFacing.SOUTH || hor == BlockFacing.SOUTH && horVariantSouth == BlockFacing.EAST)
            {
                result = new Cuboidf[]
                {
                        UpBottomBox,
                        UpTopBox.RotatedCopy(new Vec3f(0, 90, 0), RotationOrigin),
                };
                return false;
            }
        }
        return true;
    }
}
