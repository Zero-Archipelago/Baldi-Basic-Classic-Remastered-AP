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
        switch (transform.position.x, transform.position.y,  transform.position.z)
        {
            case (135.0f, 0.0f, 35.0f):
                transform.name = "SwingDoor1";
                break;
            case (215.0f, 0.0f, 35.0f):
                transform.name = "SwingDoor2";
                break;
            case (175.0f, 0.0f, 75.0f):
                transform.name = "SwingDoor3";
                break;
            case (145.0f, 0.0f, 225.0f):
                transform.name = "SwingDoor4";
                break;
            case (205.0f, 0.0f, 225.0f):
                transform.name = "SwingDoor5";
                break;
            case (75.0f, 0.0f, 325.0f):
                transform.name = "SwingDoor6";
                break;
            case (195.0f, 0.0f, 325.0f):
                transform.name = "SwingDoor7";
                break;
            case (245.0f, 0.0f, 365.0f):
                transform.name = "SwingDoor8";
                break;
            case (175.0f, 0.0f, 25.0f):
                transform.name = "SwingDoor9";
                break;
            case (295.0f, 0.0f, 215.0f):
                transform.name = "SwingDoor10";
                break;
            case (135.0f, 0.0f, 375.0f):
                transform.name = "SwingDoor11";
                break;
            case (55.0f, 0.0f, 225.0f):
                transform.name = "SwingDoor12";
                break;
        }
    }
}