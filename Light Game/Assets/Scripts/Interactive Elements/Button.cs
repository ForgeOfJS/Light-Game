using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject referenceObject;
    public GameObject destination;
    //public float objectSpeed;
    private Vector3 prevPosition;
    
    void Start()
    {
        prevPosition = referenceObject.transform.position;
    }

    public void OnButtonPress()
    {
        //priority 1 light
        if (referenceObject.GetComponent<Light>())
        {
            //turns on flicker
            if (referenceObject.GetComponent<Flicker>())
            {
                referenceObject.GetComponent<Flicker>().flickerEnabled = !referenceObject.GetComponent<Flicker>().flickerEnabled;
            }
            //if not flicker turn on light
            else
            {
                referenceObject.GetComponent<Light>().enabled = !referenceObject.GetComponent<Light>().enabled;
            }
        }
        //otherwise if it has a collider
        else if (referenceObject.GetComponent<Collider>())
        {
            //swich position between designated and original
            if (referenceObject.transform.position != prevPosition)
            {
                referenceObject.transform.position = prevPosition;
            }
            else
            {
                referenceObject.transform.position = destination.transform.position;
            }
            
        }
    }
}
