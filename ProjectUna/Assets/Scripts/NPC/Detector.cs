using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] static Transform player; //The object the detector are looking for

    [Range(0,360)] public float detectionAngle = 30;
    public float detectionRange = 5;

    public LayerMask sandbox;
    
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetectInCone();
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    void DetectInCone()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;


        Vector3 dirToTarget = (player.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, dirToTarget) < detectionAngle / 2)
        {
            float dstToPlayer = Vector3.Distance(transform.position, player.position);
            //Debug.Log("In Cone");

            RaycastHit ray;

            if (Physics.Raycast(transform.position, dirToTarget,out ray ,dstToPlayer, sandbox))
            {
                if (ray.transform == player)
                {
                    GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.up) * detectionRange;
        Gizmos.DrawRay(transform.position, direction);
    }
}
