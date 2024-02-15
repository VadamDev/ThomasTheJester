using BepInEx.Configuration;
using LethalConfig;
using LethalConfig.ConfigItems;
using LethalConfig.ConfigItems.Options;

namespace ThomasTheJester;

public class Config
{
    public readonly ConfigEntry<bool> PatchPopUpTimerEntry;

    public Config(ConfigFile configFile, bool lethalConfigLoaded)
    {
        PatchPopUpTimerEntry = configFile.Bind("General", "patchPopUpTimer", true, "[Host Only] Define if the jester's pop up timer should fit the music length");

        if(lethalConfigLoaded)
            DoLethalConfigStuff();
    }
    
    private void DoLethalConfigStuff()
    {
        var configItem = new BoolCheckBoxConfigItem(PatchPopUpTimerEntry, new BoolCheckBoxOptions
        {
            Name = "Patch Pop Up Timer"
        });
        
        LethalConfigManager.AddConfigItem(configItem);
    }
}