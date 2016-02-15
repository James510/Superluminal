using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public bool selected = false;
    public float floorOffset = 1;
    public float speed = 5.0f;
    public float stopDistanceOffset = 1;
    public float rotSpeed;
    private Vector3 moveToDest = Vector3.zero;
    private Rigidbody rb;
    private bool selectedByClick=false;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(GetComponent<Renderer>().isVisible&&Input.GetMouseButton(0))
        {
            if(!selectedByClick)
            {
                Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
                camPos.y = CameraOperator.InvertMouseY(camPos.y);
                selected = CameraOperator.selection.Contains(camPos);
            }
            if (selected)
                GetComponent<Renderer>().material.color = Color.red;
            else
                GetComponent<Renderer>().material.color = Color.white;
        }
        if (selected && Input.GetMouseButtonUp(1))
        {
            Vector3 destination = CameraOperator.GetDestination();

            if(destination != Vector3.zero)
            {
                //gameObject.GetComponent<NavMeshAgent>().SetDestination(destination); //Unity Pro
                moveToDest = destination;
                moveToDest.y += floorOffset;
            }
        }

        UpdateMove();
    }
    void UpdateMove()
    {
        if(moveToDest != Vector3.zero && transform.position != moveToDest)
        {
            /*Vector3 direction = (moveToDest - transform.position).normalized;
            direction.y = 0;
            rb.velocity = direction * speed;*/

            Vector3 toTarget = moveToDest - transform.position;
            float step = rotSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, toTarget, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
            float dist = Vector3.Distance(moveToDest, transform.position);
            rb.velocity = transform.forward * speed;

            if (Vector3.Distance(transform.position, moveToDest) < stopDistanceOffset)
                moveToDest = Vector3.zero;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void OnMouseDown()
    {
        selectedByClick = true;
        selected = true;
    }
    void OnMouseUp()
    {
        if (selectedByClick)
            selected = true;
        selectedByClick = false;
    }
}
