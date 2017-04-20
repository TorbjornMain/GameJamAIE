using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraDetection : MonoBehaviour {
    public bool detected;
    bool prevDetected = false;


    [SerializeField()]
    [Range(0, 180)]
    float FOV;
    [SerializeField()]
    [Range(0,10)]
    float radius;
    [SerializeField()]
    List<GameObject> linkedObjects;

    void Start()
    {

    }

    void Update()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target)
        {

            float ang = Vector2.Angle(transform.right, (target.transform.position - transform.position).normalized);
            print(ang);
            if (ang < FOV / 2)
            {
                
                RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, target.transform.position - transform.position, radius);
                if (wallCheck)
                {
                    detected = wallCheck.collider.gameObject == target;
                }
                else
                {
                    detected = false;
                }

            }
            else
            {
                detected = false;
            }
        }
        else
        {
            detected = false;
        }

        if((detected != prevDetected) && detected)
        {
            foreach (GameObject g in linkedObjects)
            {
                SendMessage("Activate");
            }
        }
        prevDetected = detected;
    }

    void OnDrawGizmos()
    {
        if (detected)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, -FOV / 2) * new Vector3(radius, 0, 0));
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, FOV / 2) * new Vector3(radius, 0, 0));
        Gizmos.DrawLine(transform.position + Quaternion.Euler(0, 0, -FOV / 2) * new Vector3(radius, 0, 0), transform.position + transform.rotation * new Vector3(radius, 0, 0));
        Gizmos.DrawLine(transform.position + Quaternion.Euler(0, 0, FOV / 2) * new Vector3(radius, 0, 0), transform.position + transform.rotation * new Vector3(radius, 0, 0));
    }

}
