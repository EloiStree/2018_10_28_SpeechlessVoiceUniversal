using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BindingToBackgroundImage : MonoBehaviour {

    public Image m_imageToTarget;
    public ActionToImage[] m_actionsToImage;
    public Sprite m_default;

    [System.Serializable]
    public class ActionToImage {
        public string m_action;
        public Sprite m_image;
    }
    
    public void ReceivedAction(string actionName) {
        Sprite used = m_default;
        foreach (ActionToImage atI in m_actionsToImage)
        {
            if (atI.m_action == actionName)
                used = atI.m_image;
        }

        m_imageToTarget.sprite = used;
    }


}
