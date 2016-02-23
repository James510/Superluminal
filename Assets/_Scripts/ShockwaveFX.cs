using UnityEngine;
using System.Collections;

public class ShockwaveFX : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        transform.rotation = Quaternion.Euler(Random.Range(-90.0f, 90.0f), Random.Range(-90.0f, 90.0f), 0.0f);
        GetComponent<ParticleSystem>().Emit(360);
        StartCoroutine("DeathTimer");
	}
	
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);   
    }
}
