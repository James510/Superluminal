using UnityEngine;
using System.Collections;

public class ChildPartScript : MonoBehaviour
{
    public bool selected = false;
    public int prefabNum;
    public float speedMod;
    public int healthMod;
    private bool selectedByClick = false;
    private bool selectedList = false;
    private Color originalColor;

    void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
        if (PlayerPrefs.GetInt("EditorMode") == 0)
        {
            GetComponentInParent<Unit>().hp += healthMod;
            GetComponentInParent<Unit>().speed += speedMod;
        }
    }
    void Update()
    {
        if(PlayerPrefs.GetInt("EditorMode")==1)
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
