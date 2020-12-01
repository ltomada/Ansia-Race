using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    public float maxSpeed;
    public float speed = 0f;
    public float acceleration = 0.5f;
    public float turningRate = 30f;
    public Transform carT;

    void Start()
    {
        carT = transform;
    }

    void Update()
    {
        speed += acceleration * Time.deltaTime;
        speed = Mathf.Clamp(speed, 0f, maxSpeed);
        carT.position += (carT.forward.normalized * speed);
    }
}
