using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using BulwarkStudios.Stanford.SolarSystem.SpaceVehicles;

namespace FasterShips;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    public override void Load()
    {
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll();

        foreach (var patchedMethod in harmony.GetPatchedMethods())
        {
            Log.LogInfo($"Patched: {patchedMethod.DeclaringType?.FullName}:{patchedMethod}");
        }
    }
}
public class MoreStorage
{
    [HarmonyPatch(typeof(SpaceVehicleData), nameof(SpaceVehicleData.GetSpeed))]
    public class SpaceVehicleData_GetSpeed_Patch
    {
        public static void Postfix(SpaceVehicleData __instance, ref float __result)
        {
            if (__instance.IsCargoShip || __instance.IsMiningShip || __instance.IsScienceShip)
            {
                __result *= 2;
            }
        }
    }
}