using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextPlus : MonoBehaviour {

    Text m_text;

    public bool m_useMaxCharacter = true;
    public int m_maxCharacter=50;
    public bool m_withLineReturn;
    public void Start()
    {
        m_text = GetComponent<Text>();
        CheckMaxLenght();
    }
    public void ApprendFront(string text)
    {
        if (m_text != null) {

            m_text.text = text + (!m_withLineReturn ? "" : "\n") + m_text.text ;
            CheckMaxLenght();
        }
    }

   
    public void ApprendEnd(string text)
    {
        m_text.text =  m_text.text + (!m_withLineReturn ? "" : "\n") + text;
        CheckMaxLenght();
    }
    private void CheckMaxLenght()
    {
        if (m_useMaxCharacter)
            m_text.text = m_text.text.Substring(0,m_maxCharacter<m_text.text.Length? m_maxCharacter:m_text.text.Length );
    }


}
