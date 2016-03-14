using UnityEngine;
using System.Collections;

public class MainCameraMover : MonoBehaviour
{
    public float ScrollSpeed = 50;
    public float rotateSpeed = 50;
    public float ScrollEdge = 0.1f;
    public float PanSpeed = 10;
    public float moveSpeed = 10;


	void Update ()
    {
        if (Input.GetKey("d") && !Input.GetKey("a"))
            transform.Translate(transform.right * Time.deltaTime * PanSpeed, Space.World);
        else if (Input.GetKey("a") && !Input.GetKey("d"))
            transform.Translate(transform.right * Time.deltaTime * -PanSpeed, Space.World);
        if (Input.GetKey("w") && !Input.GetKey("s"))// || Input.mousePosition.y >= Screen.height * (1 - ScrollEdge) //Scroll when at edge
            transform.Translate(transform.forward * Time.deltaTime * PanSpeed, Space.World);
        else if (Input.GetKey("s") && !Input.GetKey("w"))// || Input.mousePosition.y <= Screen.height * ScrollEdge
            transform.Translate(transform.forward * Time.deltaTime * -PanSpeed, Space.World);

        if (Input.GetKey("q")) // || Input.mousePosition.x <= Screen.width * ScrollEdge
        {
            transform.Rotate(Vector3.up * Time.deltaTime * -rotateSpeed, Space.World);
        }
        else if (Input.GetKey("e"))// || Input.mousePosition.x >= Screen.width * (1 - ScrollEdge)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.World);
        }
        /*if (Input.GetMouseButton(1))
        {
            transform.Translate(transform.right * -Input.GetAxis("Mouse X") * moveSpeed, Space.World);
            transform.Translate(transform.forward * -Input.GetAxis("Mouse Y") * moveSpeed, Space.World);
        }*/
        if(Input.GetMouseButtonDown(2))
        {
            transform.rotation = Quaternion.Euler(0.0f, transform.GetChild(0).eulerAngles.y, 0.0f);
        }
    }
}