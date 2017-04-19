using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class CameraDetection : MonoBehaviour {
    
    Rigidbody2D rb;
    public bool detected;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        detected = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        detected = false;
    }

}
