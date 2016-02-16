using UnityEngine;
using System.Collections;

public class MainCameraMover : MonoBehaviour
{
    public float ScrollSpeed = 15;
    public float ScrollEdge = 0.1f;
    public float PanSpeed = 10;
    private Vector3 initialPosition;
	void Update ()
    {
        if (Input.GetKey("d") && !Input.GetKey("a"))
            transform.Translate(Vector3.right * Time.deltaTime * PanSpeed, Space.Self);
        else if (Input.GetKey("a") && !Input.GetKey("d"))
            transform.Translate(Vector3.right * Time.deltaTime * -PanSpeed, Space.Self);
        if (Input.GetKey("w") && !Input.GetKey("s"))// || Input.mousePosition.y >= Screen.height * (1 - ScrollEdge) //Scroll when at edge
            transform.Translate(Vector3.forward * Time.deltaTime * PanSpeed, Space.Self);
        else if (Input.GetKey("s") && !Input.GetKey("w"))// || Input.mousePosition.y <= Screen.height * ScrollEdge
            transform.Translate(Vector3.forward * Time.deltaTime * -PanSpeed, Space.Self);

        /*if (Input.GetKey("q") || Input.mousePosition.x <= Screen.width * ScrollEdge) //Rotate when at edge
        {
            transform.Rotate(Vector3.up * Time.deltaTime * -rotateSpeed, Space.World);
        }
        else if (Input.GetKey("e") || Input.mousePosition.x >= Screen.width * (1 - ScrollEdge))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.World);
        }*/
    }
}
