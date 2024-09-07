using HarmonyLib;

namespace ThomasTheJester.Patches;

[HarmonyPatch(typeof(JesterAI))]
public sealed class JesterPatch
{
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    public static void Start(JesterAI __instance)
    {
        __instance.popGoesTheWeaselTheme = Plugin.Music;
    }

    [HarmonyPatch("SetJesterInitialValues")]
    [HarmonyPostfix]
    public static void SetJesterInitialValues(JesterAI __instance)
    {
        if (Plugin.ModConfig.ShouldPatchPopUpTimer())
        {
            __instance.popUpTimer = Plugin.Music.length;
        }
    }
}
