using Opsive.UltimateCharacterController.Character.Abilities.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlternativePlayerShoot : MonoBehaviour
{
    
    [SerializeField] private GameObject camera;
    [SerializeField] private Ray ray;
    [SerializeField] private RaycastHit hit;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject blood;
    [SerializeField] private ZombieHealth zombie;
    [SerializeField] private float damage;
    [SerializeField] private float timer;
    [SerializeField] private float shootingRate;
    [SerializeField] private bool isShooting = false;

    private void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    void Update()
    {
        ShowDrawRay(camera);
        Physics.Raycast(ray, out hit);

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
        }
    }

    public void ShowDrawRay(GameObject obj)
    {
        ray = new Ray(obj.transform.position, obj.transform.forward);
        Debug.DrawRay(obj.transform.position, obj.transform.forward * 10000, Color.yellow);
    }

    public void Shoot()
    {
        GameObject bloods;
        if(target.GetComponent<ZombieHealth>())
        {
            zombie = target.GetComponent<ZombieHealth>();
            zombie.pushDirrection = -ray.direction;
            bloods = Instantiate(blood, hit.point, transform.rotation);
            Destroy(bloods, 0.5f);
        }
        else
        {
            zombie = null;
        }
    }

    private void FullAutoShooting()
    {
        if (isShooting)
        {
            timer++;

            if (timer >= shootingRate)
            {
                Shoot();
                timer = 0;
            }
        }
    }
}
