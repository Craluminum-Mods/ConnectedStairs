using HarmonyLib;
using Vintagestory.API.Common;

namespace ConnectedStairs;

public class HarmonyPatches : ModSystem
{
    private Harmony HarmonyInstance => new Harmony(Mod.Info.ModID);

    public override void StartPre(ICoreAPI api)
    {
        HarmonyInstance.Patch(original: Block_OnJsonTesselation_Patch.TargetMethod(), postfix: Block_OnJsonTesselation_Patch.GetPostfix());
        HarmonyInstance.Patch(original: Block_GetSelectionBoxes_Patch.TargetMethod(), prefix: Block_GetSelectionBoxes_Patch.GetPrefix());
        HarmonyInstance.Patch(original: Block_GetCollisionBoxes_Patch.TargetMethod(), prefix: Block_GetCollisionBoxes_Patch.GetPrefix());
        api.Logger.Event("started '{0}' mod", Mod.Info.Name);
    }

    public override void Dispose()
    {
        HarmonyInstance.UnpatchAll(HarmonyInstance.Id);
    }
}