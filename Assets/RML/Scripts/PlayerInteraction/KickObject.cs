using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickObject : Interaction
{
    public bool KickItem(Transform transform, float kickDistance,float kickForce, in LayerMask kickableMask)
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, kickDistance, kickableMask))
        {
            var itemKicked = hit.collider.gameObject.GetComponent<Item>();
            if (itemKicked == null)
            {
                return false;
            }

            
                itemKicked.ApplyKick(hit.point, transform.forward, kickForce);
                return true;
            
        }

        return false;
    }
}
