using HarmonyLib;
using UnityEngine;

namespace BaldiAP.Hooks;

[HarmonyPatch(typeof(StandardDoor))]
public class StandardDoorPatches
{
    [HarmonyPatch(nameof(StandardDoor.Start))]
    [HarmonyPrefix]
    public static bool Start(StandardDoor __instance)
    {
        RenameStandardDoor(__instance.transform);
        return true;
    }

    private static void RenameStandardDoor(Transform transform)
    {
        transform.name = (transform.position.x, transform.position.y, transform.position.z) switch
        {
            (165.0f, 0.0f, 55.0f) => "StandardDoor1",
            (185.0f, 0.0f, 65.0f) => "StandardDoor2",
            (195.0f, 0.0f, 155.0f) => "StandardDoor3",
            (145.0f, 0.0f, 265.0f) => "StandardDoor4",
            (55.0f, 0.0f, 305.0f) => "StandardDoor5",
            (275.0f, 0.0f, 335.0f) => "StandardDoor6",
            (275.0f, 0.0f, 195.0f) => "StandardDoor7",
            (95.0f, 0.0f, 45.0f) => "StandardDoor8",
            (115.0f, 0.0f, 95.0f) => "StandardDoor9",
            (145.0f, 0.0f, 155.0f) => "StandardDoor10",
            (275.0f, 0.0f, 125.0f) => "StandardDoor11",
            (215.0f, 0.0f, 265.0f) => "StandardDoor12",
            (75.0f, 0.0f, 195.0f) => "StandardDoor13",
            (195.0f, 0.0f, 195.0f) => "StandardDoor14",
            (245.0f, 0.0f, 65.0f) => "StandardDoor15",
            _ => transform.name
        };
    }
}