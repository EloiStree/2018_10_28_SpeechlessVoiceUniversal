using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StresstestLanguagePackage : MonoBehaviour {

    public UI_MetaLanguageDisplayer m_displayer;
    public LanguagePackageMono m_package;
    public string[] m_randomPackage = new string[] {
        "Hey Mon Ami",
        "Hello",
        "CantSpeak"
};
    public float m_timeBeforeTest=3;
    public LanguageType m_language;
    IEnumerator Start () {

        while (true)
        {
            StressTest();
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(m_timeBeforeTest);
        }
	}

    [Header("Debug"), SerializeField]
    MetaLanguage m_lastMeta;
	void StressTest() {
        string id = m_randomPackage[UnityEngine.Random.Range(0, m_randomPackage.Length)];
        MetaLanguage meta= m_lastMeta = m_package.m_package.FindBasedOnId(id);
        m_displayer.SetWith(meta, m_language);
		
	}
}
