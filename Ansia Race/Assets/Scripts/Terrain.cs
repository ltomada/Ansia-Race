using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
            Debug.Log("Tocco!");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<CarMove>().cya = true;
        }
    }
}
