using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGBColor : MonoBehaviour
{
    private Color randomcolor; 

    void Start()
    {
        randomcolor = new Color(Random.Range(0.1f, 1), Random.Range(0.1f, 1), Random.Range(0.1f, 1));
        GetComponent<SpriteRenderer>().color= randomcolor;

    }

    
}
