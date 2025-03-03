using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_test : Hand_sending_data
{
    Text handPos;

    public void TestUI()
    {
        print("Your hand position is: " + handPos);
    }
}
