using Leap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using UnityEngine.UI;
using System;

public class Hand_Angle : PostProcessProvider
{
    [SerializeField]
    private Transform headPos;

    public Text handAngle;
    public float angle;

    public override void ProcessFrame(ref Frame inputFrame)
    {
        if (headPos == null)
        {
            headPos = Camera.main.transform;
        }
        var shoulderBasis = Quaternion.LookRotation(
        Vector3.ProjectOnPlane(headPos.forward, Vector3.up), Vector3.up);
        Vector3 headPosition = headPos.position;

        foreach (var hand in inputFrame.Hands)
        {
            Vector3 shoulderPos = headPosition
                                + (shoulderBasis * (new Vector3(0f, -0.13f, -0.1f)
                                + Vector3.left * 0.15f * (hand.IsLeft ? 1f : -1f)));
            Vector3 shoulderToHand = hand.PalmPosition - shoulderPos;
            Vector3 bottom = Vector3.ProjectOnPlane(shoulderPos, Vector3.up);
            Vector3 axis = Vector3.ProjectOnPlane(shoulderPos, Vector3.up);
            angle = MathF.Abs(Vector3.SignedAngle(
                shoulderPos - bottom, shoulderPos - hand.PalmPosition, axis));
            //Debug.Log(angle);
            handAngle.text = "Palm's Angle is: " + angle;
        }
    }
    public Hand_Angle(float initialAngle)
    {
        angle = initialAngle;
    }
    public void SetAngle(float pass_angle)
    {
        angle = pass_angle;
    }
    public double GetAngle()
    {
        return angle;
    }
    public void OnGUI()
    {
        GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
        fontSize.fontSize = 22;
        GUI.Label(new Rect(10, 400, 200, 55), "Palm's Angle is: " + angle, fontSize);
    }
}
