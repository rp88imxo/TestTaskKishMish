using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    [Header("Pick Up Items Settings")]
    [SerializeField]
    private Transform itemHoldPivot;

    [SerializeField, Range(0f, 100f)]
    private float pickUpDistance = 3f;

    [Header("Kick Settings")]
    [SerializeField]
    private Transform kickPivot;

    [SerializeField,Range(0f,3f)]
    private float kickDistance = 1.5f;

    [SerializeField, Range(0f, 100f)]
    private float kickPower = 25f;

    [Header("Additional Settings")]
    [SerializeField]
    private Transform cameraTransform;

    [SerializeField, Range(0f, 5f)]
    private float maxDistanceBetweenePlayerAndItem = 2f;

    [SerializeField, Range(0f, 1000f)]
    private float minAttractionSpeed = 0f;

    [SerializeField, Range(0f, 1000f)]
    private float maxAttractionSpeed = 450f;

    [Header("Input Settings")]
    [SerializeField]
    private KeyMapStandalone keyMap;

    [Header("Mask Settings")]
    [SerializeField]
    private LayerMask interactableMask;

    [Header("Inventory")]
    [SerializeField]
    private Inventory inventory;

    [Header("Misc")]
    [SerializeField]
    private bool showDebugInfo = true;

    private MoveObjects moveObjects;
    private KickObject kickObject;
    private InventoryManageObjects inventoryManage;

    private Item pickedUpItem;
    private Collider playerCollider;

    private void OnValidate()
    {
        if (maxAttractionSpeed < minAttractionSpeed)
        {
            maxAttractionSpeed = minAttractionSpeed;
        }
    }

    private void Awake()
    {
        moveObjects = new MoveObjects(OnReleaseItem);
        kickObject = new KickObject();
        inventoryManage = new InventoryManageObjects();

        playerCollider = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region INPUT
        if (PlayerInputInteraction.GetLiftUpKeyDown(keyMap))
        {
            if (pickedUpItem == null)
            {
                pickedUpItem = moveObjects.PickUpItem(itemHoldPivot, cameraTransform, pickUpDistance, interactableMask);
            }
            else
            {
                moveObjects.ReleaseItem();
                pickedUpItem = null;
            }
        }
        else if (PlayerInputInteraction.GetKickKeyDown(keyMap))
        {
            kickObject.KickItem(kickPivot,kickDistance, kickPower, interactableMask);
        }
        else if (PlayerInputInteraction.GetPickUpKeyDown(keyMap))
        {
            inventoryManage.AddToInventory(cameraTransform, pickUpDistance, interactableMask, inventory);
        }
        else if (PlayerInputInteraction.GetDropItemKeyDown(keyMap) && pickedUpItem == null)
        {
            inventoryManage.DropFromInventoryLast(inventory, itemHoldPivot);
        }

        #endregion

        if (showDebugInfo)
        {
            Debug.DrawRay(kickPivot.position, kickPivot.forward * kickDistance, Color.blue);
            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * pickUpDistance, Color.red);
        }
       
    }

    private void FixedUpdate()
    {
        moveObjects.UpdateItemPos(Time.deltaTime, maxDistanceBetweenePlayerAndItem, minAttractionSpeed, maxAttractionSpeed);
    }

    #region EVENTS
    void OnReleaseItem()
    {
        pickedUpItem = null;
    }
    #endregion
}
