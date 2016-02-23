using UnityEngine;
using System.Collections;

public class DeactivateDecouple : MonoBehaviour
{
    private bool isAlive=true;
    private float randX, randY, randZ;

    void Update()
    {
        if(!isAlive)
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
        
    void Deactivate()
    {
        isAlive = false;
        transform.rotation = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
        randX = Random.Range(-0.05f, 0.05f);
        randY = Random.Range(-0.05f, 0.05f);
        randZ = Random.Range(-0.05f, 0.05f);
        Destroy(this.gameObject, Random.Range(20.0f,30.0f));
    }
}
