using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TapValue : MonoBehaviour
{
    public Toggle[] m_combo = new Toggle[5];

    public void SetWith(TapValue value)
    {
        if (value == null) {
            Clear();
            return;
        }
        for (int i = 0; i < 5; i++)
        {
            m_combo[i].isOn = (value.IsFingerActive(i));
        }
    }


    public  void Clear()
    {
        for (int i = 0; i < 5; i++)
        {
            m_combo[i].isOn = false;

        }
    }
}