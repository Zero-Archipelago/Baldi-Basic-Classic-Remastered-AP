using HarmonyLib;
using UnityEngine.UI;

namespace BaldiAP.Hooks;

[HarmonyPatch(typeof(StandardMenuButton))]
public class StandardMenuButtonPatches
{
    [HarmonyPatch(nameof(StandardMenuButton.Update))]
    [HarmonyPrefix]
    public static bool Update(StandardMenuButton __instance)
    {
        
        if (__instance.name == "ClassicStyle")
        {
            __instance.GetComponent<Image>().enabled = true;
            
        }
        
        if (__instance.name == "PartyStyle")
        {
            __instance.GetComponent<Image>().enabled = false;
            
        }
        
        if (__instance.name == "DemoStyle")
        {
            __instance.GetComponent<Image>().enabled = false;
            
        }
        return true;
    }
}