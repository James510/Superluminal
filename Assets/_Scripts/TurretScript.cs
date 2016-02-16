using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour
{
    public float rotSpeed = 360.0f;
    public GameObject bullet;
    public float fireRate;
    public float inaccuracy;
    private float nextFire;

    void Start()
    {
        nextFire = Time.time;
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Hit");
        if(other.gameObject.CompareTag("Enemy"))//Detect, track and fire at enemy
        {
            Vector3 targetPos = other.gameObject.transform.position - transform.position;
            Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos), rotSpeed * Time.deltaTime);
            transform.rotation = rot;
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z); //Lock turret to an axis, doesn't work yet.
            if (Time.time > nextFire)
            {
                GameObject shot = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y + Random.Range(-inaccuracy, inaccuracy), transform.rotation.eulerAngles.z + Random.Range(-inaccuracy, inaccuracy))) as GameObject;

                Rigidbody srb = shot.GetComponent<Rigidbody>();
                srb.velocity = transform.parent.GetComponent<Rigidbody>().velocity;
                srb.AddRelativeForce(Vector3.forward * 6000);

                nextFire = Time.time + fireRate; //Determines fire rate
            }
        }
        
    }
}