using BepInEx;
using HarmonyLib;
using ThomasTheJester.Patches;
using UnityEngine;
using UnityEngine.Networking;

namespace ThomasTheJester;

[BepInPlugin(GUID, Name, Version)]
[BepInDependency("ainavt.lc.lethalconfig", BepInDependency.DependencyFlags.SoftDependency)]
public class Plugin : BaseUnityPlugin
{
    public const string GUID = "net.vadamdev.thomasthejester";
    public const string Name = "Thomas The Jester";
    public const string Version = "2.0.0";

    public static ModConfig ModConfig { get; private set; }

    public static AudioClip Music { get; private set; }

    private void Awake()
    {
        ModConfig = new ModConfig(Config);
        
        if(!ModConfig.IsPluginEnabled())
        {
            Logger.LogInfo("Plugin has been disabled by the config.");
            return;
        }

        if((Music = LoadCustomMusic()) == null)
        {
            Logger.LogError("Failed to load the audio file!");
            return;
        }
        
        Logger.LogInfo("Loaded the audio file !");
        
        Logger.LogInfo("Patching JesterAI...");
        new Harmony(GUID).PatchAll(typeof(JesterPatch));
        
        Logger.LogInfo($"Plugin {Name} ({GUID}) is loaded!");
    }

    private AudioClip LoadCustomMusic()
    {
        string dllLocation = Info.Location.TrimEnd($"{Name.Trim(' ')}.dll".ToCharArray());

        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip($"File://{dllLocation}thomas", AudioType.MPEG);
        request.SendWebRequest();

        //Please tell me there's a better way
        while(!request.isDone) {}

        return request.result == UnityWebRequest.Result.Success
            ? DownloadHandlerAudioClip.GetContent(request)
            : null;
    }
}
