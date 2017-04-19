using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField()]
    float moveSpeed;

    Rigidbody2D rb;
    Transform cam;

    [System.NonSerialized()]
    public bool cutScene;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 movVec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        rb.MovePosition(rb.position + movVec * moveSpeed * Time.deltaTime);
        rb.MoveRotation(Mathf.Rad2Deg * Mathf.Atan2(((Input.mousePosition.y/Screen.height) - 0.5f) * 2, ((Input.mousePosition.x/Screen.width) - 0.5f) * 2) - 90);
        if(!cutScene && cam != null)
        {
            cam.position = new Vector3(transform.position.x, transform.position.y, cam.position.z);
        }

	}
}
