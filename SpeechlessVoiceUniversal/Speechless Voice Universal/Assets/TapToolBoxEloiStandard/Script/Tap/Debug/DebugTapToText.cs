using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTapToText : MonoBehaviour {


    public Text m_debug;

    public void Display(HandsTapValue value)
    {
        if (m_debug && value != null)
            m_debug.text = "\\m/ \\m/: " + value.ToString() + "\n" + m_debug.text;
        else m_debug.text = "-1-";

    }


    public void Display (HandTapValue value) {
        if(m_debug && value!=null)
            m_debug.text = value.m_handType+" \\m/: " + value.ToString()+ "\n" + m_debug.text;
        else m_debug.text = "-2-";
    }

    public void Display(TapValue value)
    {
        if (m_debug && value != null)
            m_debug.text = "\\m/: " + value.ToString() + "\n" + m_debug.text;
        else m_debug.text = "-3-";
    }
}
