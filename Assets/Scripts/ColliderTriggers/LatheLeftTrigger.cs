using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatheLeftTrigger : MonoBehaviour
{
    [Header("References to other scripts")]
    public Test test;

    [Header("Other values")]
    public bool isLatheAtLeftPosition = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== "LatheTrigger")
        {
            test.moveLatheLeft = false;
            test.moveLatheRight = true;
        }
    }   
}
