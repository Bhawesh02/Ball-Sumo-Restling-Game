using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBound : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Projectile"))
            Destroy(other.transform.parent.gameObject);
        else
            Destroy(other);
    }
}
