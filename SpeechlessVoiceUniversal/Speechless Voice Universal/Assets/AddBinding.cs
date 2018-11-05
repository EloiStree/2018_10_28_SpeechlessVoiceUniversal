using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddBinding : MonoBehaviour {

    public TapBindingToAction m_bind;
    public InputField m_identity;
    public UI_HandsTapValue m_hands;
    
	
	public void Register () {

        m_bind.Add(m_identity.text, m_hands.GetTapState());

    }
}
