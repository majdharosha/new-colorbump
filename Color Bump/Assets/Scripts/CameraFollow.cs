using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float camspeed = 6;
    public Vector3 camvelocity; 

    void Start()
    {
       

    }

    void Update()
    {
        if (FindObjectOfType<PlayerController>().canMove)
        transform.position += Vector3.forward * camspeed ; 
        camvelocity = Vector3.forward * camspeed ;
    }
}
