using UnityEngine;
using System.Collections;

public class ExplosionFXScript : MonoBehaviour
{
    public float destroyDelay;
	// Use this for initialization
	void Start ()
    {
        Destroy(this.gameObject, destroyDelay);
        GetComponent<ParticleSystem>().Emit(30);
	}
}
