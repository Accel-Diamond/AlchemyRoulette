using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class RouletteCameraBehavior : MonoBehaviour
{
    // Float used to measure the turning for the Vector3.Lerp
    private float tTurn;

    // Time it takes for the camera to fully rotate
    public float secondsToFullRotate;

    // Boolean to control where the camera should rotate to
    private bool lookAtWheelInWorld;

    // Update is called once per frame
    void LateUpdate()
    {
        if (!lookAtWheelInWorld)
        {
            tTurn += Time.deltaTime;

            transform.eulerAngles = Vector3.Lerp(Vector3.zero, Vector3.up * 360f, tTurn / secondsToFullRotate);

            if(tTurn > secondsToFullRotate)
            {
                tTurn = 0f;
            }
        }
        else
        {
            if (tTurn < 5f)
            {
                tTurn += Time.deltaTime;


                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, Vector3.right * 90f, Mathf.Pow(tTurn / 5f, 3f));
            }
        }
    }

    public void SwitchToWheel()
    {
        lookAtWheelInWorld = true;

        tTurn = 0f;
    }

    public void ReturnToHUD()
    {
        lookAtWheelInWorld = false;

        tTurn = 0f;
    }
}

[CustomEditor(typeof(RouletteCameraBehavior))]
public class EditorRouletteCameraBehavior : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var objecto = (RouletteCameraBehavior)target;
        if(GUILayout.Button("Switch to Wheel"))
        {
            objecto.SwitchToWheel();
        }

        if(GUILayout.Button("Return to HUD"))
        {
            objecto.ReturnToHUD();
        }
    }
}
