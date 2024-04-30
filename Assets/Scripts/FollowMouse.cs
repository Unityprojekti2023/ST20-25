using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Camera caliperCamera; // Camera for the measuring table
    public GameObject table; // table object for the calipers height

    public Transform body;
    public Transform gib;
    public Transform screen;
    public Transform slide;
    public Transform hexa1;
    public Transform hexa2;
    public Transform flatScrew;

    public TextInformation caliperTextUI;
    public TextMesh caliperText;

    private Transform[] moveObjects;

    public float yOffset = 8f;  // offset to raise the caliper on the table
    public float scrollSpeed = 0.1f;

    public float maxXPosition = 0f;
    public float minXPosition = -0.2f;

    public float maxXScrew = -0.02974f;
    public float minXScrew = -0.22974f;

    private bool isRotated = false;
    private bool canRotate = true;

    private void Start()
    {
        // I got the print for the caliper and the moving parts are not grouped in there so we put then in array to
        // to move them together
        moveObjects = new Transform[] { body, gib, screen, slide, hexa1, hexa2};
    }
    void Update()
    {
        if (caliperCamera.gameObject.activeSelf)
        {
            canRotate = true;
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

            //ray from the camera to the mouse position
            Ray ray = caliperCamera.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 offset = hit.point - transform.position;

                //keep y-position flat so it doenst jump with the objects on the table
                transform.position += new Vector3(offset.x, 0f, offset.z);

                //screens position to the caliper and display as text
                float relativeXPosition = screen.localPosition.x *-100;
                caliperText.text = relativeXPosition.ToString("F3");
                caliperTextUI.UpdateText(relativeXPosition.ToString("F3"));
            }

            if (canRotate && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E)))
            {
                StartCoroutine(RotateCoroutine());
            }

        }

    }
    IEnumerator RotateCoroutine()
    {
        canRotate = false;

        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
        {
            if (!isRotated)
            {
                transform.Rotate(0f, -90f, 0f);
                isRotated = true;
            }
            else
            {
                transform.Rotate(0f, 90f, 0f);
                isRotated = false;
            }
        }
        yield return new WaitForSeconds(0.2f);
        canRotate = true;
    }
}
