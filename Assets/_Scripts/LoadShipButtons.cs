using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadShipButtons : MonoBehaviour
{
    private GameObject shipBuilderCore;
    private Button loadButton;

    // Use this for initialization
    void Start ()
    {
        shipBuilderCore = GameObject.Find("ShipBuilderCore");
        Text shipText = transform.GetChild(0).GetComponent<Text>();
        shipText.text = this.name;
	}
	
    void Awake()
    {
        loadButton = GetComponent<Button>();
        SetAction();
    }

    public void SetAction()
    {
        loadButton.onClick.RemoveAllListeners();
        loadButton.onClick.AddListener(() => { shipBuilderCore.SendMessage("LoadShip", this.name); });
    }

}
