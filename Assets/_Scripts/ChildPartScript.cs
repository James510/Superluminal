using UnityEngine;
using System.Collections;

public class ChildPartScript : MonoBehaviour
{
    public bool selected = false;
    public int prefabNum;
    public float speedMod;
    public int healthMod;
<<<<<<< HEAD
<<<<<<< HEAD
    public bool isMoving;
    private bool selectedByClick = false;
    private bool selectedList = false;
    private Color originalColor;
    public GameObject core;
    public GameObject shipBuilderCore;
=======
    private bool selectedByClick = false;
    private bool selectedList = false;
    private Color originalColor;
>>>>>>> origin/master
=======
    private bool selectedByClick = false;
    private bool selectedList = false;
    private Color originalColor;
>>>>>>> refs/remotes/origin/master

    void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
<<<<<<< HEAD
<<<<<<< HEAD
        GetComponentInParent<Unit>().hp += healthMod;
        GetComponentInParent<Unit>().speed += speedMod;
        if (PlayerPrefs.GetInt("EditorMode") == 1)
        {
            core = GameObject.FindGameObjectWithTag("Core");
            shipBuilderCore = GameObject.FindGameObjectWithTag("ShipBuilderCore");

        }
    }
    void Update()
    {
        if (PlayerPrefs.GetInt("EditorMode")==1)
        {
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
                        selectedList = true;
                    }

                }
                else
                {
                    if (selectedList == true)//Deselect units
                    {
                        selectedList = false;
                    }
                    GetComponent<Renderer>().material.color = originalColor;
                    foreach(Transform child in transform)
                    {
                        if (child.transform.tag == "Axis")
                            Destroy(child.gameObject);
                    }
                }
            }
            if (isMoving)
            {
                if (Input.GetMouseButtonDown(0)&&PlayerPrefs.GetInt("FirstSelect")==1)
                {
                    isMoving = false;
                    PlayerPrefs.SetInt("FirstSelect", 0);
                }
                //GetComponent<Collider>().enabled = false;
                transform.gameObject.layer = 2;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                // Casts the ray and get the first game object hit
                if(Physics.Raycast(ray, out hit))
                {
                    if(!hit.collider.CompareTag("Axis"))
                        transform.position = hit.point;
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 45.0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - 45.0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 45.0f, transform.rotation.eulerAngles.z);
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 45.0f, transform.rotation.eulerAngles.z);
                    if (Input.GetKeyDown(KeyCode.PageUp))
                        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 45.0f);
                    if (Input.GetKeyDown(KeyCode.PageDown))
                        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 45.0f);
                    if (Input.GetKey(KeyCode.Z))
                    {
                        if (hit.point.z < hit.transform.position.z)
                            transform.position = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y), Mathf.Floor(hit.point.z));
                        else
                            transform.position = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y), Mathf.Ceil(hit.point.z));
                    }
                    if (Input.GetKey(KeyCode.X))
                    {
                        if (hit.point.x < hit.transform.position.x)
                            transform.position = new Vector3(Mathf.Floor(hit.point.x), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z));
                        else
                            transform.position = new Vector3(Mathf.Ceil(hit.point.x), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z));
                    }
                    if (Input.GetKey(KeyCode.C))
                    {
                        if (hit.point.y < hit.transform.position.y)
                            transform.position = new Vector3(Mathf.Round(hit.point.x), Mathf.Floor(hit.point.y), Mathf.Round(hit.point.z));
                        else
                            transform.position = new Vector3(Mathf.Round(hit.point.x), Mathf.Ceil(hit.point.y), Mathf.Round(hit.point.z));
                    }
                    if (Input.GetKeyDown(KeyCode.Delete))
                        Destroy(this.gameObject);
                }
            }
            else
            {
                transform.gameObject.layer = 10;
                //GetComponent<Collider>().enabled = true;
            }
        }
    }

    void OnDestroy()
=======
=======
>>>>>>> refs/remotes/origin/master
        if (PlayerPrefs.GetInt("EditorMode") == 0)
        {
            GetComponentInParent<Unit>().hp += healthMod;
            GetComponentInParent<Unit>().speed += speedMod;
        }
<<<<<<< HEAD
    }
    void Update()
>>>>>>> origin/master
    {
        GetComponentInParent<Unit>().hp -= healthMod;
        GetComponentInParent<Unit>().speed -= speedMod;
    }
    void OnMouseDown() //Click selection
    {
        if (PlayerPrefs.GetInt("EditorMode") == 1)
        {
            bool hasAxis = false;
            selectedByClick = true;
            selected = true;
            foreach (Transform child in transform)
            {
                if (child.transform.tag == "Axis")
                    hasAxis = true;
            }
            if(!hasAxis)
                shipBuilderCore.GetComponent<ShipBuilderCoreScript>().SendMessage("CreateAxis", this.gameObject);

            if (PlayerPrefs.GetInt("Selecting") == 0)
            {
                isMoving = true;
                PlayerPrefs.SetInt("Selecting", 1);
            }
        }
        //unitManager.GetComponent<UnitManager>().SelectSingleUnit(this.gameObject);

    }

    void OnMouseUp()
    {
        if (PlayerPrefs.GetInt("EditorMode") == 1)
        {
<<<<<<< HEAD
            if (selectedByClick)
                selected = true;
            selectedByClick = false;
            isMoving = false;
            PlayerPrefs.SetInt("Selecting", 0);
=======
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
                        selectedList = true;
                    }

                }
                else
                {
                    if (selectedList == true)//Deselect units
                    {
                        selectedList = false;
                    }
                    GetComponent<Renderer>().material.color = originalColor;
                }
            }
        }
=======
>>>>>>> refs/remotes/origin/master
    }
    void Update()
    {
        if(PlayerPrefs.GetInt("EditorMode")==0)
        {
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
                        selectedList = true;
                    }

                }
                else
                {
                    if (selectedList == true)//Deselect units
                    {
                        selectedList = false;
                    }
                    GetComponent<Renderer>().material.color = originalColor;
                }
            }
        }
    }
    void OnDestroy()
    {
        if(PlayerPrefs.GetInt("EditorMode")==0)
        {
            GetComponentInParent<Unit>().hp -= healthMod;
            GetComponentInParent<Unit>().speed -= speedMod;
>>>>>>> origin/master
        }
    }
    void OnMouseDown() //Click selection
    {
        if (PlayerPrefs.GetInt("EditorMode") == 1)
        {
            selectedByClick = true;
            selected = true;
        }
        //unitManager.GetComponent<UnitManager>().SelectSingleUnit(this.gameObject);

    }

    void OnMouseUp()
    {
        if (PlayerPrefs.GetInt("EditorMode") == 1)
        {
            if (selectedByClick)
                selected = true;
            selectedByClick = false;
        }
    }
    void OnMouseDown() //Click selection
    {
        if (PlayerPrefs.GetInt("EditorMode") == 1)
        {
            selectedByClick = true;
            selected = true;
        }
        //unitManager.GetComponent<UnitManager>().SelectSingleUnit(this.gameObject);

    }

    void OnMouseUp()
    {
        if (PlayerPrefs.GetInt("EditorMode") == 1)
        {
            if (selectedByClick)
                selected = true;
            selectedByClick = false;
        }
    }
}
