using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public GameObject ship;
    private Rigidbody shipRigidbody;
    public bool enableMovement;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        shipRigidbody = ship.GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (enableMovement)
            {
                shipRigidbody.velocity -= new Vector3(1f, 0f, 0f);

                if (shipRigidbody.velocity.x < -1)
                {
                    shipRigidbody.velocity = Vector3.left*100;
                }
            }

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (enableMovement)
            {
                shipRigidbody.velocity += new Vector3(1f, 0f, 0f);

                if (shipRigidbody.velocity.x > 1)
                {
                    shipRigidbody.velocity = Vector3.right*100;
                }
            }
        }
    
        if (true)//(Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100f));
            shipRigidbody.position = new Vector3(mousePos.x, shipRigidbody.position.y, shipRigidbody.position.z);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            ship.GetComponent<ShipScript>().FireAll();
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            ship.GetComponent<ShipScript>().FireGun(0);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            ship.GetComponent<ShipScript>().FireGun(1);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            ship.GetComponent<ShipScript>().FireGun(2);
        }
    }
}
