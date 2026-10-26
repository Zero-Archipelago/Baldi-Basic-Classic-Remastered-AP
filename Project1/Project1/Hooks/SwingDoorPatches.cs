using BaldiAP.Enums;
using HarmonyLib;
using UnityEngine;

namespace BaldiAP.Hooks;

[HarmonyPatch(typeof(SwingDoor))]
public class SwingDoorPatches
{
    [HarmonyPatch(nameof(SwingDoor.Start))]
    [HarmonyPrefix]
    public static bool Start(SwingDoor __instance)
    {
        RenameSwingDoor(__instance.transform);
        __instance.Lock(true);
        return true;
    }

    private static void RenameSwingDoor(Transform transform)
    {
        transform.name = (transform.position.x, transform.position.y, transform.position.z) switch
        {
            (135.0f, 0.0f, 35.0f) => SwingDoorNames.WestOfStart,
            (215.0f, 0.0f, 35.0f) => SwingDoorNames.EastOfStart,
            (175.0f, 0.0f, 75.0f) => SwingDoorNames.NorthOfStart,
            (145.0f, 0.0f, 225.0f) => SwingDoorNames.LeftOfDetention,
            (205.0f, 0.0f, 225.0f) => SwingDoorNames.RightOfDetention,
            (75.0f, 0.0f, 325.0f) => SwingDoorNames.WestCafe,
            (195.0f, 0.0f, 325.0f) => SwingDoorNames.EastCafe,
            (245.0f, 0.0f, 365.0f) => SwingDoorNames.NorthEastHalls,
            (175.0f, 0.0f, 25.0f) => SwingDoorNames.SouthExit,
            (295.0f, 0.0f, 215.0f) => SwingDoorNames.EastExit,
            (135.0f, 0.0f, 375.0f) => SwingDoorNames.NorthExit,
            (55.0f, 0.0f, 225.0f) => SwingDoorNames.WestExit,
            _ => transform.name
        };
    }
}