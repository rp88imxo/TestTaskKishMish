using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : Interaction
{
    private Item itemPicked;
    private Transform pivotPos;
    private float minDistanceThreshold = 0.1f;
    private float maxDistanceSmoothStep = 1.5f; // Максимальная разность расстояний при которой будет мапиться наибольшая скорость притяжения 

    public event Action onReleaseItem;


    public MoveObjects(Action onReleaseItem)
    {
        this.onReleaseItem += onReleaseItem;
    }

    public Item PickUpItem(Transform pivotObjectPos,Transform posFrom, float pickUpDistance, in LayerMask movableMask)
    {
        

        if (Physics.Raycast(posFrom.position, posFrom.forward, out RaycastHit hit, pickUpDistance, movableMask))
        {
            itemPicked = hit.collider.gameObject.GetComponent<Item>();
            if (itemPicked == null)
            {
                return null;
            }

            pivotPos = pivotObjectPos;
            Item.onForceRelease += ReleaseItem;
            Item.onResetState += ReleaseItem;

            //itemPicked.SlideToPos(pivotObjectPos);
           itemPicked.SetPickedUp();
           return itemPicked;
            
        }

        return null;
    }

   

    public void UpdateItemPos(
        float fixedTime, 
        float maxDistanceBetweenItemAndPlayer, 
        float minAttractionSpeed, 
        float maxAttractionSpeed)
    {
        if (itemPicked != null)
        {

            float distance = Vector3.Distance(itemPicked.Rigidbody.position, pivotPos.position);
            if (distance > maxDistanceBetweenItemAndPlayer)
            {
                onReleaseItem?.Invoke();
                ReleaseItem();
                return;
            }
            //if (distance < minDistanceThreshold)
            //{
            //    return;
            //}

            float speed = Mathf.SmoothStep(minAttractionSpeed, maxAttractionSpeed, distance / maxDistanceSmoothStep) * fixedTime;
            itemPicked.Rigidbody.velocity = (pivotPos.position - itemPicked.Rigidbody.position).normalized * speed;
        }
    }

    public void ReleaseItem()
    {
        if (itemPicked != null)
        {
            itemPicked.ReleaseSoft();
            itemPicked = null;
        }
       
    }
}
