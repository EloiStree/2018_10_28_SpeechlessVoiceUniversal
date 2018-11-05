using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapUnityUiSimulator : TapSimulator { 


    public ButtonPlus [] m_fingers = new ButtonPlus[10];

    public override bool IsOn(FingerIndex fingerIndex)
    {
            return m_fingers[(int)fingerIndex].IsUserPressing();
    }

    private void Reset()
    {
        m_context = TapInputType.UIInput;
    }
}
