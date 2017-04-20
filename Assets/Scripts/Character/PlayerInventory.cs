using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    [Range(0.01f,100)]
    public float maxWeight;
    public float curWeight
    {
        get
        {
            float w = 0;
            foreach(ItemInfo i in items)
            {
                w += i.weight;
            }
            return w;
        }
    }

    
    List<ItemInfo> items = new List<ItemInfo>();

    void PickUp(ItemPickup item)
    {
        if(item.itemInfo.weight + curWeight < maxWeight)
        {
            items.Add(item.itemInfo);
            Destroy(item.gameObject);
        }
    }


}
