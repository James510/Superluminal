using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Unit : MonoBehaviour
{
    public bool selected = false;
    public int hp;
    public float floorOffset = 1;
    public float speed = 5.0f;
    public float stopDistanceOffset = 1;
    public float rotSpeed;
    public bool hasMainCannon = false;
    public float maxDistance=1000;
    public List<GameObject> turrets;
    public GameObject selectionCircle;
    public GameObject[] enemies;
    public GameObject unitManager;
    public GameObject explosionFX;
    public GameObject deathExplosionFX;
    public bool isEnemy;
    private Vector3 moveToDest = Vector3.zero;
    private bool selectedByClick=false;
    private bool selectedList = false;
    private bool hasTarget = false;
    private bool isAlive = true;
    private GameObject target;
	
	// Update is called once per frame
    void Start()
    {
        unitManager = GameObject.FindGameObjectWithTag("UnitManager");
        GameObject select = Instantiate(selectionCircle, transform.position, transform.rotation) as GameObject;
        select.transform.parent = transform;
        StartCoroutine("TargetAquisition",Random.Range(0.1f,0.8f));
        // selectionCircle.GetComponent<SpriteRenderer>().enabled = false;
        //GetComponent<ParticleSystem>().Emit(1); //Fire Main Cannon
        if (isEnemy)
        {
            transform.tag = "Enemy";
            gameObject.layer = 9;
            foreach (Transform child in transform)
            {
                if (child.tag == "140mm" || child.tag == "400mm")
                {
                    child.GetComponent<TurretScript>().isEnemy = true;
                }
            }
        }
        int turretTemp = 0;
        foreach (Transform child in transform)
        {
            child.gameObject.layer = 10;
            if (child.tag == "140mm" || child.tag == "400mm")
            {
                turrets.Add(child.gameObject);
                turretTemp++;
            }
        }

    }

    IEnumerator TargetAquisition(float offset)
    {
        if(isAlive)
        {
            if (enemies.Length > 0)
            {
                //if (!hasTarget)
                //{
                target = enemies[0];
                for (int x = 0; x < enemies.Length; x++)
                {
                    if (Vector3.Distance(transform.position, enemies[x].transform.position) < Vector3.Distance(transform.position, target.transform.position))
                        target = enemies[x];
                }
                foreach (Transform child in transform)
                {
                    if (Vector3.Distance(transform.position, target.transform.position) < 500 && child.tag == "140mm")//set vector3 distance as a local variable instead
                    {
                        //Debug.Log("Firing");
                        hasTarget = true;
                        for (int y = 0; y < turrets.Count; y++)
                            turrets[y].SendMessage("SetTarget", target);
                    }
                    if (Vector3.Distance(transform.position, target.transform.position) < 1000 && child.tag == "400mm")
                    {
                        hasTarget = true;
                        for (int y = 0; y < turrets.Count; y++)
                            turrets[y].SendMessage("SetTarget", target);
                    }
                }
                //}
                if (hasTarget && target == null)
                {
                    hasTarget = false;
                }
            }
            yield return new WaitForSeconds(2.0f + offset);
            StartCoroutine("TargetAquisition", offset);

        }
    }


    void EnemyList (GameObject[] list)
    {
        enemies = list;
    }

	void Update ()
    {
        if(isAlive)
        {
            if (hp < 1)
            {
                if(selected)
                {
                    unitManager.GetComponent<UnitManager>().DeselectUnit(this.gameObject);
                }
                Instantiate(deathExplosionFX, transform.position, transform.rotation);
                foreach (Transform child in transform)
                {
                    child.SendMessage("Deactivate");
                }
                transform.DetachChildren();
                isAlive = false;
                Destroy(this.gameObject);
            }
            //Debug.Log(selectedList);
            if (GetComponent<Renderer>().isVisible && Input.GetMouseButton(0)) //Detect if unit is in view and select by dragging
            {
                if (!selectedByClick)
                {
                    Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
                    camPos.y = CameraOperator.InvertMouseY(camPos.y);
                    selected = CameraOperator.selection.Contains(camPos);
                }
                if (selected)
                {
                    GetComponent<Renderer>().material.color = Color.red;
                    if (selectedList == false)
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
            if (hasTarget && target == null)
            {
                hasTarget = false;
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if(isAlive)
        {
            //Debug.Log("Hit");
            hp -= other.GetComponent<TurretScript>().damage;
            Instantiate(explosionFX, new Vector3(transform.position.x + Random.Range(-4.0f, 4.0f), transform.position.y + Random.Range(-2.0f, 2.0f), transform.position.z + Random.Range(-4.0f, 4.0f)), transform.rotation);
        }
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
            //float dist = Vector3.Distance(moveToDest, transform.position);
            transform.Translate(Vector3.forward * speed);
            //transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z); // locks the z axis. will be changed in the future
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
