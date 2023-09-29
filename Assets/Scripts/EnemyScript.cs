using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private LogicManager logicManager;
    public int health;
    public GameObject gun;
    private List<GameObject> hardpoints = new List<GameObject>();

    private void Start()
    {
        logicManager = GameObject.FindWithTag("LogicManager").GetComponent<LogicManager>();
        for (int i = 0; i < transform.childCount; i++)
        {
            int chance = Random.Range(0, 4);
            if (chance == 0)
            {
                GameObject instance = Instantiate(gun, transform.GetChild(i), false);
                instance.GetComponent<GunScript>().cooldown = 2f * Random.Range(0.5f, 1f);
                instance.GetComponent<GunScript>().damageLayer = 6;
                instance.GetComponent<GunScript>().rotation = transform.rotation;
                hardpoints.Add(transform.GetChild(i).gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = transform.up * 50;
        Fire();

        if (transform.position.z < logicManager.lowerBound * 2f)
        {
            SelfDestruct();
        }

        if (health <= 0)
        {
            SelfDestruct();
            logicManager.addPoints(1);
        }
    }

    public void Damage(int amount)
    {
        health -= amount;
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

    public void Fire()
    {
        foreach (GameObject hardpoint in hardpoints)
        {
            hardpoint.transform.GetChild(0).gameObject.GetComponent<GunScript>().Fire();
        }
    }
}
