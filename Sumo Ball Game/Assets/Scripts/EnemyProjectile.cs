using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private GameObject player;
    private PlayerController play;
    public float projectileSpeed = 3.5f;

    private void Awake()
    {
        player = GameObject.Find("Player");
        play = player.GetComponent<PlayerController>();
        Invoke("Lauch",2);
    }
    void Lauch()
    {
        Vector3 playerPos = player.transform.position;
        float angle = play.AngleFromToPoint(transform.position, playerPos, Vector3.forward);
        angle *= -1;
        GameObject newProjectile = Instantiate(play.projectileLauncher, transform.position, Quaternion.Euler(0, angle, 0));
        GameObject projectile = newProjectile.transform.GetChild(0).gameObject;
        Rigidbody proRb = projectile.GetComponent<Rigidbody>();
        Vector3 proTowards = playerPos - projectile.transform.position;
        proRb.AddForce(proTowards * projectileSpeed, ForceMode.Impulse);
        Invoke("Lauch",Random.Range(3,5));
    }
    private void OnDestroy()
    {
        CancelInvoke("Lauch");
    }
}
