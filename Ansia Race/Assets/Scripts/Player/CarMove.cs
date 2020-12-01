using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    public float maxSpeed;
    public float speed = 0f;
    public float acceleration = 0.5f;
    public float turningRate = 30f;
    public float slowPercentage = 40f;

    void Start()
    {

    }

    void Update()
    {
        SpeedUp();
        TurnCar();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
            HitObstacle(other);
    }

    //Accelerazione della macchina
    private void SpeedUp()
    {
        speed += acceleration * Time.deltaTime;
        speed = Mathf.Clamp(speed, 0f, maxSpeed);
        //carT.Translate(carT.forward.normalized * speed); <------Non so perchè ma funziona male con la rotazione
        transform.position += transform.forward.normalized * speed;
    }

    //Rotazione della macchina
    private void TurnCar()
    {
        float rotation = Input.GetAxis("Horizontal") * turningRate * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }

    //Collisione con ostacolo
    private void HitObstacle(Collider other)
    {
        Destroy(other.gameObject);
        speed *= (1f - (slowPercentage / 100f));
    }
}
