using UnityEngine;
using System.Collections;

public class MainCameraControllers : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void StartButton()
    {
        Application.LoadLevel(2);
    }
    void OptionsButton()
    {

    }

    void ExitButton()
    {
        Application.Quit();
    }
}
