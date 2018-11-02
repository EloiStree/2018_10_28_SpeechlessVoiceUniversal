using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Sentence : MonoBehaviour {

    public Text m_sentenceContent;
    public Text m_sentenceLanguage;
    public Button m_soundToPlay;
    public Button m_linkedImage;

    public Sentence m_value;

    public OnMediaRequested m_onSoundRequested;
    public OnMediaRequested m_onImageRequested;

    public ArabicText m_arabicText;

    public void Start()
    {
        m_soundToPlay.onClick.AddListener(NotifyImageRequest);
        m_linkedImage.onClick.AddListener(NotifySoundRequest);

    }

    public void SetWithSentence(Sentence value)
    {
        if (value.m_language == LanguageType.Arabic)
        {
            m_arabicText.enabled = true;
            m_arabicText.Text = value.m_text;
        }
        else {
            m_arabicText.enabled = false;
        }
        m_sentenceContent.text = value.m_text;
        m_sentenceLanguage.text = value.m_language.ToString();
        m_soundToPlay.interactable = !string.IsNullOrEmpty(value.m_media.m_audioName) ;
        m_linkedImage.interactable = !string.IsNullOrEmpty(value.m_media.m_pictureName);
        m_value = value;
        if (value.m_media.m_focusOnCall)
            NotifyImageRequest();


    }

    public void NotifySoundRequest()
    {
        Debug.Log("Sound request:" + m_value.m_media.m_audioName);
        m_onSoundRequested.Invoke(m_value.m_media.m_audioName);
    }

    public void NotifyImageRequest()
    {
        m_onImageRequested.Invoke(m_value.m_media.m_pictureName);
    }
}
