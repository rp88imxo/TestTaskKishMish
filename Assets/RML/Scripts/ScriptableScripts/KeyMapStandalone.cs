using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Key Map Standalone", menuName = "RML/Input/KeyMapStandalone")]
public class KeyMapStandalone : ScriptableObject
{
    public KeyCode PickUpKey = KeyCode.E;
    public KeyCode LiftUpKey = KeyCode.F;
    public KeyCode KickKey = KeyCode.V;
    public KeyCode DropItemKey = KeyCode.G;
}
