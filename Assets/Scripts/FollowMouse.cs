using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Camera caliperCamera; // Camera for the measuring table
    public GameObject table; // table object for the calipers height

    public Transform body;
    public Transform gib;
    public Transform scale;
    public Transform screen;
    public Transform slide;
    public Transform hexa1;
    public Transform hexa2;
    public Transform flatScrew;

    private Transform[] moveObjects;

    public float yOffset = 8f;  // offset to raise the caliper on the table
    public float scrollSpeed = 0.1f;

    public float maxXPosition = 0f;
    public float minXPosition = -0.1f;

    public float maxXScrew = -0.02974f;
    public float minXScrew = -0.12974f;

    private void Start()
    {
        // I got the print for the caliper and the moving parts are not grouped in there so we put then in array to
        // to move them together
        moveObjects = new Transform[] { body, gib, scale, screen, slide, hexa1, hexa2};
    }
    void Update()
    {
        if (caliperCamera.gameObject.activeSelf)
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel"); //mouse scroll input

            foreach (Transform moveObject in moveObjects)
            { 
                //moving group with mousescroll input and speed and max values
                Vector3 newPosition = moveObject.localPosition + Vector3.right * scrollInput * scrollSpeed;
                newPosition.x = Mathf.Clamp(newPosition.x, minXPosition, maxXPosition);
                moveObject.localPosition = newPosition;
            }
            //the screws relative position is messed up in the prefab so I handled its movement seperately
            Vector3 newPosition2 = flatScrew.localPosition + Vector3.right * scrollInput * scrollSpeed;
            newPosition2.x = Mathf.Clamp(newPosition2.x, minXScrew, maxXScrew);
            flatScrew.localPosition = newPosition2;


            //body.Translate(Vector3.right * scrollInput * scrollSpeed, Space.World);
            //gib.Translate(Vector3.right * scrollInput * scrollSpeed, Space.World);
            //scale.Translate(Vector3.right * scrollInput * scrollSpeed, Space.World);
            //screen.Translate(Vector3.right * scrollInput * scrollSpeed, Space.World);
            //slide.Translate(Vector3.right * scrollInput * scrollSpeed, Space.World);
            //hexa1.Translate(Vector3.right * scrollInput * scrollSpeed, Space.World);
            //hexa2.Translate(Vector3.right * scrollInput * scrollSpeed, Space.World);
            //flatScrew.Translate(Vector3.right * scrollInput * scrollSpeed, Space.World);

            Vector3 mousePosition = Input.mousePosition;
            //mouseposition from screen to worldspace
            Vector3 worldPosition = caliperCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, caliperCamera.transform.position.z));
            //raise calipers y position from table
            worldPosition.y = table.transform.position.y + yOffset;
            //update objects position to mouse position
            transform.position = worldPosition;
            //object going fast and far compared to the mouse movemet, change calculations

        }

    }
}
