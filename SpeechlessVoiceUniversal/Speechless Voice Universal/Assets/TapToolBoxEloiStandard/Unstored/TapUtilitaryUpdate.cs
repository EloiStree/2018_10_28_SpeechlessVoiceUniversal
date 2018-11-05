using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapUtilitaryUpdate : MonoBehaviour {

    public int m_frame;
    private IEnumerator Start()
    {
        while (true) {
            yield return new WaitForEndOfFrame();
            TapUtilitary.UpdateEndFrameToCall();
        }
    }

    void Update () {
       TapUtilitary.UpdateToCall();	
        m_frame = Time.frameCount; 
    }
}
