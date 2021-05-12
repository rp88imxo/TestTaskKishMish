using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> items;

    public int Count => items.Count;

    private void Awake()
    {
        items = new List<Item>(20);
    }

    public void AddToInventory(Item item)
    {
        items.Add(item);
    }

    public Item RemoveFromInventoryLast()
    {
        int lastIndex = Count - 1;
        if (lastIndex < 0)
        {
            return null;
        }
        Item item = items[lastIndex];
        items.RemoveAt(lastIndex);
        return item;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
