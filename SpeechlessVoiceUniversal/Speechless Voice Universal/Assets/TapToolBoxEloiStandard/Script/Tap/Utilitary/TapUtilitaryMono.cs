using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapUtilitaryMono : MonoBehaviour {

    public OnHandsValueDetected m_onHandsTapDetected;

    public TapInputType[] m_listenToAtStart = new TapInputType[] { TapInputType.UIInput, TapInputType.TapWithUs};


    [Header("View")]
    public HandType m_userHandType;
    public HandsTapValue m_handsState;
    public HandsTapValue dd;


    [Header("Debug ")]
    public List<HandsTapValue> m_debugReceived = new List<HandsTapValue>();
    public int m_debugCount=10;
    [Header("Debug TMP")]
    public bool[] down;
    public bool[] pressing;
    public bool[] up;

    public void Update()
    {
        m_userHandType = TapUtilitary.m_userHandType;
        m_handsState = TapUtilitary.GetHandsState();

        down = TapUtilitary.m_handsIsDownState;
        pressing = TapUtilitary.m_handsPressState;
        up = TapUtilitary.m_handsIsUpState;

        dd = TapUtilitary.dd;
    }

    void Start () {
        TapUtilitary.CheckForSingleton();

        foreach (TapInputType type in m_listenToAtStart)
        {
            TapUtilitary.ListenTo(type, true);
        }
        
        TapUtilitary.m_onAllTapDetected.AddListener(
            delegate (HandsTapValue value) {
                m_onHandsTapDetected.Invoke(value);
                AddToDebug(value);
            });
    }

    // UnTESTED
    private void AddToDebug(HandsTapValue value)
    {
        m_debugReceived.Insert(0, value);
        bool toMuchValue;
        do
        {
            toMuchValue = m_debugReceived.Count > m_debugCount;
            if (toMuchValue)
                m_debugReceived.RemoveAt(m_debugCount - 1);

        }
        while (toMuchValue);

    }



    public void SetWith(TapInputType tapInput)
    {
        TapUtilitary.ListenTo(tapInput,true);
    }

    public void SetWithTapWithUs() { SetWith(TapInputType.TapWithUs); }
    public void SetWithStandard() { SetWith(TapInputType.EloiStandard); }
    public void SetWithOpen() { SetWith(TapInputType.OpenInput); }
    public void SetWithUI() { SetWith(TapInputType.UIInput); }
    public void SetWithKeyboard() { SetWith(TapInputType.KeyboardInput); }

    public void SwitchTapWithUs() { SwitchLisener(TapInputType.TapWithUs); }
    public void SwitchStandard() {  SwitchLisener(TapInputType.EloiStandard); }
    public void SwitchOpen() {      SwitchLisener(TapInputType.OpenInput); }
    public void SwitchUI() {        SwitchLisener(TapInputType.UIInput); }
    public void SwitchKeyboard() {  SwitchLisener(TapInputType.KeyboardInput); }

    public void SetTapWithUsHandSide(HandType hand) {
        if (hand == HandType.Left)
            SetTapWithUsAsLeft();
        else SetTapWithUsAsRight();
    }
    public void SetTapWithUsAsLeft() { TapUtilitary.SetTapHandType(HandType.Left); }
    public void SetTapWithUsAsRight() { TapUtilitary.SetTapHandType(HandType.Right); }

    public void SetAzerty()
    {
        TapUtilitary.SetKeyboardsToAzerty();
    }
    public void SetQuerty()
    {
        TapUtilitary.SetKeyboardsToQuerty();
    }


    public void StopListeningToInput() {
        TapUtilitary.StopListeningToInputs();
    }


    private void SwitchLisener(TapInputType tapInput)
    {
        TapUtilitary.SwitchListener(tapInput);
    }
}


public class FingerState{

    [SerializeField]
    private bool[] fingerState = new bool[10];


    public bool IsDown(FingerIndex fingerIndex, bool ignoreHandSide=false) {

        if (ignoreHandSide)
        {
            switch (fingerIndex)
            {
                case FingerIndex.LeftPinky:
                case FingerIndex.RighPinky:
                    return fingerState[(int)FingerIndex.LeftPinky] || fingerState[(int)FingerIndex.RighPinky];
                case FingerIndex.LeftMiddle:
                case FingerIndex.RightMiddle:
                    return fingerState[(int)FingerIndex.LeftMiddle] || fingerState[(int)FingerIndex.RightMiddle];
                case FingerIndex.leftThumb:
                case FingerIndex.RightThumb:
                    return fingerState[(int)FingerIndex.leftThumb] || fingerState[(int)FingerIndex.RightThumb];
                case FingerIndex.LeftIndex:
                case FingerIndex.RightIndex:
                    return fingerState[(int)FingerIndex.LeftIndex] || fingerState[(int)FingerIndex.RightIndex];
                case FingerIndex.LeftRing:
                case FingerIndex.RightRing:
                    return fingerState[(int)FingerIndex.LeftRing] || fingerState[(int)FingerIndex.RightRing];
                default:
                    break;
            }
        }
        else {
            return fingerState[(int)fingerIndex];

        }
        return false;
    }
   

}
