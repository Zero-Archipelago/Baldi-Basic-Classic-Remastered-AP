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
        return true;
    }

    private static void RenameSwingDoor(Transform transform)
    {
        transform.name = (transform.position.x, transform.position.y, transform.position.z) switch
        {
            (135.0f, 0.0f, 35.0f) => "SwingDoor1",
            (215.0f, 0.0f, 35.0f) => "SwingDoor2",
            (175.0f, 0.0f, 75.0f) => "SwingDoor3",
            (145.0f, 0.0f, 225.0f) => "SwingDoor4",
            (205.0f, 0.0f, 225.0f) => "SwingDoor5",
            (75.0f, 0.0f, 325.0f) => "SwingDoor6",
            (195.0f, 0.0f, 325.0f) => "SwingDoor7",
            (245.0f, 0.0f, 365.0f) => "SwingDoor8",
            (175.0f, 0.0f, 25.0f) => "SwingDoor9",
            (295.0f, 0.0f, 215.0f) => "SwingDoor10",
            (135.0f, 0.0f, 375.0f) => "SwingDoor11",
            (55.0f, 0.0f, 225.0f) => "SwingDoor12",
            _ => transform.name
        };
    }
}