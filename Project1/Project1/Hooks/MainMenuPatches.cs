using System;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace BaldiAP.Hooks;

[HarmonyPatch(typeof(MainMenu))]
public class MainMenuPatches
{
    // [HarmonyPatch(nameof(MainMenu.Start))]
    // [HarmonyPostfix]
    // public static void Start(MainMenu __instance)
    // {
    //     GameObject.FindObjectOfType<MainMenu>().transform.Find("Copyright").GetComponent<TextMeshProUGUI>().text = "connect_status";
    //     GameObject.FindObjectOfType<MainMenu>().transform.Find("Version").GetComponent<TextMeshProUGUI>().text = "Baldi Archipelago v0.0.1";
    // }
    
    [HarmonyPatch(nameof(MainMenu.Quit))]
    [HarmonyPrefix]
    public static bool Quit(MainMenu __instance)
    {
        Application.Quit();
        return false;
    }
}