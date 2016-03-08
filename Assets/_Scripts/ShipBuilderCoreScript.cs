using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class ShipBuilderCoreScript : MonoBehaviour
{
    public GameObject[] prefabList;
    public Button prefabButtonTemp;
    public Button loadShipButtonTemp;
    public GameObject loadMenuContent;
    public GameObject loadMenu;
    public GameObject core;
    public Text shipName;

    // Use this for initialization
    void Start ()
    {
        PlayerPrefs.SetInt("EditorMode", 1);
        loadMenu.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
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
            file = "Untitled Ship";
        core.SendMessage("SaveShip", file);
    }

    void LoadPrefab(int prefab)
    {
        GameObject clone = Instantiate(prefabList[prefab], new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z),transform.rotation) as GameObject;

    }

    void LoadButtons()
    {

    }
}
