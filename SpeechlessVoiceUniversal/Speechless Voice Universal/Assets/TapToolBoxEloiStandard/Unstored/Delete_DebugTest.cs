using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete_DebugTest : MonoBehaviour {
    
	void Update () {

        if (TapUtilitary.IsFingerDown(FingerIndex.LeftPinky))
        {
            Debug.Log("DOWN PINKY");
        }

        if (TapUtilitary.IsFingerPressing(FingerIndex.LeftPinky))
        {
            Debug.Log("PRESS PINKY");
        }

        if (TapUtilitary.IsFingerUp(FingerIndex.LeftPinky))
        {
            Debug.Log("UP PINKY");
        }

    }
}
