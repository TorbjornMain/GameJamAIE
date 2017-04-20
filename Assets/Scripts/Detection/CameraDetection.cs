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
    [Range(0,20)]
    float radius;
    [SerializeField()]
    List<GameObject> linkedObjects;

    [SerializeField()]
    Color clearColor;
    [SerializeField()]
    Color detectedColor;

    List<LineRenderer> lines;

    void Start()
    {
        lines = new List<LineRenderer>(2);
        for (int i = 0; i < 2; i++)
        {
            GameObject ln = new GameObject();
            ln.transform.position = transform.position;
            ln.AddComponent<LineRenderer>();
            ln.transform.parent = transform;
            LineRenderer lr = ln.GetComponent<LineRenderer>();
            lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
            lr.startColor = Color.green;
            lr.endColor = new Color(0, 0, 0, 0);
            lr.widthMultiplier = 0.2f;
            lr.useWorldSpace = false;
            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, radius * new Vector3(1, 0, 0));
            lines.Add(lr);
        }
    }

    void Update()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target)
        {

            float ang = Vector2.Angle(transform.right, (target.transform.position - transform.position).normalized);
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
                g.SendMessage("Activate");
            }
        }

        if(detected)
        {
            lines[0].startColor = lines[1].startColor = detectedColor;
        }
        else
        {
            lines[0].startColor = lines[1].startColor = clearColor;
        }
        int layerMask = LayerMask.GetMask("Player");
        layerMask = ~layerMask;

        RaycastHit2D rc = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, FOV / 2) * transform.right, radius, layerMask);
        if (rc)
        {
            lines[0].SetPosition(1, rc.distance * (Quaternion.Euler(0, 0, FOV / 2) * transform.right));
            lines[0].endColor = Color.Lerp(lines[1].startColor, new Color(0, 0, 0, 0), rc.distance / radius);
        }
        else
        {
            lines[0].SetPosition(1, radius * (Quaternion.Euler(0, 0, FOV / 2) * transform.right));
            lines[0].endColor = new Color(0, 0, 0, 0);
        }

        rc = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, -FOV / 2) * transform.right, radius, layerMask);
        if (rc)
        {
            lines[1].SetPosition(1, rc.distance * (Quaternion.Euler(0, 0, -FOV / 2) * transform.right));
            lines[1].endColor = Color.Lerp(lines[1].startColor, new Color(0, 0, 0, 0), rc.distance / radius);
        }
        else
        {
            lines[1].SetPosition(1, radius * (Quaternion.Euler(0, 0, -FOV / 2) * transform.right));
            lines[1].endColor = new Color(0, 0, 0, 0);
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
