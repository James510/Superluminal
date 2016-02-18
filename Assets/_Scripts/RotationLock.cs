using UnityEngine;
using System.Collections;

public class RotationLock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //   transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
         transform.eulerAngles = new Vector3(-90.0f, 0.0f, 0.0f); // locking the selection sprite
                                     // may find a better solution later
    }
}
