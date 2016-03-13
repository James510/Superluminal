using UnityEngine;
using System.Collections;
//OBSELETE
public class BulletScript : MonoBehaviour
{
    public bool isEnemy = false;
    public GameObject hit;
    public int damage;


    void Start ()
    {
        Destroy(this.gameObject, 2.0f);
	}
    void Update()
    {
        transform.Translate(Vector3.forward * 3);

    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy") && !isEnemy)
        {
            other.SendMessage("Damage", damage);
            Instantiate(hit,transform.position,transform.rotation);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Friend") && isEnemy)
        {
            other.SendMessage("Damage", damage);
            Instantiate(hit, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }

    }
}
