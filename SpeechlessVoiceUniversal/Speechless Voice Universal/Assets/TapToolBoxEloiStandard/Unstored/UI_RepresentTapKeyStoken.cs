using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RepresentTapKeyStoken : MonoBehaviour {

    public Color m_down = Color.green*0.8f;
    public Color m_pressing = Color.green*0.6f;
    public Color m_up = Color.green*1f;

    public Image [] m_finggersButton = new Image[10];

  
	void Update () {


        for (int i = 0; i < 10; i++)
        {
            Color c = Color.white;
            if (TapUtilitary.IsFingerDown((FingerIndex)i))
            {
                c = m_down;
            }
            else if (TapUtilitary.IsFingerPressing((FingerIndex)i))
            {
                c = m_pressing;
            }else if (TapUtilitary.IsFingerUp((FingerIndex)i))
            {
                c = m_up;
            }
            m_finggersButton[i].color = c;
        }

	}
}
