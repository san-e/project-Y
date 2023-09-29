using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private LogicManager logicManager;
    public int damageLayer;
    public int damage = 5;

    private void Start()
    {
        logicManager = GameObject.FindWithTag("LogicManager").GetComponent<LogicManager>();
        
    }

    private void Update()
    {
        if (damageLayer == 3)
        {
            GetComponent<Rigidbody>().velocity = transform.forward * 500;
        }
        else if (damageLayer == 6)
        {
            GetComponent<Rigidbody>().velocity = transform.up * 500;
        }

        if (transform.position.z > logicManager.upperBound * 2f || transform.position.z < logicManager.lowerBound * 2f)
        {
            SelfDestruct();
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == damageLayer && damageLayer == 3) 
        {
            collision.gameObject.GetComponent<EnemyScript>().Damage(damage);
            SelfDestruct();
        }
        else if (collision.gameObject.layer == damageLayer && damageLayer == 6)
        {
            collision.gameObject.transform.GetComponentInParent<ShipScript>().Damage(damage);
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
