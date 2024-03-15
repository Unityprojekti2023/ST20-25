using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleJog : MonoBehaviour
{
    [Header("References to objects")]
    public Transform handleJog;

    [Header("References to other scripts")]
    public MouseControlPanelInteractable mouseControlPanelInteractable;

    public void updateJogPosition()
    {
        switch(mouseControlPanelInteractable.handleJogPosition)                     // Switch case for handle jog position, definitely not the best way to do this :D
        {
            case 1:
                handleJog.position = new Vector3(-202.32f, 136.81f, 227.42f);
            break;

            case 2:
                handleJog.position = new Vector3(-203.8f, 136.37f,  227.42f);
            break;

            case 3:
                handleJog.position = new Vector3(-204.5f, 135.08f, 227.42f);
            break;

            case 4:
                handleJog.position = new Vector3(-203.8f, 133.79f, 227.42f);
            break;

            case 5:
                handleJog.position = new Vector3(-202.32f, 133.36f, 227.42f);
            break;

            case 6:
                handleJog.position = new Vector3(-200.84f, 133.79f, 227.42f);
            break;

            case 7:
                handleJog.position = new Vector3(-200.2f, 135.08f, 227.42f);
            break;

            case 8:
                handleJog.position = new Vector3(-200.84f, 136.4f, 227.42f);
            break;
        }
    }
}
