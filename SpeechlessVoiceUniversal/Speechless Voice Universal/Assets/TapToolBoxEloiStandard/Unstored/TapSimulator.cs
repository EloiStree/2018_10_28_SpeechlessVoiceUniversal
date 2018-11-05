using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TapSimulator : MonoBehaviour, ITapListener
{

    public TapInputType m_context = TapInputType.OpenInput;
    public bool IsListening()
    {
        return m_listeningToTap;
    }

    public void SetListenerTo(bool on)
    {
        m_listeningToTap = on;
    }

    public void Switch()
    {
        SetListenerTo(!IsListening());
    }

    public string GetName()
    {
        return m_context.ToString();
    }
    public TapInputType GetListenerType()
    {
        return m_context;
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
    public bool m_listeningToTap = true;

    public float m_commitDelay=0.3f;

    [Header("Event (Touch)")]
    public OnTapValueDetected m_onTapValueDetected;
    public OnHandValueDetected m_onHandValueDetected;
    public OnHandsValueDetected m_onHandsValueDetected;

    [Header("Debug (Touch)")]
    private bool m_isListeningCombo;
    private float m_countDown;

    private TapValue lastTapLeft = new TapValue(-1), lastTapRight= new TapValue(-1);

    public  abstract bool IsOn(FingerIndex fingerIndex);
    public bool [] m_hands= new bool[10];



    
    public ToDoOnHandsTapValueDetected toDoOnhandsvalueDetected;


    public void AddListener(ToDoOnHandsTapValueDetected listener)
    {
        RemoveListener(listener);
        toDoOnhandsvalueDetected += listener;
    }

    public void RemoveListener(ToDoOnHandsTapValueDetected listener)
    {
        toDoOnhandsvalueDetected -= listener;
    }


    public void AddListener(ToDoOnTapValueDetected listener)
    {
    }

    public void RemoveListener(ToDoOnTapValueDetected listener)
    {
    }
    public bool IsFingerPressing(FingerIndex finger)
    {
        return IsOn(finger);

    }

    private void Awake()
    {
        m_hands = new bool[10];
    }
    private void Update()
    {
        bool anyFingerDown = IsAnyFingersPressing();
        if (anyFingerDown) {
            m_countDown = m_commitDelay;
            m_isListeningCombo = true;
            RefreshFingersState();
        }

        if (m_isListeningCombo && !anyFingerDown) {
            m_countDown -= Time.deltaTime;
            if (m_countDown < 0f) {
                NotifyTap();
                m_countDown = 0f;
                m_isListeningCombo = false;
            }
        }
    }

    private void NotifyTap()
    {

        if (!m_listeningToTap) {

            ResetHandsValue();
            return;
        }


        TapUtility.GetTapValueFrom(m_hands, out lastTapLeft, out lastTapRight);
        //Debug.Log(lastTapLeft.m_combo + ".<>." + lastTapRight.m_combo);

        bool leftFound = lastTapLeft != null && lastTapLeft.GetTapCombo() != TapCombo.T_____;
        bool rightFound = lastTapRight != null && lastTapRight.GetTapCombo() != TapCombo.T_____;
        bool anyFound = leftFound || rightFound;

        if (leftFound)
        {
            m_onTapValueDetected.Invoke(lastTapLeft);
            m_onHandValueDetected.Invoke(new HandTapValue(HandType.Left, lastTapLeft.GetTapCombo()));
        }
        if (rightFound)
        {
            m_onTapValueDetected.Invoke(lastTapRight);
            m_onHandValueDetected.Invoke(new HandTapValue(HandType.Right, lastTapRight.GetTapCombo()));
        }

        if (anyFound)
        {
            m_onHandsValueDetected.Invoke(new HandsTapValue(lastTapLeft.GetTapCombo(), lastTapRight.GetTapCombo()));

            if (toDoOnhandsvalueDetected != null)
                toDoOnhandsvalueDetected(this, new HandsTapValue(lastTapLeft.GetTapCombo(), lastTapRight.GetTapCombo()));
        }

        ResetHandsValue();
    }

    private void ResetHandsValue()
    {
        m_hands = new bool[10];
    }

    private bool[] GetFingersState()
    {
        bool[] fingers = new bool[10];
        for (int i = 0; i < 10; i++)
        {
            if(!fingers[i])
                fingers[i] =  IsOn((FingerIndex)i);
        }
        return fingers;
    }
    private void RefreshFingersState()
    {
        for (int i = 0; i < 10; i++)
        {
            if (!m_hands[i])
                m_hands[i] = IsOn((FingerIndex)i);
        }
    }

    private bool IsAnyFingersPressing()
    {
        for (int i = 0; i < 10; i++)
        {
            if(IsOn( (FingerIndex)i ))
                return true;
        }
        return false;
    }

    public void Reset()
    {
        CheckThat10BoolsAreSet();
    }
    public void OnValidate()
    {
        CheckThat10BoolsAreSet();
    }

    private void CheckThat10BoolsAreSet()
    {
        if (m_hands.Length != 10)
            m_hands = new bool[10];
    }

}

