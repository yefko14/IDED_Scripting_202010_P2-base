using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControllerPool : MonoBehaviour
{
    [SerializeField]
    private float spawnRate = 1f;

    [SerializeField]
    private float firstSpawnDelay = 0f;

    [SerializeField]
    private PlayerRefactor player;

    [SerializeField]
    PoolTarget pool;

    void Start()
    {
        if (pool != null)
        {
            InvokeRepeating("spawn", firstSpawnDelay, spawnRate);
        }
    }

    void Update()
    {
        if (player != null)
        {
            player.onPlayerDied += StopSpawning;
        }
    }
    void spawn()
    {
        pool.GetItem();
    }

    private void StopSpawning()
    {
        CancelInvoke();
    }
}
