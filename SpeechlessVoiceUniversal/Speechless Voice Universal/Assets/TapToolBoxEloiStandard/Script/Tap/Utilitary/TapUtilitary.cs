using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapUtilitary  {

    public static TapUtilitary Instance = new TapUtilitary();

    private static TapUtilitaryConfig m_config;


    public static TapUtilitaryConfig Configuration { get {
            if (m_config == null)
            {
                m_config = GameObject.FindObjectOfType<TapUtilitaryConfig>();
            }
            if (m_config == null)
            {
               GameObject  toCreate = Resources.Load<GameObject>("TapUtilitary");
               GameObject created = GameObject.Instantiate(toCreate);
                created.name = "#TapUtility";
                m_config = created.GetComponent<TapUtilitaryConfig>();  
            }

            return m_config;
        }
    }

  

    public static HandType m_userHandType = HandType.Right;
    public static OnTapValueDetected m_onTapDetected= new OnTapValueDetected();
    public static OnHandsValueDetected m_onAllTapDetected = new OnHandsValueDetected();
    public static ITapListener[] m_listeners = new ITapListener[0];

    public static void SetHandTo(HandType value)
    {
        m_userHandType = value;

    }

    public static void NotifyTapDetected( TapValue value)
    {
        CheckForSingleton();
        TapValue val = new TapValue(value.GetTapCombo());
        if (m_userHandType == HandType.Left)
            val.Inverse();

        HandsTapValue hands = TapUtility.ConvertToHandsValue(m_userHandType , val);

        NotifyHandsTapValueDetected(TapInputType.TapWithUs, hands);

        if (IsListeningTo(TapInputType.TapWithUs))
            m_onTapDetected.Invoke(value);
    }

   
    public static void NotifyHandsTapValueDetected(TapInputType source, HandsTapValue value)
    {
        CheckForSingleton();
        if (IsListeningTo(source))
             m_onAllTapDetected.Invoke(value);
    }


    public static void ListenTo(TapInputType inputType, bool listenOn) {

        CheckForSingleton();
        if (!m_listenerIsActive.ContainsKey(inputType))
            m_listenerIsActive.Add(inputType, listenOn);
        else m_listenerIsActive[inputType] = listenOn;
    }
    public static  bool IsListeningTo(TapInputType inputType) {

        CheckForSingleton();
        if (!m_listenerIsActive.ContainsKey(inputType))
            m_listenerIsActive.Add(inputType, false);

        return m_listenerIsActive[inputType] ;
    }
    private static Dictionary<TapInputType, bool> m_listenerIsActive = new Dictionary<TapInputType, bool>();





    public static void CheckForSingleton()
    {
        TapUtilitaryConfig config = Configuration;
    }

    internal static void SwitchListener(TapInputType tapInput)
    {
        ListenTo(tapInput, !IsListeningTo(tapInput));
    }

    internal static void SetTapHandType(HandType handType)
    {
        m_userHandType = handType;
    }
    public static void SwitchTapHand()
    {
        if (m_userHandType == HandType.Left)
            m_userHandType = HandType.Right;
        else m_userHandType = HandType.Left;

    }

    public static void SetKeyboardsToQuerty()
    {
        TapKeyboardSimulator[] keyboards = GameObject.FindObjectsOfType<TapKeyboardSimulator>();
        for (int i = 0; i < keyboards.Length; i++)
        {
            keyboards[i].SetWithQuerty();
        }
    }

    public static void SetKeyboardsToAzerty()
    {
        TapKeyboardSimulator[] keyboards = GameObject.FindObjectsOfType<TapKeyboardSimulator>();
        for (int i = 0; i < keyboards.Length; i++)
        {
            keyboards[i].SetWithAzerty();
        }
    }

    public static void StopListeningToInputs()
    {
        foreach (TapInputType input in Enum.GetValues(typeof(TapInputType)))
        {
            ListenTo(input,false);
        }
    }




    #region SET LISTERNER

    internal static void SetListeners(ITapListener[] tapListener)
    {

        foreach (ITapListener list in m_listeners)
        {
            list.RemoveListener(NotifyTapDetected);
            list.RemoveListener(NotifyHandsTapDetected);
        }

        m_listeners = tapListener;
        foreach (ITapListener list in m_listeners)
        {
            list.AddListener(NotifyTapDetected);
            list.AddListener(NotifyHandsTapDetected);
        }

    }

    private static void NotifyHandsTapDetected(ITapListener listener, TapValue value)
    {
        NotifyTapDetected(value);
    }

    private static void NotifyTapDetected(ITapListener listener, HandsTapValue value)
    {
        NotifyHandsTapValueDetected(listener.GetListenerType(), value);
    }
    #endregion



    private static int m_lasUpdateFrame=-1;
    private static HandsTapValue m_handsState = new HandsTapValue(TapCombo.T_____, TapCombo.T_____);
    public  static HandsTapValue dd = new HandsTapValue(TapCombo.T_____, TapCombo.T_____);

    public static HandsTapValue GetHandsState() {
        int frame = Time.frameCount;
        if (m_lasUpdateFrame == frame)
        {
            return m_handsState;
        }
        else m_handsState.Clear();

        m_lasUpdateFrame = frame;
        bool[] fingersState = new bool[10];

        foreach (ITapListener listener in m_listeners)
        {
            for (int i = 0; i < 10; i++)
            {
                if (IsListeningTo(listener.GetListenerType())) {
                    if (listener.GetListenerType() == TapInputType.TapWithUs) {

                        bool isLeft = m_userHandType == HandType.Left;

                        if (isLeft)
                        {
                            if (i < 5)
                                fingersState[i] = fingersState[i] || listener.IsFingerPressing((FingerIndex)i);
                        }
                        else
                        {
                            if (i > 4)
                                fingersState[i] = fingersState[i] || listener.IsFingerPressing((FingerIndex)i);
                        }

                    }
                    else
                     fingersState[i] = fingersState[i] || listener.IsFingerPressing((FingerIndex)i);

                    
                }
            }


        }
       
        m_handsState.Set(fingersState);
        //if(m_handsState.HasFingerDown())
        //    Debug.Log(frame + ": " + m_handsState.ToString());

        return m_handsState;
    }

   

    public bool IsFinngerPressing(FingerIndex finger) {

        return m_handsState.IsDown(finger);
    }

    #region DOWN & UP Detection
    // SHould be isolted in different class

    public static void UpdateToCall()
    {

    //    m_handsState = GetHandsState();
    //    DetectedAndUpdateDownState(m_handsState.GetHandsState());
    }
    public static void UpdateEndFrameToCall()
    {

        m_handsState = GetHandsState();
        DetectedAndUpdateDownState(m_handsState.GetHandsState());
    }


    private static void DetectedAndUpdateDownState(bool[] fingersState)
    {
        bool onValueChange;
        bool onDownDetected;
        bool onUpDetected;
        for (int i = 0; i < 10; i++)
        {
            m_handsIsDownState[i] = false;

            onValueChange = DoesFingerChangedState(ref fingersState, ref m_handsPressState, i);
            onDownDetected = DoesChangeStateIsADown(fingersState, onValueChange, i);
            onUpDetected = DoesChangeStateIsAUp(fingersState, onValueChange, i);

            m_handsIsDownState[i] = onDownDetected;
            m_handsIsUpState[i] = onUpDetected;
            m_handsPressState[i] = fingersState[i];
        }
    }

    private static bool DoesChangeStateIsADown(bool[] fingersState, bool onValueChange, int i)
    {
        return onValueChange && fingersState[i];
    }

    private static bool DoesChangeStateIsAUp(bool[] fingersState, bool onValueChange, int i)
    {
        return onValueChange && !fingersState[i];
    }


    private static bool DoesFingerChangedState(ref bool[] fingersState,ref bool[] previousState, int index)
    {
        return fingersState[index] != m_handsPressState[index];
    }

    public static bool[] m_handsPressState = new bool[10];
    public static bool[] m_handsIsDownState = new bool[10];
    public static bool[] m_handsIsUpState = new bool[10];

    
    internal static bool IsFingerDown(FingerIndex value)
    {
        return m_handsIsDownState[(int)value];
    }

    internal static bool IsFingerPressing(FingerIndex value)
    {
        return m_handsPressState[(int)value];
    }
    internal static bool IsFingerUp(FingerIndex value)
    {
        return m_handsIsUpState[(int)value];
    }

    #endregion



}
