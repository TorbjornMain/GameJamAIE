using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraDetection : MonoBehaviour {
    public bool detected;
    bool delayedDetectSent = false;



    [SerializeField()]
    [Range(0, 180)]
    float FOV;
    [SerializeField()]
    [Range(0,20)]
    float radius;
    [SerializeField()]
    List<GameObject> linkedObjects;
    [SerializeField()]
    float detectDelay;

    float timeDetected = 0;

    [SerializeField()]
    Color clearColor;
    [SerializeField()]
    Color detectedColor;

    List<LineRenderer> lines;
    Light spotlight;
    [SerializeField()]
    Light spotlightPrefab;

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

        spotlight = Instantiate<Light>(spotlightPrefab);
        spotlight.transform.position = transform.position + new Vector3(0, 0, 0.9f);
        spotlight.transform.parent = transform;
        spotlight.shadows = LightShadows.Hard;
        spotlight.shadowStrength = 1;
        spotlight.intensity = 8;
        spotlight.color = clearColor;
        spotlight.range = radius - 1;
        spotlight.spotAngle = FOV;
        Camera[] cam = { GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() };
        spotlight.GetComponent<LightShafts>().m_Cameras = cam;
        
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

        if(!delayedDetectSent && detected)
        {
            timeDetected = Mathf.Min(Time.deltaTime + timeDetected, detectDelay);
            if (timeDetected == detectDelay)
            {
                delayedDetectSent = true;
                foreach (GameObject g in linkedObjects)
                {
                    g.SendMessage("Activate");
                }

            }
        }


        spotlight.color = lines[0].startColor = lines[1].startColor = Color.Lerp(clearColor, detectedColor, timeDetected / detectDelay);
        if(!detected)
        { 
            timeDetected = Mathf.Max(0, timeDetected - Time.deltaTime);
            delayedDetectSent = false;
        }

        int layerMask = LayerMask.GetMask("Player");
        layerMask = ~layerMask;

        RaycastHit2D rc = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, FOV / 2) * transform.right, radius, layerMask);
        if (rc)
        {
            lines[0].SetPosition(1, rc.distance * (Quaternion.Euler(0, 0, FOV / 2) * new Vector3(1, 0, 0)));
            lines[0].endColor = Color.Lerp(lines[1].startColor, new Color(0, 0, 0, 0), rc.distance / radius);
        }
        else
        {
            lines[0].SetPosition(1, radius * (Quaternion.Euler(0, 0, FOV / 2) * new Vector3(1,0,0)));
            lines[0].endColor = new Color(0, 0, 0, 0);
        }

        rc = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, -FOV / 2) * transform.right, radius, layerMask);
        if (rc)
        {
            lines[1].SetPosition(1, rc.distance * (Quaternion.Euler(0, 0, -FOV / 2) * new Vector3(1, 0, 0)));
            lines[1].endColor = Color.Lerp(lines[1].startColor, new Color(0, 0, 0, 0), rc.distance / radius);
        }
        else
        {
            lines[1].SetPosition(1, radius * (Quaternion.Euler(0, 0, -FOV / 2) * new Vector3(1, 0, 0)));
            lines[1].endColor = new Color(0, 0, 0, 0);
        }

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

        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, -FOV / 2) * transform.right * radius);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, FOV / 2) * transform.right * radius);
        Gizmos.DrawLine(transform.position + Quaternion.Euler(0, 0, -FOV / 2) * transform.right * radius, transform.position + transform.right * radius);
        Gizmos.DrawLine(transform.position + Quaternion.Euler(0, 0, FOV / 2) * transform.right * radius, transform.position + transform.right * radius);
    }

}
