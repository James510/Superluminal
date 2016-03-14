using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CoreScript : MonoBehaviour
{
    private string line;
<<<<<<< HEAD
<<<<<<< HEAD
    public Text shipName;
=======
    public Text shipName;   
>>>>>>> origin/master
=======
    public Text shipName;   
>>>>>>> refs/remotes/origin/master
    public List<float> parts = new List<float>();
    public List<GameObject> prefabs = new List<GameObject>();

    void Start()
    {
        Unit self = GetComponent<Unit>();
        self.hp += 100;
        //LoadShip("frigatetest");
    }

    void GetList(List<GameObject> clone)
    {
        prefabs = clone;
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
<<<<<<< HEAD
        StreamWriter w;
        if (file.Contains(".shp"))
            w = File.CreateText("C:\\Users\\James510\\Desktop\\Auragon\\Superluminal\\Ships\\" + file);
        else
            w = File.CreateText("C:\\Users\\James510\\Desktop\\Auragon\\Superluminal\\Ships\\" + file + ".shp");
=======

        StreamWriter w = File.CreateText("C:\\Users\\James510\\Desktop\\Auragon\\Superluminal\\Ships\\" + file + ".shp");
<<<<<<< HEAD
>>>>>>> origin/master
=======
>>>>>>> refs/remotes/origin/master
        for (int i = 0; i < parts.Count; i++)
            w.WriteLine(parts[i]);
        w.Close();
    }

    void MirrorLeft()
    {
        List<GameObject> temp = new List<GameObject>();
        foreach(Transform child in transform)
        {
            GameObject clone;
            if (child.transform.localPosition.x < 0)
            {
                clone = Instantiate(child.gameObject, new Vector3(-1 * child.transform.localPosition.x, child.transform.position.y, child.transform.position.z), child.transform.rotation) as GameObject;
                clone.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                clone.transform.localRotation = new Quaternion(clone.transform.localRotation.x * -1.0f,clone.transform.localRotation.y,clone.transform.localRotation.z,clone.transform.localRotation.w * -1.0f);
                temp.Add(clone);
            }
            else if (child.transform.localPosition.x > 0)
                Destroy(child.gameObject);
        }
        for (int i = 0; i < temp.Count; i++)
            temp[i].transform.SetParent(this.transform);
    }

    void MirrorRight()
    {
        List<GameObject> temp = new List<GameObject>();
        foreach (Transform child in transform)
        {
            GameObject clone;
            if (child.transform.localPosition.x > 0)
            {
                clone = Instantiate(child.gameObject, new Vector3(-1 * child.transform.localPosition.x, child.transform.position.y, child.transform.position.z), child.transform.rotation) as GameObject;
                clone.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                clone.transform.localRotation = new Quaternion(clone.transform.localRotation.x * -1.0f, clone.transform.localRotation.y, clone.transform.localRotation.z, clone.transform.localRotation.w * -1.0f);
                temp.Add(clone);
            }
            else if (child.transform.localPosition.x < 0)
                Destroy(child.gameObject);
        }
        for (int i = 0; i < temp.Count; i++)
            temp[i].transform.SetParent(this.transform);
    }


    void ClearList()
    {

        parts.Clear();
    }
}