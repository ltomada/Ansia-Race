using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPieces : MonoBehaviour
{
    [Header("")]
    [Header("Parts Settings")]
    public GameObject car;
    public GameObject boostObject;
    public bool boostPossible = true;
    public GameObject[] carParts;
    public bool[] carPartsCheck;
    public int c;
    public GameObject leftSpark;
    public GameObject rightSpark;

    [Header("")]
    [Header("Effects Settings")]
    [Header("")]
    public float controlRate = 0.5f;
    public float speedUp = 0.15f;


    private int controlCounter = 0;

    void Start()
    {
        for (int i = 0; i < carPartsCheck.Length; i++)
            carPartsCheck[i] = true;
        c = 0;
    }

    void Update()
    {
        
    }

    public void UnmountPart()
    {
        if (c < carParts.Length)
        {
            //check parte da smontare
            int partSelector = (int)Random.Range(0, carParts.Length);
            partSelector = Mathf.Clamp(partSelector, 0, carParts.Length - 1);
            while (carPartsCheck[partSelector] == false)
            {
                partSelector = (int)Random.Range(0, carParts.Length);
                partSelector = Mathf.Clamp(partSelector, 0, carParts.Length - 1);
            }

            //smonta il pezzo
            if (carParts[partSelector].name == "ruota_AS")
                leftSpark.SetActive(true);

            if (carParts[partSelector].name == "ruota_AD")
                rightSpark.SetActive(true);

                carParts[partSelector].AddComponent<Rigidbody>();
            carParts[partSelector].GetComponent<Rigidbody>().isKinematic = false;
            carParts[partSelector].GetComponent<Rigidbody>().useGravity = true;
            carParts[partSelector].transform.parent = null;
            carPartsCheck[partSelector] = false;
            ExtraEffect(carParts[partSelector]);
            c ++;
        }
        else
        {
            boostObject.GetComponent<Rigidbody>().isKinematic = false;
            boostObject.GetComponent<Rigidbody>().useGravity = true;
            boostObject.transform.parent = null;
            boostPossible = false;
        }
    }

    public void ExtraEffect(GameObject pezzo)
    {
        string tag = pezzo.tag;

        //effetti vari
        if (tag == "Ruota")
        {
            controlCounter ++;
            this.GetComponent<CarMove>().turningRate = this.GetComponent<CarMove>().realTurningRate / (1f + (controlRate * controlCounter));
        }

        if (tag == "Peso")
        {
            this.GetComponent<CarMove>().realMaxSpeed += speedUp;
        }
    }
}
