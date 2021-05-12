using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInputInteraction
{

    public static bool GetMouseButtonDown()
    {
      return Input.GetMouseButtonDown(0);
    }

    public static bool GetPickUpKeyDown(KeyMapStandalone keyMap)
    {
        return Input.GetKeyDown(keyMap.PickUpKey);
    }

    public static bool GetLiftUpKeyDown(KeyMapStandalone keyMap)
    {
        return Input.GetKeyDown(keyMap.LiftUpKey);
    }
    public static bool GetKickKeyDown(KeyMapStandalone keyMap)
    {
        return Input.GetKeyDown(keyMap.KickKey);
    }

    public static bool GetDropItemKeyDown(KeyMapStandalone keyMap)
    {
        return Input.GetKeyDown(keyMap.DropItemKey);
    }

}
