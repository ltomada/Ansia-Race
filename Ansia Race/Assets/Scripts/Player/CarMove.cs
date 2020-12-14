using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    [Header ("")]
    [Header ("Standard Movement Settings")]
    public float maxSpeed;
    public float speed = 0f;
    public float acceleration = 0.5f;
    public float turningRate = 30f;
    public float slowPercentage = 70f;
    public float slowedMaxSpeed = 0.1f;
    public float slowingDeceleration = 0.09f;
    public float realMaxSpeed;
    public float realTurningRate;

    [Header("")]
    [Header("Boost Settings")]
    [Header("")]
    public float boostSpeed = 0f;
    public float boostAccelerationTime = 0f;
    public float boostSpeedTime = 0f;
    public float boostDecelerationTime = 0f;

    public bool boosting = false;
    public bool boostRelease = false;

    public float fullBoostTimer = 0f;
    public float accelTimer = 0f;
    public float boostspeedTimer = 0f;
    public float decelTimer = 0f;

    [Header("")]
    [Header("Breaks Settings")]
    [Header("")]
    public float breaksIntegrity = 1f;
    public float breaksDeceleration = 0f;

    //Other private variables
    private float lerpMoment = 0f;


    void Start()
    {
        fullBoostTimer = boostAccelerationTime + boostSpeedTime + boostDecelerationTime;
        accelTimer = boostAccelerationTime;
        boostspeedTimer = boostSpeedTime;
        decelTimer = boostDecelerationTime;
        realMaxSpeed = maxSpeed;
        realTurningRate = turningRate;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && breaksIntegrity > 0)
            Breaking();
        else
            Moving();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Collisione con Ostacolo
        if (other.tag == "Obstacle")
            HitObstacle(other);

        //Corsia lenta
        if (other.tag == "SlowArea")
        {
            maxSpeed = slowedMaxSpeed;
            SlowMove();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SlowArea")
        {
            maxSpeed = realMaxSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Collisione con muro
        if (collision.gameObject.tag == "Wall")
            HitWall();

        //Zona rallentante
        if (collision.gameObject.tag == "SlowArea")
        {
            maxSpeed = slowedMaxSpeed;
            SlowMove();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //Zona rallentante
        if (collision.gameObject.tag == "SlowArea")
            maxSpeed = realMaxSpeed;
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

    //Boost e decelerazione
    private void Boost()
    {
        if ((accelTimer > 0) || (boostspeedTimer > 0))          //accelera
        {
            speed += LerpNumber(speed, boostSpeed, boostAccelerationTime);
            accelTimer -= Time.deltaTime;
            if (accelTimer <= 0)                  //stasi
            {
                speed = boostSpeed;
                boostspeedTimer -= Time.deltaTime;
            }
            transform.position += transform.forward.normalized * speed;
        }
        else              //decelera
        {
            boosting = false;
            boostRelease = true;
            if ((decelTimer > 0) && (speed > maxSpeed))
            {
                speed += LerpNumber(speed, maxSpeed, boostDecelerationTime);
                decelTimer -= Time.deltaTime;
                transform.position += transform.forward.normalized * speed;
            }
            else           //resetta
            {
                BoostTimersReset();
                boostRelease = false;
                this.GetComponent<BoostPieces>().UnmountPart();
            }
        }
    }

    //Collisione con ostacolo
    private void HitObstacle(Collider other)
    {
        Destroy(other.gameObject);
        speed *= (1f - (slowPercentage / 100f));
    }

    //Collisione con muro
    private void HitWall()
    {
        speed *= (1f - (slowPercentage / 100f));
    }

    //Per checkare se stare fermo o partire
    private void Moving()
    {
        
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && !boosting && !boostRelease && this.GetComponent<BoostPieces>().boostPossible)
        {
            boosting = true;
            Debug.Log("Dovrebbe funzionare");
        }

        if (boosting || boostRelease)
            Boost();
        else
            SpeedUp();

         TurnCar();
       
    }

    //Freno
    private void Breaking()
    {
        breaksIntegrity -= Time.deltaTime;
        if (speed > 0)
        {
            speed -= breaksDeceleration * Time.deltaTime;
            TurnCar();
        }
        else
        {
            breaksIntegrity += Time.deltaTime;
        }
        speed = Mathf.Clamp(speed, 0f, speed);
        transform.position += transform.forward.normalized * speed;
    }

    //Zona rallentata
    private void SlowMove()
    {
        if (speed > slowedMaxSpeed)
            speed -= slowingDeceleration * Time.deltaTime;
        else
            speed = Mathf.Clamp(speed, 0, maxSpeed);
    }

    //Per gestire curve di movimento (not really)
    private float LerpNumber(float a, float b, float time)
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            lerpMoment += (b - a) * Time.deltaTime;
            lerpMoment = Mathf.Clamp(lerpMoment, 0f, (b - a));
            return lerpMoment;
        }
        else
        {
            lerpMoment = 0f;
            return lerpMoment;
        }
    }

    //Reset boost timers
    private void BoostTimersReset()
    {
        fullBoostTimer = boostAccelerationTime + boostSpeedTime + boostDecelerationTime;
        accelTimer = boostAccelerationTime;
        boostspeedTimer = boostSpeedTime;
        decelTimer = boostDecelerationTime;
    }
}
