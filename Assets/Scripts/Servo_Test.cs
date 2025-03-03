using Leap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
public class Servo_Test : PostProcessProvider
{
    void Start()
    {

    }

    void Update()
    {

    }
    public override void ProcessFrame(ref Frame inputFrame)
    {
        foreach (var hand in inputFrame.Hands)
        {
           
        }
    }
    void OnUpdateHand(Hand _hand)
    {
        Finger _index = _hand.GetIndex();
    }
}
