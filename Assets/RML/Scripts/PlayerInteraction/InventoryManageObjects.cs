using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManageObjects : Interaction
{

    public bool AddToInventory(Transform posFrom, float distanceToCheck, in LayerMask interactionMask, Inventory inventory)
    {

        if (Physics.Raycast(posFrom.position, posFrom.forward, out RaycastHit hit, distanceToCheck, interactionMask))
        {
            var itemPicked = hit.collider.gameObject.GetComponent<Item>();
            if (itemPicked == null)
            {
                return false;
            }

            itemPicked.gameObject.SetActive(false);
            inventory.AddToInventory(itemPicked);
            return true;
        }

        return false;
    }

    public bool DropFromInventoryLast(Inventory inventory, Transform spawnPivot)
    {
        var item = inventory.RemoveFromInventoryLast();
        if (item == null)
        {
            return false;
        }

        item.ResetState(spawnPivot);
        
        return true;
    }

}
