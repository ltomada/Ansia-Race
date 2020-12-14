using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPieces : MonoBehaviour
{
    public GameObject car;
    public GameObject boostObject;
    public bool boostPossible = true;
    public GameObject[] carParts;
    public bool[] carPartsCheck;
    public int c;

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
            carParts[partSelector].GetComponent<Rigidbody>().isKinematic = false;
            carParts[partSelector].GetComponent<Rigidbody>().useGravity = true;
            carParts[partSelector].transform.parent = null;
            carPartsCheck[partSelector] = false;
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
}
