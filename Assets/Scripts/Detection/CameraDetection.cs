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

    [SerializeField()]
    Vector3 lightOff;

    void Awake()
    {

        spotlight = Instantiate<Light>(spotlightPrefab);
        spotlight.transform.position = transform.position + new Vector3(0, 0, 1.5f);
        spotlight.transform.parent = transform;
        spotlight.transform.localEulerAngles = new Vector3(0, 90, 0);
        spotlight.shadows = LightShadows.Hard;
        spotlight.shadowStrength = 1;
        spotlight.intensity = 8;
        spotlight.color = clearColor;
        spotlight.range = radius;
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


        spotlight.color = Color.Lerp(clearColor, detectedColor, timeDetected / detectDelay);
        if(!detected)
        { 
            timeDetected = Mathf.Max(0, timeDetected - Time.deltaTime);
            delayedDetectSent = false;
        }

        int layerMask = LayerMask.GetMask("Player");
        layerMask = ~layerMask;
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
