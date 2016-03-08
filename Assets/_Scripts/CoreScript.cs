using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CoreScript : MonoBehaviour
{
    private string line;
    public Text shipName;   
    public List<float> parts = new List<float>();
    public List<GameObject> prefabs = new List<GameObject>();

    void Start()
    {
        Unit self = GetComponent<Unit>();
        self.hp += 100;
        //LoadShip("frigatetest");
    }

    void LoadShip(string file)
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        ClearList();
        StreamReader s = File.OpenText("C:\\Users\\James510\\Desktop\\Auragon\\Superluminal\\Ships\\"+file);
        line = s.ReadLine();
        while (line != null)
        {
            float x = 0;
            if (float.TryParse(line, out x))
            {
                parts.Add(float.Parse(line));
            }
            line = s.ReadLine();
        }

        for (int i = 0; i < parts.Count; i+=4)
        {
            GameObject clone = Instantiate(prefabs[(int)parts[i]], new Vector3(transform.position.x+parts[i + 1], transform.position.y + parts[i + 2], transform.position.z + parts[i + 3]), transform.rotation) as GameObject;
            clone.transform.SetParent(transform);
        }
    }

    void SaveShip(string file)
    {
        foreach (Transform child in transform)
        {
            if(child.tag!="GUINon")
            {
                parts.Add(child.GetComponent<ChildPartScript>().prefabNum);
                parts.Add(child.transform.localPosition.x);
                parts.Add(child.transform.localPosition.y);
                parts.Add(child.transform.localPosition.z);
            }
        }

        StreamWriter w = File.CreateText("C:\\Users\\James510\\Desktop\\Auragon\\Superluminal\\Ships\\" + file + ".shp");
        for (int i = 0; i < parts.Count; i++)
            w.WriteLine(parts[i]);
        w.Close();
    }

    void ClearList()
    {
        parts.Clear();
    }
}