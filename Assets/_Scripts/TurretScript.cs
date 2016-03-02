using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour
{
    public float angle = 10;
    public float rotSpeed = 360.0f;
    public GameObject bullet;
    public float fireRate;
    public float inaccuracy;
    public int damage;
    public bool isEnemy = false;
    public float bulletScale=1.0f;
    private float nextFire;
    private bool hasTarget;
    private GameObject target;
    private Light flash;
    private bool isAlive = true;
    private float randX, randY, randZ;

    void Start()
    {
        //Debug.Log(LayerMask.GetMask("Allies"));
        nextFire = Time.time;
        flash = GetComponent<Light>();
        GetComponent<ParticleSystem>().startSize = bulletScale;
        if(isEnemy)
        {
            ParticleSystem temp = GetComponent<ParticleSystem>();
            ParticleSystem.CollisionModule temp2 = temp.collision;
            temp2.collidesWith = 256;
        }
        //GetComponent<ParticleSystem>().shape = inaccuracy;
    }

    void Update()
    {
        //Debug.Log(Vector3.Angle(transform.forward, target.transform.position - transform.position));
        if(isAlive)
        {
            if (hasTarget && target != null)
            {
                Vector3 targetPos = target.transform.position - transform.position;
                Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos), rotSpeed * Time.deltaTime);
                transform.rotation = rot;
                //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z); //Lock turret to an axis, doesn't work yet.
                if (Time.time > nextFire && (Vector3.Angle(transform.forward, target.transform.position - transform.position) < angle))
                {
                    //GameObject shot = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(transform.rotation.eulerAngles.x + Random.Range(-inaccuracy, inaccuracy), transform.rotation.eulerAngles.y + Random.Range(-inaccuracy, inaccuracy), transform.rotation.eulerAngles.z + Random.Range(-inaccuracy, inaccuracy))) as GameObject;
                    GetComponent<ParticleSystem>().Emit(1);
                    //flash.enabled = true;
                    nextFire = Time.time + fireRate; //Determines fire rate
                }
                else
                {
                    //flash.enabled = false;
                }
            }
            else
            {
                hasTarget = false;
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x + randX, transform.position.y + randY, transform.position.z + randZ);
            if (randX > 0)
                randX -= 0.0001f;
            else if (randX < 0)
                randX += 0.0001f;
            if (randY > 0)
                randY -= 0.0001f;
            else if (randY < 0)
                randY += 0.0001f;
            if (randZ > 0)
                randZ -= 0.0001f;
            else if (randZ < 0)
                randZ += 0.0001f;
        }
    }

    void SetTarget(GameObject tgt)
    {
        target = tgt;
        hasTarget = true;
    }

    void Deactivate()
    {
        isAlive = false;
        transform.rotation = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
        randX = Random.Range(-0.05f, 0.05f);
        randY = Random.Range(-0.05f, 0.05f);
        randZ = Random.Range(-0.05f, 0.05f);
        Destroy(this.gameObject, 30.0f);
    }
    /*
    void OnTriggerStay(Collider other) //The old method of tracking
    {
        //Debug.Log("Hit");
        
        if ((other.gameObject.CompareTag("Enemy") && !isEnemy) || (other.gameObject.CompareTag("Friend") && isEnemy))//Detect, track and fire at enemy
        {
            //Debug.Log("Detected");
            if (!hasTarget)//There is something in here that causes tremendous lag whenever a ship aquires a target.
            {
                hasTarget = true;
                target = other.gameObject;
            }
            if (hasTarget && target == null)
            {
                hasTarget = false;
            }
        }
        if (hasTarget)
        {
            Vector3 targetPos = target.transform.position - transform.position;
            Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos), rotSpeed * Time.deltaTime);
            transform.rotation = rot;
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z); //Lock turret to an axis, doesn't work yet.
            if (Time.time > nextFire)
            {
                GameObject shot = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(transform.rotation.eulerAngles.x + Random.Range(-inaccuracy, inaccuracy), transform.rotation.eulerAngles.y + Random.Range(-inaccuracy, inaccuracy), transform.rotation.eulerAngles.z + Random.Range(-inaccuracy, inaccuracy))) as GameObject;

                Rigidbody srb = shot.GetComponent<Rigidbody>();
                srb.velocity = transform.parent.GetComponent<Rigidbody>().velocity;
                srb.AddRelativeForce(Vector3.forward * 6000);
                shot.GetComponent<BulletScript>().damage = damage;
                if (other.gameObject.CompareTag("Friend") && isEnemy)
                    shot.GetComponent<BulletScript>().isEnemy = true;
                else
                    shot.GetComponent<BulletScript>().isEnemy = false;
                shot.transform.localScale += new Vector3(bulletScale, bulletScale, bulletScale * 10);
                nextFire = Time.time + fireRate; //Determines fire rate
            }
        }
    }
    */
}