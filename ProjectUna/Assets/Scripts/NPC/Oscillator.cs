using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private bool oscillatePosition = true;
    [SerializeField] private bool oscillateRotation = true;

    [SerializeField] private float patrolSpeed = 1;
    [SerializeField] private Vector3 patrolVector;
    private Vector3 startingPos;

    [SerializeField] private float patrolAngularSpeed = 1;
    [SerializeField] private float patrolRotation;
    private float startingRot;

    
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        startingRot = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time;

        if (oscillatePosition)
        {
            UpdatePosition(Mathf.Sin(t * patrolSpeed));
        }
        if (oscillateRotation)
        {
            UpdateRotation(Mathf.Sin(t * patrolAngularSpeed));
        }       
    }

    private void UpdatePosition(float sinFactor)
    {
        transform.position = new Vector3(patrolVector.x * sinFactor + startingPos.x, patrolVector.y * sinFactor + startingPos.y, patrolVector.z * sinFactor + startingPos.z);
    }

    private void UpdateRotation(float sinFactor)
    {
        transform.eulerAngles = new Vector3(0, 0, patrolRotation * sinFactor + startingRot);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(pos, patrolVector+ pos);
        Gizmos.DrawLine(pos, -patrolVector+ pos);
    }
}
