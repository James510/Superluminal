using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public GameObject unitManager;
    public bool selected = false;
    public int hp;
    public float floorOffset = 1;
    public float speed = 5.0f;
    public float stopDistanceOffset = 1;
    public float rotSpeed;
    private Vector3 moveToDest = Vector3.zero;
    private bool selectedByClick=false;
    private bool selectedList = false;
    private GameObject[] targets;
    public GameObject selectionCircle;
	
	// Update is called once per frame
    void Start()
    {
        StartCoroutine("TargetAquisition",Random.Range(0.1f,0.5f));
       // selectionCircle.GetComponent<SpriteRenderer>().enabled = false;
    }

    IEnumerator TargetAquisition(float offset)
    {
        targets = GameObject.FindGameObjectsWithTag("Enemy"); //Easy fix for target aquisition
        //Debug.Log(targets.Length);
        yield return new WaitForSeconds(2.0f+offset);
        StartCoroutine("TargetAquisition", offset);
    }

	void Update ()
    {
        if (hp < 1)
        {
            Destroy(this.gameObject);
        }
        //Debug.Log(selectedList);
        if (GetComponent<Renderer>().isVisible&&Input.GetMouseButton(0)) //Detect if unit is in view and select by dragging
        {
            if(!selectedByClick)
            {
                Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
                camPos.y = CameraOperator.InvertMouseY(camPos.y);
                selected = CameraOperator.selection.Contains(camPos);
            }
            if (selected)
            {
                GetComponent<Renderer>().material.color = Color.red;
                if(selectedList==false)
                {
                    unitManager.GetComponent<UnitManager>().SelectAdditionalUnit(this.gameObject);
                    selectionCircle.GetComponent<SpriteRenderer>().enabled = true;
                    selectedList = true;
                }
                
            }
            else
            {
                if (selectedList == true)//Deselect units
                {
                    unitManager.GetComponent<UnitManager>().DeselectAllUnits();
                    selectionCircle.GetComponent<SpriteRenderer>().enabled = false;
                    selectedList = false;
                }
                GetComponent<Renderer>().material.color = Color.white;
            }
        }
        if (selected && Input.GetMouseButtonUp(1))//Move script
        {
            unitManager.GetComponent<UnitManager>().DestOffset();
            //Vector3 destination = CameraOperator.GetDestination();

            /*if(destination != Vector3.zero)
            {
                //gameObject.GetComponent<NavMeshAgent>().SetDestination(destination); //Unity Pro
               // moveToDest = destination;
                //moveToDest.y += floorOffset;
            }*/
        }

        UpdateMove();
    }

    void OnParticleCollision(GameObject other)
    {
        //Debug.Log("Hit");
        //other.GetComponent<ParticleSystem>();
    }

    public void Damage(int dmg)
    {
        hp -= dmg;
    }

    void SetDest(Vector3 dest)//Set destination as given by UnitManager
    {
        moveToDest = dest;
    }
    void UpdateMove()
    {
        if(moveToDest != Vector3.zero && transform.position != moveToDest) //Move to target
        {
            /*Vector3 direction = (moveToDest - transform.position).normalized;
            direction.y = 0;
            rb.velocity = direction * speed;*/

            Vector3 toTarget = moveToDest - transform.position;
            float step = rotSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, toTarget, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
            float dist = Vector3.Distance(moveToDest, transform.position);
            transform.Translate(Vector3.forward * speed);
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z); // locks the z axis. will be changed in the future
            if (Vector3.Distance(transform.position, moveToDest) < stopDistanceOffset) //If in range, stop
            {
                moveToDest = Vector3.zero;
            }
                
        }
        else //Stop
        {
            //rb.velocity = Vector3.zero;
        }
    }

    void OnMouseDown() //Click selection
    {
        selectedByClick = true;
        selected = true;
        //unitManager.GetComponent<UnitManager>().SelectSingleUnit(this.gameObject);
        
    }
    void OnMouseUp()
    {
        if (selectedByClick)
            selected = true;
        selectedByClick = false;
    }
}
