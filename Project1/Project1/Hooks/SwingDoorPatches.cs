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
        return true;
    }

    public static void RenameSwingDoor(Transform transform)
    {
        switch (transform.position)
        {
            case (0f, 0f, 0f);
        }
    }
}