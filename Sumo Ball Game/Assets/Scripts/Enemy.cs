using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.5f;
    private Rigidbody enemyRb;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 towardsDsit = (player.transform.position - transform.position).normalized;
        towardsDsit.y = 0;
        enemyRb.AddForce(towardsDsit * speed);

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
                }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject.transform.parent.gameObject);
        }
    }
}
