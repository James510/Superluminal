using UnityEngine;
using System.Collections;

public class PlanetScript : MonoBehaviour
{
    public float rotSpeed=0;

	void Update ()
    {

        transform.rotation = Quaternion.Euler(0.0f, transform.eulerAngles.y + rotSpeed, 0.0f);
    }
}
