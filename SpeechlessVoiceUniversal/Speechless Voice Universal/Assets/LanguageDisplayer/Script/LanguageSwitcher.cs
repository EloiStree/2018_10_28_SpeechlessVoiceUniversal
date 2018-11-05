using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LanguageSwitcher : MonoBehaviour {

    public Dropdown m_dropdown;
    public LanguageSwitch m_onLanguageChange;
    [Header("Debug")]
    public LanguageType[] m_availableLanguage;

    void Start () {
        SetOption();
        SwitchLanguages(m_dropdown.value);
        m_dropdown.onValueChanged.AddListener(SwitchLanguages);
    }

    public void SwitchLanguages (int index) {

        m_onLanguageChange.Invoke(m_availableLanguage[index]);
    }

    internal LanguageType GetLanguage()
    {
        throw new NotImplementedException();
    }

    [System.Serializable]
    public class LanguageSwitch : UnityEvent<LanguageType> {

    }

    public void OnValidate()
    {
        SetOption();

    }

    private void SetOption()
    {
        m_availableLanguage = Enum.GetValues(typeof(LanguageType)).Cast<LanguageType>().ToArray(); ;
        m_dropdown.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        for (int i = 0; i < m_availableLanguage.Length; i++)
        {
            options.Add(new Dropdown.OptionData(m_availableLanguage[i].ToString()));
        }
        m_dropdown.AddOptions(options);
    }
}
