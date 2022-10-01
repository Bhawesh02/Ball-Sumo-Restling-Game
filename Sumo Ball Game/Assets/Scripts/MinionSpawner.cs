using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    private GameObject spawnManger;
    private SpawnManager spawn;

    private void Awake()
    {
        spawnManger = GameObject.Find("Spwan Manager");
        spawn = spawnManger.gameObject.GetComponent<SpawnManager>();
        Invoke("SpawnMinion", 2);
    }
    void SpawnMinion()
    {
        Debug.Log("monion");
        int enemyIndex = Random.Range(0,spawn.enemyPrefab.Length);
        int numOfMinion = Random.Range(0,3);
        for(int i = 0;i<numOfMinion;i++)
        {
            GameObject newEnemy = Instantiate(spawn.enemyPrefab[enemyIndex], spawn.GenerateSpawnPos(), spawn.enemyPrefab[enemyIndex].transform.rotation);
            spawn.enemys.Add(newEnemy);
        }
        Invoke("SpawnMinion",Random.Range(2,4));
    }

    private void OnDestroy()
    {
        CancelInvoke("SpawnMinion");
    }


}
