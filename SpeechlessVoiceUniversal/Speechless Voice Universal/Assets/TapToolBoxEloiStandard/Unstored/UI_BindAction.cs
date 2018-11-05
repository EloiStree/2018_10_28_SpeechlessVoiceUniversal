using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BindAction : MonoBehaviour {

    public Text m_actionDisplay;
    public Text m_tapValueDisplay;
    public UI_TapValue m_tapValue;
    public UI_HandTapValue m_handValue;
    public UI_HandsTapValue m_handsValue;


    public void SetWith(TapValue value)
    {
        SetWith("", value);
    }


    public void SetWith(HandTapValue value)
    {
        SetWith("", value);
    }


    public void SetWith(HandsTapValue value)
    {
        SetWith("", value);
    }

    public void SetWith(string actionContext, TapValue value)
    {
        SetActionContent(actionContext, GetDescriptionOf(value));
        CleerAll();
        if(m_tapValue)
        m_tapValue.SetWith(value);
    }

    public void SetWith(string actionContext, HandTapValue value)
    {
        SetActionContent(actionContext, GetDescriptionOf(value));
        CleerAll();
        if(m_handValue)
        m_handValue.SetWith(value);

    }
    public void SetWith(string actionContext, HandsTapValue value)
    {

        SetActionContent(actionContext, GetDescriptionOf(value));
        CleerAll();
        if(m_handsValue)
        m_handsValue.SetWith(value);
    }

    private void CleerAll()
    {
        m_tapValue.Clear();
        m_handValue.Clear();
        m_handsValue.Clear();
    }

    private string GetDescriptionOf(TapValue value)
    {
        return "TAP: "+ value.ToString();
    }
    private string GetDescriptionOf(HandTapValue value)
    {
        return "HAND: " + value.ToString();
    }
    private string GetDescriptionOf(HandsTapValue value)
    {
        return "HANDS: " + value.ToString();
    }



    public  void SetActionContent(string actionContext, string tapValueDescription)
    {
        bool isEmpty = string.IsNullOrEmpty(actionContext);
        if(m_actionDisplay)
             m_actionDisplay.text = isEmpty?"": actionContext;
        if(m_tapValueDisplay)
             m_tapValueDisplay.text = isEmpty ? "" : tapValueDescription;
    }
}
