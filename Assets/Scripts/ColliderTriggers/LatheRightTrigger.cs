using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheRightTrigger : MonoBehaviour
{
    [Header("References to other scripts")]
    public Test test;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== "LatheTrigger")
        {
            test.moveLatheLeft = true;
            test.moveLatheRight = false;
            test.counter++;
        }
    }   

    private void OnTriggerExit(Collider other) 
    {
        
    }
}
