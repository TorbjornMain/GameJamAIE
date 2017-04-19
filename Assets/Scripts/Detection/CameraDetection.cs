using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class CameraDetection : MonoBehaviour {

    [SerializeField()]
    float FOV;


    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Vector2 direction = (other.attachedRigidbody.position - rb.position).normalized;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);

        if (angle > rb.rotation - FOV / 2 && angle > rb.rotation + FOV / 2)
        {
            print("detected");
        }

    }
}
