using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ListenToTapValue : MonoBehaviour, ITapListener
{

     TapInputType m_context =TapInputType.TapWithUs;
    public bool IsListening()
    {
        return m_listenToTapWithUsStandard;
    }

    public void SetListenerTo(bool on)
    {
        m_listenToTapWithUsStandard = on;
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
    [SerializeField]
    bool m_listenToTapWithUsStandard=true;
    public OnTapValueDetected m_onTapWithUsDetected;

    public HandsTapValue m_lastFrame = new HandsTapValue(TapCombo.T_____, TapCombo.T_____);
    public HandsTapValue m_currentFrame = new HandsTapValue(TapCombo.T_____, TapCombo.T_____);
  

    void Update () {

        string input = Input.inputString;
        
                m_currentFrame.Clear();

        if (input.Length > 0) {
            m_lastFrame.Clear();
            foreach (char c in input.ToCharArray())
            {
                if (m_listenToTapWithUsStandard)
                {
                    TapValue tapValue = TapUtility.GetTapBasedOnTapWithUsStandard(c);
                    if (tapValue == null || !tapValue.HasFingersPressed())
                    {
                        if (Input.GetKeyDown(KeyCode.Backspace))
                            tapValue = new TapValue(TapCombo.T_OOO_);
                        if (Input.GetKeyDown(KeyCode.Return))
                            tapValue = new TapValue(TapCombo.TO__OO);
                        if (Input.GetKeyDown(KeyCode.Space))
                            tapValue = new TapValue(TapCombo.TOOOOO);

                    }
                    m_lastFrame.Append(tapValue);
                    m_currentFrame.Append(tapValue);
                    m_onTapWithUsDetected.Invoke(tapValue);


                    if (toDoOntapvalueDetected != null)
                        toDoOntapvalueDetected(this, tapValue);
                }
            }
        }
    }



    public ToDoOnTapValueDetected toDoOntapvalueDetected;

    public void AddListener(ToDoOnTapValueDetected listener)
    {
        RemoveListener(listener);
        toDoOntapvalueDetected += listener;
    }

    public void RemoveListener(ToDoOnTapValueDetected listener)
    {
        toDoOntapvalueDetected -= listener;
    }

    public void AddListener(ToDoOnHandsTapValueDetected listener)
    {
    }

    public void RemoveListener(ToDoOnHandsTapValueDetected listener)
    {
    }

    public bool IsFingerPressing(FingerIndex finger)
    {
        return m_currentFrame.IsDown(finger);

    }
}

[System.Serializable]
public class OnHandsValueDetected : UnityEvent<HandsTapValue> { }
[System.Serializable]
public class OnHandValueDetected : UnityEvent<HandTapValue> { }
[System.Serializable]
public class OnTapValueDetected : UnityEvent<TapValue> { }
