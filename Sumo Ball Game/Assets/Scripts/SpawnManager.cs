using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject[] powerUpPrefab;
    public GameObject[] miniBossPrefab;
    public int enemyCount;
    public List<GameObject> enemys = new List<GameObject>();
    public int waveNumber = 1;
    public int miniBossEveryWave = 3;
    
    
    private float powerUpSpawnTime = 5.0f;
    private float spawnRange = 9.0f;
    public bool powerUPExsit = false;
    
    

    // Start is called before the first frame update
    void Start()
    {
        SpawnenemyWave(waveNumber);
        Invoke("SpawnPowerUP",Random.Range(0,powerUpSpawnTime));
    }

    // Update is called once per frame
    void Update()
    {
    
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            if (waveNumber % miniBossEveryWave != 0)
                SpawnenemyWave(waveNumber);
            else
                SpawnMiniBoss();
        }
    }

    void SpawnMiniBoss()
    {
        enemys.Clear();
        int miniBossNo = Random.Range(0, miniBossPrefab.Length);
        GameObject newEnemy = Instantiate(miniBossPrefab[miniBossNo], GenerateSpawnPos(), miniBossPrefab[miniBossNo].transform.rotation);
        enemys.Add(newEnemy);
    }
    private void SpawnenemyWave(int enemyNum = 3)
    {
        enemys.Clear();
        for (int i = 0; i < enemyNum; i++)
        {
            int enemyNo = Random.Range(0, enemyPrefab.Length);
            GameObject newEnemy = Instantiate(enemyPrefab[enemyNo], GenerateSpawnPos(), enemyPrefab[enemyNo].transform.rotation);
            enemys.Add(newEnemy);
        }
    }
    private  void SpawnPowerUP()
    {
        if (!powerUPExsit)
        {
            int powerUpNo = Random.Range(0,powerUpPrefab.Length);
            Instantiate(powerUpPrefab[powerUpNo], GenerateSpawnPos(), powerUpPrefab[powerUpNo].transform.rotation);
            powerUPExsit = true;
            
        }
        Invoke("SpawnPowerUP", Random.Range(0, powerUpSpawnTime));

    }


    public Vector3 GenerateSpawnPos()
    {
        float spawnRangeX = Random.Range(-spawnRange, spawnRange);
        float spawnRangeZ = Random.Range(-spawnRange, spawnRange);

        Vector3 enemyPos = new Vector3(spawnRangeX, 0, spawnRangeZ);
        return enemyPos;
    }
}
