using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HandTapValue : MonoBehaviour {

    public GameObject m_left, m_right;
    public UI_TapValue m_tapvalue;

    public void SetWith(HandsTapValue value)
    {
        if (!value.GetLeftValue().HasFingersPressed() && value.GetRightValue().HasFingersPressed())
        {
            SetWith(new HandTapValue(HandType.Right, value.GetRightValue().GetTapCombo()));
        }
        else if (value.GetLeftValue().HasFingersPressed() && !value.GetRightValue().HasFingersPressed())
        {

            SetWith(new HandTapValue(HandType.Left, value.GetLeftValue().GetTapCombo()));
        }
        else Clear();

    }

        public void SetWith(HandTapValue value)
    {
        if (value == null)
        {
            Clear();
            return;
        }
        bool isLeft = value.m_handType == HandType.Left;
        m_left.SetActive(isLeft);
        m_right.SetActive(!isLeft);
        m_tapvalue.SetWith(value);
    }

    internal void Clear()
    {
        m_left.SetActive(true);
        m_right.SetActive(true);
        m_tapvalue.Clear();
    }
}

