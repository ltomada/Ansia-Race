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
        SpeedUp();
        TurnCar();
    }

    //Accelerazione della macchina
    private void SpeedUp()
    {
        speed += acceleration * Time.deltaTime;
        speed = Mathf.Clamp(speed, 0f, maxSpeed);
        //carT.Translate(carT.forward.normalized * speed); <------Non so perchè ma funziona male con la rotazione
        carT.position += carT.forward.normalized * speed;
    }

    //Rotazione della macchina
    private void TurnCar()
    {
        float rotation = Input.GetAxis("Horizontal") * turningRate * Time.deltaTime;
        carT.Rotate(0, rotation, 0);
    }
}
