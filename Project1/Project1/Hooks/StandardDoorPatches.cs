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
        switch (transform.position.x, transform.position.y, transform.position.z)
        {
            case (165.0f, 0.0f, 55.0f):
                transform.name = "StandardDoor1";
                break;
            case (185.0f, 0.0f, 65.0f):
                transform.name = "StandardDoor2";
                break;
            case (195.0f, 0.0f, 155.0f):
                transform.name = "StandardDoor3";
                break;
            case (145.0f, 0.0f, 265.0f):
                transform.name = "StandardDoor4";
                break;
            case (55.0f, 0.0f, 305.0f):
                transform.name = "StandardDoor5";
                break;
            case (275.0f, 0.0f, 335.0f):
                transform.name = "StandardDoor6";
                break;
            case (275.0f, 0.0f, 195.0f):
                transform.name = "StandardDoor7";
                break;
            case (95.0f, 0.0f, 45.0f):
                transform.name = "StandardDoor8";
                break;
            case (115.0f, 0.0f, 95.0f):
                transform.name = "StandardDoor9";
                break;
            case (145.0f, 0.0f, 155.0f):
                transform.name = "StandardDoor10";
                break;
            case (275.0f, 0.0f, 125.0f):
                transform.name = "StandardDoor11";
                break;
            case (215.0f, 0.0f, 265.0f):
                transform.name = "StandardDoor12";
                break;
            case (75.0f, 0.0f, 195.0f):
                transform.name = "StandardDoor13";
                break;
            case (195.0f, 0.0f, 195.0f):
                transform.name = "StandardDoor14";
                break;
            case (245.0f, 0.0f, 65.0f):
                transform.name = "StandardDoor15";
                break;
        }
    }
}