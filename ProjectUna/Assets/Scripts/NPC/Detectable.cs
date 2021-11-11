using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detectable : MonoBehaviour
{
    //Attached to an object that Detectors are looking for

    [SerializeField] private bool canBeDetected = true;

    [SerializeField] private bool instantDetection = false;
    [SerializeField] private int InitialHealth = 60;
    [SerializeField] private int currentHealth;
    private float detectionTime;
    private float detectionResetDuration = 2f;

    private void Start()
    {
        currentHealth = InitialHealth;
    }

    private void Update()
    {
        if (Time.time > detectionTime + detectionResetDuration && currentHealth != InitialHealth)
        {
            currentHealth = InitialHealth;
        }
    }

    public void OnDetect() //A raycast from a detector has landed on this object
    {
        if (!canBeDetected)
        {
            return;
        }
        
        detectionTime = Time.time;
        
        if (instantDetection)
        {
            OnDetectComplete();
        }
        else
        {
            //reduce health by 1
            currentHealth--;

            if (currentHealth <= 0)
            {
                OnDetectComplete();
            }
        }
    }

    private void OnDetectComplete()
    {
        Debug.Log("Object has been detected!");
        canBeDetected = false;
    }
}
