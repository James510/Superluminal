using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadPrefabButton : MonoBehaviour
{
    public int  prefabNum=0;
    private GameObject shipBuilderCore;
    private Button loadButton;

<<<<<<< HEAD
<<<<<<< HEAD
=======
    // Use this for initialization
>>>>>>> origin/master
=======
    // Use this for initialization
>>>>>>> refs/remotes/origin/master
    void Start()
    {
        shipBuilderCore = GameObject.Find("ShipBuilderCore");
        Text shipText = transform.GetChild(0).GetComponent<Text>();
        shipText.text = this.name;
    }

<<<<<<< HEAD
<<<<<<< HEAD
=======
=======
>>>>>>> refs/remotes/origin/master
    // Update is called once per frame
    void Update()
    {

    }

<<<<<<< HEAD
>>>>>>> origin/master
=======
>>>>>>> refs/remotes/origin/master
    void Awake()
    {
        loadButton = GetComponent<Button>();
        SetAction();
    }

    public void SetAction()
    {
        loadButton.onClick.RemoveAllListeners();
        loadButton.onClick.AddListener(() => { shipBuilderCore.SendMessage("LoadPrefab", prefabNum); });
    }
}
