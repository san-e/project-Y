using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipScript : MonoBehaviour
{
    public float rotationStrength;
    public float rotationMax = 30;
    public float rotationMin = -30f;
    private Rigidbody rb;
    private GameObject ship;
    private LogicManager logicManager;
    private InputManager inputManager;
    private float timer;
    public Camera mainCamera;
    private float targetRotation;
    private List<GameObject> hardpoints = new List<GameObject>();
    public GameObject gun;
    public int health = 100;
    public int maxHealth = 100;
    public Image healthbar;

    // Start is called before the first frame update
    void Start()
    {
        logicManager = GameObject.FindWithTag("LogicManager").GetComponent<LogicManager>();
        rb = GetComponentInChildren<Rigidbody>();
        ship = transform.GetChild(0).gameObject;
        inputManager = GameObject.FindWithTag("InputManager").GetComponent<InputManager>();

        for (int i = 0; i < ship.transform.GetChild(0).childCount; i++)
        {
            hardpoints.Add(ship.transform.GetChild(0).GetChild(i).gameObject);
        }

        foreach (GameObject hardpoint in hardpoints)
        {
            GameObject gunInstance = Instantiate(gun, hardpoint.transform, false);
            gunInstance.GetComponent<GunScript>().damageLayer = 3;
            gunInstance.GetComponent<GunScript>().rotation = transform.rotation;
        }       
    }

    // Update is called once per frame
    void Update()
    {
        float ratio = (float)health / (float)maxHealth;
        healthbar.GetComponent<RectTransform>().sizeDelta = new Vector2(4.25f, 211.1f * ((float)health/(float)maxHealth));

        if (ratio > 0.5f)
        {
            healthbar.GetComponent<Image>().color = Color.green;
        } else if (ratio <= 0.5f && ratio > 0.25f)
        {
            healthbar.GetComponent<Image>().color = Color.yellow;
        } else
        {
            healthbar.GetComponent<Image>().color = Color.red;
        }

        if (health <= 0)
        {
            SelfDestruct();
        }

        if (rb.position.x <= logicManager.leftBound || rb.position.x >= logicManager.rightBound)
        {
            rb.velocity = -rb.velocity;
            timer = 0.35f;
            inputManager.enableMovement = false;
            if (rb.position.x > 0)
            {
                rb.position = new Vector3(logicManager.rightBound - 1f, rb.position.y, rb.position.z);
            } else if (rb.position.x < 0)
            {
                rb.position = new Vector3(logicManager.leftBound + 1f, rb.position.y, rb.position.z);
            }
        }

        if (rb.velocity.x < 0)
        {
            targetRotation = rotationMin * rb.velocity.x/100;

        }
        else if (rb.velocity.x > 0)
        {
            targetRotation = rotationMax * -rb.velocity.x/100;
        }
        else
        {
            targetRotation = 0;
        }

        Quaternion targetQuaternion = Quaternion.Euler(0f, 0f, targetRotation);
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetQuaternion,
            rotationStrength * Time.deltaTime
        );

        transform.position = new Vector3(transform.position.x, 0f, -40f);
        ship.transform.position = new Vector3(ship.transform.position.x, 0f, -40f);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            inputManager.enableMovement = true;
        }
    }

    public void FireAll()
    {
        foreach (GameObject hardpoint in hardpoints)
        {
            hardpoint.transform.GetChild(0).gameObject.GetComponent<GunScript>().Fire();
        }
    }

    public void FireGun(int index)
    {
        hardpoints[index].transform.GetChild(0).gameObject.GetComponent<GunScript>().Fire();
    }

    public void Damage(int amount)
    {
        health -= amount;
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
