using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CoreScript : MonoBehaviour
{
    public string file;
    private string line;
    public List<float> parts = new List<float>();
    public List<GameObject> prefabs = new List<GameObject>();

    void Start()
    {
        LoadShip(file);
    }

    void LoadShip(string name)
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        ClearList();
        StreamReader s = File.OpenText(file + ".shp");
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
            Instantiate(prefabs[(int)parts[i]], new Vector3(transform.position.x+parts[i + 1], transform.position.y + parts[i + 2], transform.position.z + parts[i + 3]), transform.rotation);
        }
    }

    void SaveShip()
    {
        foreach (Transform child in transform)
        {
            parts.Add(child.GetComponent<ChildPartScript>().prefabNum);
            parts.Add(child.transform.localPosition.x);
            parts.Add(child.transform.localPosition.y);
            parts.Add(child.transform.localPosition.z);
        }

        StreamWriter w = File.CreateText(file + ".shp");
        for (int i = 0; i < parts.Count; i++)
            w.WriteLine(parts[i]);
        w.Close();
    }

    void ClearList()
    {
        parts.Clear();
    }
}