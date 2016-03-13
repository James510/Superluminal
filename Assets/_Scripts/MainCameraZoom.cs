using UnityEngine;
using System.Collections;

public class MainCameraZoom : MonoBehaviour
{
    public Vector2 zoomRange = new Vector2(-10, 100);
    public float CurrentZoom = 0;
    public float ZoomZpeed = 1;
    public float ZoomRotation = 1;
    public Vector2 zoomAngleRange = new Vector2(20, 70);
    public float rotateSpeed = 10;
    private Vector3 initialPosition;
    private Vector3 initialRotation;
    private float x = 0;
    private float y = 0;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.eulerAngles;
    }


    void Update()
    {
        // panning     
        /*if (Input.GetMouseButton(0))
        {
            transform.Translate(Vector3.right * Time.deltaTime * PanSpeed * (Input.mousePosition.x - Screen.width * 0.5f) / (Screen.width * 0.5f), Space.World);
            transform.Translate(Vector3.forward * Time.deltaTime * PanSpeed * (Input.mousePosition.y - Screen.height * 0.5f) / (Screen.height * 0.5f), Space.World);
        }*/

        // zoom in/out
        CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 1000 * ZoomZpeed;

        CurrentZoom = Mathf.Clamp(CurrentZoom, zoomRange.x, zoomRange.y);

        transform.position = new Vector3(transform.position.x, transform.position.y - (transform.position.y - (initialPosition.y + CurrentZoom)) * 0.1f, transform.position.z);

        //float x = transform.eulerAngles.x - (transform.eulerAngles.x - (initialRotation.x + CurrentZoom * ZoomRotation)) * 0.1f;
        //x = Mathf.Clamp(x, zoomAngleRange.x, zoomAngleRange.y);

        //transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, transform.eulerAngles.z);

        /*if (Input.GetMouseButton(2) && Input.mousePosition.x >= Screen.width * (-1))
        {
            transform.eulerAngles = new Vector3(Input.mousePosition.x, transform.eulerAngles.y, transform.eulerAngles.z);
        }*/
        if (Input.GetMouseButton(2))
        {
            x += Input.GetAxis("Mouse X") * 5;
            y -= Input.GetAxis("Mouse Y") * 5;
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            transform.rotation = rotation;
        }
    }
}