using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddLanguage : MonoBehaviour {

    public LanguagePackageMono m_package;

    public InputField m_identity;
    public InputField m_english;
    public InputField m_original;
    public LanguageSwitcher m_originalLanguage;
    public InputField m_foreing;
    public LanguageSwitcher m_foreignLanguage;
    public InputField m_foreignAudio;


    public void Register () {
        m_package.m_package.Add(m_identity.text, m_english.text, m_original.text, m_originalLanguage.GetLanguage(), m_foreing.text, m_foreignLanguage.GetLanguage(), m_foreignAudio.text);

    }
}
