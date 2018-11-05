using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapKeyboardSimulator : TapSimulator
{


    public KeyCode[] m_fingers= new KeyCode[] {
        KeyCode.A,
        KeyCode.Z,
        KeyCode.E,
        KeyCode.R,
        KeyCode.C,
        KeyCode.N,
        KeyCode.U,
        KeyCode.I,
        KeyCode.O,
        KeyCode.P
    };

    public void SetWithAzerty() {
        if (base.m_context != TapInputType.KeyboardInput)
            return;
        m_fingers = new KeyCode[] {
        KeyCode.A,
        KeyCode.Z,
        KeyCode.E,
        KeyCode.R,
        KeyCode.C,
        KeyCode.N,
        KeyCode.U,
        KeyCode.I,
        KeyCode.O,
        KeyCode.P
    };
    }
    public void SetWithQuerty()
    {
        if (base.m_context != TapInputType.KeyboardInput)
            return;
        m_fingers = new KeyCode[] {
        KeyCode.Q,
        KeyCode.W,
        KeyCode.E,
        KeyCode.R,
        KeyCode.C,
        KeyCode.N,
        KeyCode.U,
        KeyCode.I,
        KeyCode.O,
        KeyCode.P
    };
    }

    

    private void Reset()
    {
        m_context = TapInputType.KeyboardInput;
    }
    public override bool IsOn(FingerIndex fingerId)
    {
            return Input.GetKey(m_fingers[(int)fingerId]);
    }

    
}
