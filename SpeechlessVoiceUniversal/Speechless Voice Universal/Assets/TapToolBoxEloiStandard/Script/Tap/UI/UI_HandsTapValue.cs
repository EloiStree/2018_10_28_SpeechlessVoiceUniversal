using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI_HandsTapValue : MonoBehaviour {
    
    public UI_TapValue m_left;
    public UI_TapValue m_right;

    public void SetWith(HandTapValue value, bool withReset=true) {

        if (value.m_handType == HandType.Left)
        {
            m_left.SetWith(value);
            if (withReset)
                m_right.SetWith(null);
        }
        else if (value.m_handType == HandType.Right) {
            m_right.SetWith(value);

            if (withReset)
                m_left.SetWith(null);
        }
    }

    internal HandsTapValue GetTapState()
    {

        bool[] handLeft = new bool[10];
        for (int i = 0; i < 5; i++)
        {
            handLeft[i] = m_left.m_combo[i].isOn;
        }
        for (int i = 0; i < 5; i++)
        {
            handLeft[i+5] = m_right.m_combo[i].isOn;
        }

        HandsTapValue hs = new HandsTapValue();
        hs.Set(handLeft);
        return hs;

    }

    public void SetWith(HandsTapValue value)
    {
        if (value == null)
        {
            Clear();
            return;
        }
        m_left.SetWith(value.GetHand(HandType.Left));
        m_right.SetWith(value.GetHand(HandType.Right));
    }

    public  void Clear()
    {
        m_left.Clear();
        m_right.Clear();
    }
}
