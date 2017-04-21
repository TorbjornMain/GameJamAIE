using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable()]


[RequireComponent(typeof(Rigidbody2D))]
public class SweepAI : MonoBehaviour {

    [SerializeField()]
    AnimationCurve rotation;
    [SerializeField()]
    [Range(0, 30)]
    float sweepTime = 5;
    Rigidbody2D rb;

    // Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.MoveRotation(rotation.Evaluate(Time.timeSinceLevelLoad/sweepTime)*360 - 180);
	}
}
