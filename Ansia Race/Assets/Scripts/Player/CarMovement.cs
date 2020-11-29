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
    public Transform t;
    public Vector3 rotationVec = new Vector3(0f, 0f, 0f);

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        t = this.gameObject.transform;
        speed = 0;
    }

    void Update()
    {
        if ((Input.GetAxis("Vertical") > -1) && (rb.velocity.magnitude < maxSpeed))
        {
            rb.AddForce(this.gameObject.transform.forward * accelerationForce, ForceMode.VelocityChange);
            speed = rb.velocity.magnitude;
        }

        rotationVec = new Vector3(0f, t.rotation.y + (turningRate * Input.GetAxis("Horizontal") * Time.deltaTime), 0f);
        t.Rotate(rotationVec.x, t.rotation.y + rotationVec.y, rotationVec.z, Space.World);
    }
}
