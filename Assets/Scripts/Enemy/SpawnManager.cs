using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoints;

    public static GameObject colliderObj;

    public static bool isSpawned;

    public EnemySpawner[] spawner;

    private void Start()
    {
        isSpawned = false;
    }

    private void Update()
    {
        if (isSpawned)
        {
            for(int i = 0; i< spawner.Length; i++)
            {
                if (colliderObj.name == spawner[i].triggerArea.name)
                {
                    Instantiate(spawner[i].enemyToSpawn, spawnPoints.transform.position, Quaternion.identity);
                    colliderObj.SetActive(false);
                    colliderObj = null;
                }
            }

            isSpawned = false;
        }
    }

    
}

[System.Serializable]
public class EnemySpawner
{
    public GameObject triggerArea;
    public GameObject enemyToSpawn;
}
