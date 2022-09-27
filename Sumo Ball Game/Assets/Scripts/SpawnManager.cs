using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9.0f;
    public int enemyCount ;
    public int waveNumber = 1;

    

    // Start is called before the first frame update
    void Start()
    {
        SpawnenemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnenemyWave(waveNumber);
        }
    }

    private void SpawnenemyWave(int enemyNum = 3)
    {
        for (int i = 0; i < enemyNum; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
        }
    }


    private Vector3 GenerateSpawnPos()
    {
        float spawnRangeX = Random.Range(-spawnRange, spawnRange);
        float spawnRangeZ = Random.Range(-spawnRange, spawnRange);

        Vector3 enemyPos = new Vector3(spawnRangeX, 0, spawnRangeZ);
        return enemyPos;
    }
}
