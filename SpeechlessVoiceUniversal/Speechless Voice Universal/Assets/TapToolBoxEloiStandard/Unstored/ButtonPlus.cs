using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class ButtonPlus :MonoBehaviour, IPointerDownHandler , IPointerUpHandler
{
    [SerializeField, Header("Debug (Don't Touch)")]
    private bool m_onPressing;
    public bool IsUserPressing() { return m_onPressing; }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_onPressing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_onPressing = false;
    }
}
