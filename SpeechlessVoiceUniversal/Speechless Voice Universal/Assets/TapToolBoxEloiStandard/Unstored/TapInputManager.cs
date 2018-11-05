using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapInputManager : MonoBehaviour {

    public TapInputType m_listeningTo= TapInputType.KeyboardInput;

    public ListenToEloiStandard m_listenerStandard;
    public ListenToTapValue     m_listenerTapWithUs;
    public TapKeyboardSimulator m_keyboardSimulator;
    public TapUnityUiSimulator  m_uiSimulator;
    public TapKeyboardSimulator m_openSimulator;

    public void Awake()
    {
        SetListenerTo(m_listeningTo);
    }

    private void SetListenerTo(TapInputType tapInput)
    {
        m_listeningTo = tapInput;
        StopAllFromListening();
        switch (tapInput)
        {
            case TapInputType.TapWithUs:
                if (m_listenerTapWithUs)
                    m_listenerTapWithUs.SetListenerTo(true);
                break;
            case TapInputType.EloiStandard:
                if (m_listenerStandard)
                    m_listenerStandard.SetListenerTo(true);
                break;
            case TapInputType.KeyboardInput:
                if (m_keyboardSimulator)
                    m_keyboardSimulator.SetListenerTo(true);
                break;
            case TapInputType.UIInput:
                if (m_uiSimulator)
                    m_uiSimulator.SetListenerTo(true);
                break;
            case TapInputType.OpenInput:
                if (m_openSimulator)
                    m_openSimulator.SetListenerTo(true);
                break;
            default:
                break;
        }
    }

    private void StopAllFromListening()
    {
        if(m_listenerStandard)
            m_listenerStandard.SetListenerTo(false);
        if(m_listenerTapWithUs)
            m_listenerTapWithUs.SetListenerTo(false);
        if(m_keyboardSimulator)
            m_keyboardSimulator.SetListenerTo(false);
        if(m_uiSimulator)
            m_uiSimulator.SetListenerTo(false);
        if(m_openSimulator)
            m_openSimulator.SetListenerTo(false);
    }

    public void Reset()
    {
        SetListenerTo(m_listeningTo);
    }

   

    public void OnValidate()
    {

        SetListenerTo(m_listeningTo);
    }


    public void SetWith(TapInputType tapInput) {
        SetListenerTo(tapInput);
    }

    public void SetWithTapWithUs() { SetWith(TapInputType.TapWithUs); }
    public void SetWithStandard() { SetWith(TapInputType.EloiStandard); }
    public void SetWithOpen() { SetWith(TapInputType.OpenInput); }
    public void SetWithUI() { SetWith(TapInputType.UIInput); }
    public void SetWithKeyboard() { SetWith(TapInputType.KeyboardInput); }

}


public enum TapInputType {
 TapWithUs,
 EloiStandard,
 KeyboardInput,
 UIInput,
 OpenInput
}