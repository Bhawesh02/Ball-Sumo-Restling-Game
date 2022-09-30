using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    public PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            player.onGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.onGround = false;
        }
    }
}
