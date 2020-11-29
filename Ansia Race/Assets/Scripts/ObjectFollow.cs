using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    public Vector3 offset = new Vector3(0f, 6.5f, -9f);
    public GameObject followObject;

    void Start()
    {
        
    }

    void Update()
    {
        if (followObject != null)
            this.gameObject.transform.position = followObject.transform.position + offset;
    }
}
