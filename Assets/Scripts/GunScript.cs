using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public GameObject projectile;
    public float cooldown;
    private float coolerdown;
    public int damageLayer = 3;
    public Quaternion rotation;

    private void Start()
    {
        coolerdown = cooldown;
    }

    private void Update()
    {
        coolerdown -= Time.deltaTime;
    }

    public void Fire()
    {
        if (coolerdown <= 0)
        {
            Vector3 gunInWorldSpace = transform.TransformPoint(new Vector3(0f, 0f, 0f));
            GameObject bullet = Instantiate(projectile, gunInWorldSpace, rotation);
            bullet.GetComponent<BulletScript>().damageLayer = damageLayer;
            bullet.GetComponent<BulletScript>().damage = 10;
            coolerdown = cooldown;
        }
    }
}
