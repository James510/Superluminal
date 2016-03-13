using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ShipBuilderCoreScript : MonoBehaviour
{
    public List<GameObject> prefabList = new List<GameObject>();
    public Button prefabButtonTemp;
    public Button loadShipButtonTemp;
    public GameObject loadMenuContent;
    public GameObject loadMenu;
    public GameObject core;
    public GameObject prefabMenuContent;
    public Text shipName;
    public GameObject xAxis, yAxis, zAxis;

    void Start ()
    {
        PlayerPrefs.SetInt("EditorMode", 1);
        PlayerPrefs.SetInt("Selecting", 0);
        loadMenu.SetActive(false);
        core.SendMessage("GetList", prefabList);
        LoadButtons();
    }
	
    void CreateAxis(GameObject other)
    {
        GameObject x = Instantiate(xAxis, new Vector3(other.transform.position.x + 1,other.transform.position.y,other.transform.position.z), transform.rotation) as GameObject;
        GameObject y = Instantiate(yAxis, new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z), transform.rotation) as GameObject;
        GameObject z = Instantiate(zAxis, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z + 1), transform.rotation) as GameObject;
        x.transform.SetParent(other.transform);
        y.transform.SetParent(other.transform);
        z.transform.SetParent(other.transform);
    }

    void LoadShipMenu()
    {
        loadMenu.SetActive(true);
        StartCoroutine("CheckFiles");
        
    }
    IEnumerator CheckFiles()
    {
        int offset = 0;
        yield return new WaitForSeconds(0.01f);
        string[] files = Directory.GetFiles("C:\\Users\\James510\\Desktop\\Auragon\\Superluminal\\Ships");
        foreach (string file in files)
        {

            Button clone = Instantiate(loadShipButtonTemp, new Vector3(loadMenu.transform.position.x, loadMenu.transform.position.y + offset, 0), transform.rotation) as Button;
            clone.transform.SetParent(loadMenuContent.transform);
            clone.name = Path.GetFileName(file);
            offset -= 60;
        }
    }

    void LoadShip(string file)
    {
        core.SendMessage("LoadShip",file);
        foreach(Transform child in loadMenuContent.transform)
        {
            Destroy(child.gameObject);
        }
        loadMenu.SetActive(false);
    }
    void SaveShip()
    {
        string file = shipName.text.ToString();
        if (file == "")
            file = "Untitled Ship.shp";
        core.SendMessage("SaveShip", file);
    }

    void LoadPrefab(int prefab)
    {
        GameObject clone = Instantiate(prefabList[prefab], new Vector3(0.0f,0.0f,0.0f),transform.rotation) as GameObject;
        clone.GetComponent<ChildPartScript>().isMoving = true;
        clone.GetComponent<ChildPartScript>().prefabNum = prefab;
        clone.transform.SetParent(core.transform);
        PlayerPrefs.SetInt("Selecting", 1);
        PlayerPrefs.SetInt("FirstSelect", 1);
    }

    void LoadButtons()
    {
        int xOffset = -120;
        int yOffset = 0;
        int rowCount = 0;
        for(int i=0;i<prefabList.Count;i++)
        {
            Button instButton = Instantiate(prefabButtonTemp, new Vector3(prefabMenuContent.transform.position.x + xOffset, prefabMenuContent.transform.position.y + yOffset, 0.0f), transform.rotation) as Button;
            instButton.transform.SetParent(prefabMenuContent.transform,false);
            instButton.GetComponent<LoadPrefabButton>().prefabNum = i;  
            instButton.name = prefabList[i].name;
            xOffset += 120;
            rowCount++;
            if(rowCount==3)
            {
                rowCount = 0;
                yOffset -= 120;
                xOffset = -120;
            }
        }
    }
}
