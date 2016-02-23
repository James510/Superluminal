using UnityEngine;
using System.Collections;

public class DeathExplosionFX : MonoBehaviour
{
    public GameObject deathTrail;
    public GameObject shockwave;

    // Use this for initialization
    void Start ()
    {
        GetComponent<ParticleSystem>().Emit(300);
        for (int x = 0; x < 5; x++)
            Instantiate(deathTrail, transform.position, transform.rotation);
        Instantiate(shockwave, transform.position, transform.rotation);
    }
}
