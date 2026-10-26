using BaldiAP.Enums;
using HarmonyLib;
using UnityEngine;

namespace BaldiAP.Hooks;


[HarmonyPatch(typeof(ItemManager))]
public class ItemManagerPatches
{
    [HarmonyPatch(nameof(ItemManager.Awake))]
    [HarmonyPostfix]
    public static void Awake(ItemManager __instance)
    {
        var item_list = Resources.FindObjectsOfTypeAll<ItemObject>();
        foreach (var item in item_list)
        {
            if (item.name == ItemNames.AlarmClock)
            {
                __instance.AddItem(item);
            }
            else if (item.name == ItemNames.BSODA)
            {
                Debug.Log(item);
            }
        }
    }
}