using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//SHAMELESSLY stolen from the fantastic Sebastian Lague on YT
[CustomEditor (typeof(Detector))]
public class EditorDetectorDisplay : Editor
{
    private void OnSceneGUI()
    {
        Detector dtr = (Detector)target;
        Handles.color = Color.white;

        Handles.DrawWireArc(dtr.transform.position, Vector3.forward, Vector3.up, 360, dtr.detectionRange);

        Vector3 viewAngleA = dtr.DirFromAngle(-dtr.detectionAngle / 2, false);
        Vector3 viewAngleB = dtr.DirFromAngle(dtr.detectionAngle / 2, false);

        Handles.DrawLine(dtr.transform.position, dtr.transform.position + viewAngleA * dtr.detectionRange);
        Handles.DrawLine(dtr.transform.position, dtr.transform.position + viewAngleB * dtr.detectionRange);
    }
}
