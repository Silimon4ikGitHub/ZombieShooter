using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public int KilledZombieCount;
    
    [SerializeField] private GameObject zombie;
    [SerializeField] private float spawnTime;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float offsetTime;

    private GameObject[] points;
    private float timer;
    


    private void Start()
    {
        points = GameObject.FindGameObjectsWithTag("Points");
        SpawnAll();
    }

    private void Update()
    {
        timer++;

        if(timer >= spawnTime)
        {
            SpawnOneZombie();
            timer = 0;
        }
    }

    private void SpawnAll()
    {
        for (int i = 0; i < points.Length; i++)
        {
            int random = Random.Range(0, points.Length);
            Instantiate(zombie, points[random].transform.position, transform.rotation);
        }
    }

    private void SpawnOneZombie()
    {
        int random = Random.Range(0, points.Length);
        Instantiate(zombie, points[random].transform.position, transform.rotation);
        
        if (spawnTime > minSpawnTime)
            spawnTime *= offsetTime;    
    }
}
