using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPage : MonoBehaviour {
    public string m_pageName = "indexpage";
    public Animator m_animator;

    public void SetPage(int index) {

        m_animator.SetInteger(m_pageName, index);
    }
}
