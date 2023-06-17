using Opsive.UltimateCharacterController.Demo.UnityStandardAssets.Vehicles.Car;
using Opsive.UltimateCharacterController.Traits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    public int KilledZombieCount;
    
    [SerializeField] private GameObject zombie;
    [SerializeField] private GameObject[] deadZombies;
    [SerializeField] private float spawnTime;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float offsetTime;
    [SerializeField] private bool isNoSavedZombie;

    private GameObject[] points;
    [SerializeField]  private float timer;
    


    private void Start()
    {
        deadZombies = new GameObject[10];
        points = GameObject.FindGameObjectsWithTag("Points");
        SpawnAll();
        CheckArray(ref isNoSavedZombie, deadZombies);
    }

    private void FixedUpdate()
    {
        timer++;

        if(timer >= spawnTime)
        {
            CheckArray(ref isNoSavedZombie, deadZombies);

            if (isNoSavedZombie)
            {
                SpawnOneZombie();
                timer = 0;
            }
            else if (!isNoSavedZombie)
            {
                SpawnSavedZombie();
                timer = 0;
            }

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
        var currentZombie = Instantiate(zombie, points[random].transform.position, transform.rotation);
        TimeerDecrease();
    }

    public void AddDeadZombie(GameObject zombie)
    {
        for(int i = 0; i < deadZombies.Length;i++) 
        {
            if (deadZombies[i] == null)
            {
                deadZombies[i] = zombie;
                break;
            }
        }
    }

    public void SaveZombie(GameObject zombie)
    {
        zombie.SetActive(false);
    }

    private bool CheckArray(ref bool isArrayEmpty, GameObject[] array)
    {
        isArrayEmpty = false;
        int counter = 0;
        int arrayLength = array.Length;

        for(int i = 0; i < array.Length; i++)
        {
            if (array[i] == null)
                counter++;
        }

        if (counter == arrayLength)
        {
            isArrayEmpty = true;
            Debug.Log(isArrayEmpty);
        }
        return isArrayEmpty;
    }

    private void SpawnSavedZombie()
    {
        int random = Random.Range(0, points.Length);

        for (int i = 0; i < deadZombies.Length; i++)
        {
            if (deadZombies[i] != null)
            {
                deadZombies[i].SetActive(true);
                RefreshZombieComponents(deadZombies[i], deadZombies[i].GetComponent<Rigidbody>());
                deadZombies[i].transform.position = points[random].transform.position;
                deadZombies[i] = null;
                break;
            }
        }
        TimeerDecrease();
    }

    private void RefreshZombieComponents(GameObject zombie, Rigidbody zombieRb)
    {
        //zombieRb.useGravity = false;
        //zombieRb.isKinematic = true;
        zombie.GetComponent<Health>().Heal(100);
        zombie.GetComponent<CapsuleCollider>().enabled = true;
        zombie.GetComponent<NavMeshAgent>().enabled = true;
        zombie.GetComponent<Animator>().enabled = true;
        
    }

    private void TimeerDecrease()
    {
        if (spawnTime > minSpawnTime)
            spawnTime *= offsetTime;
    }
}
