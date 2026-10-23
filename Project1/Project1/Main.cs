using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace BaldiAP
{
    public class Main : BaseUnityPlugin
    {
        public const string GameName = "Baldi's Basics Classic Remastered";
        public const string pluginGuid = "grenhunterr.baldi.archipelago.unityisstupid";
        public const string pluginName = "Archipelago";
        public const string pluginVersion = "0.0.1";
        
        public static ConfigEntry<bool>? EnableDebugLogging;
        public static ConfigEntry<bool>? FilterLog;
        public static ConfigEntry<float>? MessageInTime;
        public static ConfigEntry<float>? MessageHoldTime;
        public static ConfigEntry<float>? MessageOutTime;
        
        private readonly Harmony _harmony = new Harmony(pluginGuid);
        public static ManualLogSource? logger;
        
        public static AP_Hand ArchipelagoHandler;
        public static ItemHandler ItemHandler;

        void Awake()
        {
            logger = Logger;
            _harmony.PatchAll();
            
            ArchipelagoHandler = gameObject.AddComponent<AP_Hand>();
            ItemHandler = gameObject.AddComponent<ItemHandler>();
            
            EnableDebugLogging = Config.Bind(
                "Logging",
                "EnableDebugLogging",
                false,
                "Enables or disables debug logging in the Archipelago Console."
            );
        
            FilterLog = Config.Bind(
                "Logging",
                "FilterLog",
                false,
                "Filter the archipelago log to only show messages relevant to you."
            );
        
            MessageInTime = Config.Bind(
                "Logging",
                "MessageInTime",
                0.25f,
                "How long messages take to animate in."
            );
        
            MessageHoldTime = Config.Bind(
                "Logging",
                "MessageHoldTime",
                3f,
                "How long messages stay in the log before animating out."
            );
        
            MessageOutTime = Config.Bind(
                "Logging",
                "MessageOutTime",
                0.5f,
                "How long messages take to animate out."
            );
        }
    }
}