using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAll : MonoBehaviour {


    public bool state = true;

    public GameObject[] toSwitch;

	// Use this for initialization
	void Start () {
        SwitchFct();
	}

    public  void SwitchFct()
    {
        state = !state;
        for (int i = 0; i < toSwitch.Length; i++)
        {
            toSwitch[i].SetActive(!state);

        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
