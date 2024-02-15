using System.Linq;
using BepInEx;
using BepInEx.Bootstrap;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Networking;

namespace ThomasTheJester;

[BepInPlugin(GUID, Name, Version)]
[BepInDependency("ainavt.lc.lethalconfig", BepInDependency.DependencyFlags.SoftDependency)]
public class Plugin : BaseUnityPlugin
{
    public const string GUID = "ThomasTheJester";
    public const string Name = "Thomas The Jester";
    public const string Version = "1.0.0";

    public static Config ModConfig { get; private set; }

    public static AudioClip Music { get; private set; }

    private void Awake()
    {
        ModConfig = new Config(Config, IsLethalConfigLoaded());
        
        Music = LoadCustomMusic();
        if(Music == null)
            return;
        
        Logger.LogInfo("Patching JesterAI...");
        new Harmony(GUID).PatchAll(typeof(JesterPatch));
        
        Logger.LogInfo($"Plugin {Name} is loaded!");
    }

    private AudioClip LoadCustomMusic()
    {
        var dllLocation = Info.Location.TrimEnd($"{GUID}.dll".ToCharArray());
        
        var request = UnityWebRequestMultimedia.GetAudioClip($"File://{dllLocation}thomas.mp3", AudioType.MPEG);
        request.SendWebRequest();

        //Please tell me there's a better way
        while(!request.isDone) {}

        if(request.result == UnityWebRequest.Result.Success)
            return DownloadHandlerAudioClip.GetContent(request);
        
        Logger.LogError("Failed to load the audio file!");
        
        return null;
    }

    private static bool IsLethalConfigLoaded()
    {
        return Chainloader.PluginInfos.Any(pluginInfo => pluginInfo.Value.Metadata.GUID.Equals("ainavt.lc.lethalconfig"));
    }
}
