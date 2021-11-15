using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    private bool isFlicker = false;
    private float timeDelay;
    public float lowerRange;
    public float higherRange;
    [HideInInspector]
    public bool flickerEnabled = false;

    // Update is called once per frame
    void Update()
    {
        if (flickerEnabled) {
            if (!isFlicker)
            {

                StartCoroutine(LightFlicker());
            }
        }
    }

    IEnumerator LightFlicker()
    {
        isFlicker = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(lowerRange, higherRange);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(lowerRange, higherRange);
        yield return new WaitForSeconds(timeDelay);
        isFlicker = false;

    }

    void FlickerPower()
    {
        this.gameObject.GetComponent<Light>().enabled = false;
        flickerEnabled = !flickerEnabled;
    }
}
