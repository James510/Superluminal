using UnityEngine;
using System.Collections;

public class MainCameraMover : MonoBehaviour
{
    public float ScrollSpeed = 15;

    public float ScrollEdge = 0.1f;

    public float PanSpeed = 10;

    public Vector2 zoomRange = new Vector2(-10, 100);

    public float CurrentZoom = 0;

    public float ZoomZpeed = 1;

    public float ZoomRotation = 1;

    public Vector2 zoomAngleRange = new Vector2(20, 70);

    public float rotateSpeed = 10;

    private Vector3 initialPosition;

    private Vector3 initialRotation;

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey("d") && !Input.GetKey("a"))
        {
            transform.Translate(Vector3.right * Time.deltaTime * PanSpeed, Space.Self);
        }
        else if (Input.GetKey("a") && !Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * Time.deltaTime * -PanSpeed, Space.Self);
        }

        if (Input.GetKey("w") && !Input.GetKey("s"))// || Input.mousePosition.y >= Screen.height * (1 - ScrollEdge)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * PanSpeed, Space.Self);
        }
        else if (Input.GetKey("s") && !Input.GetKey("w"))// || Input.mousePosition.y <= Screen.height * ScrollEdge
        {
            transform.Translate(Vector3.forward * Time.deltaTime * -PanSpeed, Space.Self);
        }

        /*if (Input.GetKey("q") || Input.mousePosition.x <= Screen.width * ScrollEdge)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * -rotateSpeed, Space.World);
        }
        else if (Input.GetKey("e") || Input.mousePosition.x >= Screen.width * (1 - ScrollEdge))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.World);
        }*/
    }
}
