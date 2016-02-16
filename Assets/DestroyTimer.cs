using UnityEngine;
using System.Collections;

public class DestroyTimer : MonoBehaviour
{
    public float destroyTimer = 1.0f;
	// Use this for initialization
	void Start ()
    {
        Destroy(this.gameObject, destroyTimer);
	}
}
