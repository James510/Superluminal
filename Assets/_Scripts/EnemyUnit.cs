using UnityEngine;
using System.Collections;

public class EnemyUnit : MonoBehaviour
{
    public int hp;


	void Start ()
    {
	    
	}
	

	void Update ()
    {
	    if(hp<1)
        {
            Destroy(this.gameObject);
        }
	}

    public void Damage(int dmg)
    {
        hp -= dmg;
    }
}
