using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private GameObject spawnManger;
    private SpawnManager spawn;
    public float powerupTime = 5.0f;
    private float powerUpStrenght = 15.0f;
    public float speed = 5;
    public bool hasPowerUp = false;
    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        spawnManger = GameObject.Find("Spwan Manager");
        spawn = spawnManger.gameObject.GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        powerupIndicator.transform.position = transform.position + new Vector3(0,-0.5f,0);


    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            powerupIndicator.SetActive(true);
            spawn.powerUPExsit = false;
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    { 
        yield return new WaitForSeconds(powerupTime);
        powerupIndicator.SetActive(false);

        hasPowerUp = false; 
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayDirec = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayDirec * powerUpStrenght, ForceMode.Impulse);

            Debug.Log("Collided with "+ collision.gameObject.name + " with hasPowerUp "+ hasPowerUp);
        }
    }
}
