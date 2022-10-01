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
    private float powerUpStrenght = 15.0f;
    private float projectileSpeed = 3.5f;
    private bool canLauchProjectile = false;
    private bool canJump = false;
    public bool jumped = false;
    

    public float powerupTime = 5.0f;
    public List<GameObject> projectileLaunchers = new List<GameObject>();
    public float speed = 5;
    public bool hasPowerUp = false;
    public GameObject powerupIndicator;
    public GameObject projectileLauncher;
    public float jumpForce = 15.0f;
    public float smashForce = 25.0f;
    public bool onGround;



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
         


        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (canLauchProjectile)
            {
                LaunchProjectile();
            }
            else if (canJump)
          {
                if (onGround)
                {
                    StartCoroutine(Smash());
                }
                
          }

        }

    }


    private void LaunchProjectile()
    {

        for (int i = 0; i < spawn.enemyCount; i++)
        {
            if (spawn.enemys[i] != null)
            {
                Vector3 enemyPos = new Vector3(spawn.enemys[i].transform.position.x, spawn.enemys[i].transform.position.y, spawn.enemys[i].transform.position.z);
                float angle = AngleFromToPoint(transform.position, enemyPos, Vector3.forward);
                angle *= -1;
                GameObject newProjectile = Instantiate(projectileLauncher,transform.position,Quaternion.Euler(0, angle, 0));
                GameObject projectile = newProjectile.transform.GetChild(0).gameObject;
                Rigidbody proRb = projectile.GetComponent<Rigidbody>();
                Vector3 proTowards = enemyPos - projectile.transform.position ;
                proRb.AddForce(proTowards * projectileSpeed  ,ForceMode.Impulse);



            }
        }



    }

    IEnumerator Smash()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        onGround = false;
        yield return new WaitUntil(() => onGround);
        Debug.Log("Landed");
        for (int i = 0; i < spawn.enemyCount; i++)
        {
            if (spawn.enemys[i] != null)
            {
                Vector3 enemyPos = new Vector3(spawn.enemys[i].transform.position.x, spawn.enemys[i].transform.position.y, spawn.enemys[i].transform.position.z);
                float disbBtw = Vector3.Distance(enemyPos,transform.position);
                Vector3 emeyDirec = (enemyPos - transform.position).normalized;
                Rigidbody enemyRb = spawn.enemys[i].GetComponent<Rigidbody>();
                enemyRb.AddForce(emeyDirec * (smashForce - disbBtw),ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUp") || other.CompareTag("PowerUp1") || other.CompareTag("PowerUp2"))
        {
            Destroy(other.gameObject);
            powerupIndicator.SetActive(true);
            spawn.powerUPExsit = false;
            if(other.CompareTag("PowerUp"))
            {
                hasPowerUp = true;
            }
            else if (other.CompareTag("PowerUp1"))
            {
                canLauchProjectile = true;
            }
            else
            {
                canJump = true;
            }
            StartCoroutine(PowerupCountdownRoutine());

        }
    }

    IEnumerator PowerupCountdownRoutine()
    { 
        yield return new WaitForSeconds(powerupTime);
        powerupIndicator.SetActive(false);
        canLauchProjectile = false;
        hasPowerUp = false;
        canJump = false;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayDirec = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayDirec * powerUpStrenght, ForceMode.Impulse);
        }
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject.transform.parent.gameObject);
        }

    }
    

    public float AngleFromToPoint(Vector3 fromPoint,
                                     Vector3 toPoint,
                                     Vector3 zeroDirection)
    {
        Vector3 targetDirection = toPoint - fromPoint;
        float sign = Mathf.Sign(zeroDirection.x * targetDirection.z - zeroDirection.z * targetDirection.x);
        float angle = Vector3.Angle(zeroDirection, targetDirection) * sign;
        return angle < 0 ? angle + 360 : angle;
    }


    
}
