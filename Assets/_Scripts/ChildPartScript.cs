using UnityEngine;
using System.Collections;

public class ChildPartScript : MonoBehaviour
{
    public int prefabNum;
    public float speedMod;
    public int healthMod;
    void Start()
    {
        GetComponentInParent<Unit>().hp += healthMod;
        GetComponentInParent<Unit>().speed += speedMod;
    }
    void OnDestroy()
    {
        if(PlayerPrefs.GetInt("EditorMode")==1)
        {
            GetComponentInParent<Unit>().hp -= healthMod;
            GetComponentInParent<Unit>().speed -= speedMod;
        }
    }
}
