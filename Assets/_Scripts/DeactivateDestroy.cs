using UnityEngine;
using System.Collections;

public class DeactivateDestroy : MonoBehaviour
{
    void Deactivate()
    {
        Destroy(this.gameObject);
    }
}
