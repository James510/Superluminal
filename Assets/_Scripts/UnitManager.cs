using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnitManager : MonoBehaviour
{
    public List<GameObject> selectedUnits;
    public GameObject[] enemies;
    public GameObject[] friends;
    //public List<List<GameObject>> unitList;
    public GameObject moveEffectObject;

    // Use this for initialization
    void Start()
    {
        //May have to initialize list
        StartCoroutine("UnitList");
        selectedUnits.Clear();
    }

    IEnumerator UnitList()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        friends = GameObject.FindGameObjectsWithTag("Friend");
        for (int x = 0; x < friends.Length; x++)
            friends[x].SendMessage("EnemyList", enemies);
        for (int x = 0; x < enemies.Length; x++)
            enemies[x].SendMessage("EnemyList", friends);
        //Debug.Log(targets.Length);
        yield return new WaitForSeconds(0.1f);
        StartCoroutine("UnitList");
    }

    void Update() //Move click effect
    {
        if(Input.GetMouseButtonDown(1))
            Instantiate(moveEffectObject, CameraOperator.GetDestination(), moveEffectObject.transform.rotation);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        friends = GameObject.FindGameObjectsWithTag("Friend");
        for (int x = 0; x < friends.Length; x++)
            friends[x].SendMessage("EnemyList", enemies);
        for (int x = 0; x < enemies.Length; x++)
            enemies[x].SendMessage("EnemyList", friends);
        //Debug.Log(targets.Length);

    }

    public void DestOffset() //Formation movement
    {
        Vector3 destination = CameraOperator.GetDestination();
        
        float averageX=0,averageZ=0;
        for (int x = 0; x < selectedUnits.Count; x++)
        {
            averageX += selectedUnits[x].transform.position.x;
            averageZ += selectedUnits[x].transform.position.z;
        }

        averageX /= selectedUnits.Count;
        averageZ /= selectedUnits.Count;
       
        for (int x = 0; x < selectedUnits.Count; x++)
        {

            Vector3 Temp = destination;
            if(selectedUnits[x].transform.position.x <= averageX)
                Temp.x -= averageX-  selectedUnits[x].transform.position.x  ;
            else
            {
                Temp.x +=  selectedUnits[x].transform.position.x- averageX;
            }
            if (selectedUnits[x].transform.position.z <= averageZ)
                Temp.z -= averageZ -selectedUnits[x].transform.position.z ;
            else
            {
                Temp.z +=  selectedUnits[x].transform.position.z -  averageZ;
            }
            selectedUnits[x].SendMessage("SetDest", Temp);            
        }
    }

    public bool IsSelected(GameObject unit)
    {
        if (selectedUnits.Contains(unit))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SelectSingleUnit(GameObject unit)
    {
        selectedUnits.Clear();
        selectedUnits.Add(unit);
        //Debug.Log(selectedUnits);
    }

    public void SelectAdditionalUnit(GameObject unit)
    {
        selectedUnits.Add(unit);
    }

    public void DeselectAllUnits()
    {
        selectedUnits.Clear();
    }

    public List<GameObject> GetSelectedUnits()
    {
        return selectedUnits;
    }
}
