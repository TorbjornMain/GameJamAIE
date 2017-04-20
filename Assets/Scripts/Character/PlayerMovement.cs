﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInventory))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField()]
    float moveSpeed;

    [SerializeField()]
    [Range(0,1)]
    float weightSpeedFactor;

    Rigidbody2D rb;
    Transform cam;
    PlayerInventory pi;

    [System.NonSerialized()]
    public bool cutScene;

	// Use this for initialization
	void Start () {
        pi = GetComponent<PlayerInventory>();
        rb = GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 movVec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 vel = movVec * moveSpeed * Time.deltaTime;
        vel = Vector2.Lerp(vel, vel * weightSpeedFactor, pi.curWeight / pi.maxWeight);
        rb.MovePosition(rb.position + vel);
        rb.MoveRotation(Mathf.Rad2Deg * Mathf.Atan2(((Input.mousePosition.y/Screen.height) - 0.5f) * 2, ((Input.mousePosition.x/Screen.width) - 0.5f) * 2) - 90);
        if(!cutScene && cam != null)
        {
            cam.position = new Vector3(transform.position.x, transform.position.y, cam.position.z);
        }

	}
}
