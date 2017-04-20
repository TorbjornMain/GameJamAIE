using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Gate : MonoBehaviour {

    Collider2D c;
    MeshRenderer mr;

	// Use this for initialization
	void Start () {
        mr = GetComponent<MeshRenderer>();
        mr.enabled = false;
        c = GetComponent<Collider2D>();
        c.enabled = false;
	}

    void Activate()
    {
        mr.enabled = true;
        c.enabled = true;
    }
}
