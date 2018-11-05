using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLaunchMetaPossibility : MonoBehaviour {

    public MetaLanguageUtilitary m_utilitary;
    public float m_timeBetweenSwitch = 3f;
    public string[] m_ids;

    void Start () {
        InvokeRepeating("RandomId", 0, m_timeBetweenSwitch);
	}
	
	void RandomId() {
        m_ids = m_utilitary.GetValideIdenities();
        m_utilitary.DisplayMetaLanguage(m_ids[UnityEngine.Random.Range(0, m_ids.Length)]);

	}
}
