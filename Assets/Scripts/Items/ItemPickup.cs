using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable()]
public struct ItemInfo
{

    [Range(0, 1)]
    public float fragility;

    [Range(0, 100)]
    public float weight;

    public float value;
}


[RequireComponent(typeof(Collider2D))]
public class ItemPickup : MonoBehaviour {

    public ItemInfo itemInfo;
    
	void OnTriggerEnter2D(Collider2D other)
    {
        other.SendMessage("PickUp", this);
    }
}
