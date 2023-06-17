using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : MonoBehaviour
{
    public Vector3 pushDirrection;

    [SerializeField] private float pushForce;
    [SerializeField] private float destroyTime;

    private Rigidbody myRb;
    private Rigidbody[] allRigidbodies;
    private ZombieSpawner spawner;


    private void Awake()
    {
        //myRb = transform.gameObject.GetComponent<Rigidbody>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<ZombieSpawner>();
        allRigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    public void Dead()
    {
        //myRb.useGravity = true;
        //myRb.isKinematic = false;
        Push(pushDirrection);

        transform.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        transform.gameObject.GetComponent<Animator>().enabled = false;
        transform.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        
        AddKillCount();

        Invoke("DestoryBody", destroyTime);
    }

    public void StartHitReaction()
    {
        transform.gameObject.GetComponent<Animator>().SetBool("IsDamaged", true);
    }

    public void EndHitReaction()
    {
        transform.gameObject.GetComponent<Animator>().SetBool("IsDamaged", false);
    }

    private void Push(Vector3 dirrection)
    {
        foreach(Rigidbody rigidbody in allRigidbodies)
        {
            rigidbody.AddForce(-dirrection * pushForce);
        }
    }

    private void DestoryBody()
    {
        spawner.AddDeadZombie(transform.gameObject);
        spawner.SaveZombie(transform.gameObject);
        //Destroy(transform.gameObject);
    }

    private void AddKillCount()
    {
        spawner.KilledZombieCount++;
    }



}
