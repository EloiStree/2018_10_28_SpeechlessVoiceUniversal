using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityInputListener : MonoBehaviour {


    public OnUnityInput m_onCharacterFound;
    public OnUnityInput m_onStringFound;

    [System.Serializable]
    public class OnUnityInput : UnityEvent<string> { }

	void Update () {
        string str= Input.inputString;
        if (!string.IsNullOrEmpty(str)) {
            char[] c = str.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                string s =""+ c[i];
                m_onCharacterFound.Invoke(s);
            }
            m_onStringFound.Invoke(str);
        }
		
	}
}
