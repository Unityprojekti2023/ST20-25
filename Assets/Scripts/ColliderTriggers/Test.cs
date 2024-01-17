using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform drill;

    public bool moveLatheLeft = true;
    public bool moveLatheRight = false;
    public int counter = 0;

    void Update()
    {
        if (moveLatheLeft && counter < 3) 
        {  
            drill.transform.Translate(5 * Time.deltaTime, 0f, 0f);
        }

        if (moveLatheRight) 
        {
            drill.transform.Translate(-5 * Time.deltaTime, 0f, 0f);
        }
    }
}
