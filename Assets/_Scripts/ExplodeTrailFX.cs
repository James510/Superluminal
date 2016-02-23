using UnityEngine;
using System.Collections;

public class ExplodeTrailFX : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-500.0f, 500.0f), Random.Range(-500.0f, 500.0f), Random.Range(-500.0f, 500.0f)));
        StartCoroutine("DeathTimer");
	}
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(20.0f);
        GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(15.0f);
        Destroy(this.gameObject);
    }
}
