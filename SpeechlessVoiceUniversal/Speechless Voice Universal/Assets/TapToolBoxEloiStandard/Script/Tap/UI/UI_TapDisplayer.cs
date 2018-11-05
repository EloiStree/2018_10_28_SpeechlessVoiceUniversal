using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TapDisplayer : MonoBehaviour {

    public UI_HandsTapValue m_hands;
    public UI_HandTapValue m_hand;
    public UI_TapValue m_tap;

	public void SetWith ( HandsTapValue hands) {
        if (hands == null)
            return;

        if(m_hands!=null)
            m_hands.SetWith(hands);
        if (m_hand != null)
            m_hand.SetWith(hands.GetHandTapValue());
        if (m_tap != null)
            m_tap.SetWith(hands.GetTapValue());

    }
}
