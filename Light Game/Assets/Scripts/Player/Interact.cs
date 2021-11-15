using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public float interactDistance = 5f;
    public GameObject point;
    public GameObject currObject = null;
    public float distance;
    public float carrySpeed = 5f;
    public float throwForce;
    private float charge;
    public float maxCharge = 60f;

    void Start()
    {
        charge = maxCharge;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && charge > 0f)
        {
            //turn on/off flashlight
            this.gameObject.GetComponent<Light>().enabled = !this.gameObject.GetComponent<Light>().enabled;

        }
        //turn off flashlight if charge is below 0
        if (charge < 0f)
        {
            this.gameObject.GetComponent<Light>().enabled = false;
        }
        //lower charge if light is enabled
        if (this.gameObject.GetComponent<Light>().enabled)
        {
            charge -= 1f * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //recharge battery
            charge = maxCharge;
        }
        if (currObject != null)
        {
            //check if we have an object and if we do then check if we want to drop or throw
            Carry();
            Check();
        }
        else
        {
            //otherwise check if we want to pick up an obj
            InteractWith();
        }
    }

    void Carry()
    {
        currObject.transform.position = Vector3.Lerp(currObject.transform.position, point.transform.position, Time.deltaTime * carrySpeed);
    }

    void InteractWith()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, interactDistance))
            {
                Rigidbody p = hit.transform.GetComponent<Rigidbody>();
                if (p != null)
                {
                    p.isKinematic = true;
                    currObject = p.gameObject;
                }
                //activate button on E press.
                else if (hit.transform.GetComponent<Button>())
                {
                    hit.transform.GetComponent<Button>().OnButtonPress();
                }
            }
        }
    }

    void Check()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DropObject();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            
            ThrowObject();
        }
    }

    void DropObject()
    {
        currObject.transform.GetComponent<Rigidbody>().isKinematic = false;
        currObject = null;
    }
    void ThrowObject()
    {
        currObject.transform.GetComponent<Rigidbody>().isKinematic = false;
        GameObject thrownObject = currObject;
        //add a force directly away from player
        currObject = null;
        thrownObject.transform.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce);
        
    }
}
