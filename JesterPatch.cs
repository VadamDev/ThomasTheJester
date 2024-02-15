using HarmonyLib;

namespace ThomasTheJester;

[HarmonyPatch(typeof(JesterAI))]
public class JesterPatch
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
        if (Plugin.ModConfig.PatchPopUpTimerEntry.Value)
        { 
            __instance.popUpTimer = Plugin.Music.length;
        }
    }
}
