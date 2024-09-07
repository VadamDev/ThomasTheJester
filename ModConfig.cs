using BepInEx.Configuration;

namespace ThomasTheJester;

public sealed class ModConfig(ConfigFile configFile)
{
    private readonly ConfigEntry<bool> _enabled = configFile.Bind("Common", "Enabled", true, "Enable or disable the mod directly from here. Clients will still have the custom music if the mod is enabled on their side");
    private readonly ConfigEntry<bool> _patchPopUpTimerEntry = configFile.Bind("Server", "PatchPopUpTimer", true, "Define if the jester's pop up timer should fit the music length, ignored if the option above is disabled");

    public bool IsPluginEnabled() => _enabled.Value;
    public bool ShouldPatchPopUpTimer() => _patchPopUpTimerEntry.Value;
}
