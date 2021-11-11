using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] static Detectable[] detectables; //The objects the detectors are looking for

    [Range(0,360)] public float detectionAngle = 30;
    public float detectionRange = 5;

    public LayerMask sandbox;
    
    // Start is called before the first frame update
    void Start()
    {
        if (detectables == null)
        {
            detectables = GameObject.FindObjectsOfType<Detectable>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (detectables != null)
        {
            foreach (Detectable dt in detectables)
            {
                Transform obj = dt.gameObject.transform;
                DetectInCone(obj, dt);
            }           
        }        
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    void DetectInCone(Transform detectable, Detectable dt)
    {
        Vector3 dirToTarget = (detectable.position - transform.position).normalized;
        if (Vector3.Angle(transform.up, dirToTarget) < detectionAngle / 2)
        {
            float dstTodetectables = Vector3.Distance(transform.position, detectable.position);
            //Debug.Log("In Cone");

            RaycastHit ray;

            if (Physics.Raycast(transform.position, dirToTarget,out ray ,dstTodetectables, sandbox))
            {
                if (ray.transform == detectable)
                {
                    GetComponent<MeshRenderer>().material.color = Color.red;
                    dt.OnDetect();
                    return;
                }
            }
        }

        GetComponent<MeshRenderer>().material.color = Color.green;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.up) * detectionRange;
        Gizmos.DrawRay(transform.position, direction);
    }
}
