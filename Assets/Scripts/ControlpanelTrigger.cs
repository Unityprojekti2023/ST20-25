using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlpanelTrigger : MonoBehaviour
{
    public bool isPlayerNearControlpanel = false;
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerNearControlpanel = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerNearControlpanel = false;
        }
    }
}
