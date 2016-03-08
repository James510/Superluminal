﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadPrefabButton : MonoBehaviour
{
    public int  prefabNum=0;
    private GameObject shipBuilderCore;
    private Button loadButton;

    // Use this for initialization
    void Start()
    {
        shipBuilderCore = GameObject.Find("ShipBuilderCore");
        Text shipText = transform.GetChild(0).GetComponent<Text>();
        shipText.text = this.name;
    }

    // Update is called once per frame
    void Update()
    {

    }

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
