using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float maxSpeed = 15f;
    public float accelerationForce = 0f;
    public float turningRate = 10f;
    public float speed;
    public Rigidbody rb;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        speed = 0;
    }

    void Update()
    {
        if ((Input.GetAxis("Vertical") > -1) && (rb.velocity.magnitude < maxSpeed))
        {
            rb.AddForce(this.gameObject.transform.forward * accelerationForce, ForceMode.VelocityChange);
            speed = rb.velocity.magnitude;
        }

    }
}
