using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapUtilitaryConfig : MonoBehaviour {

    public HandType m_handTypeStart;


    [Header("Debug")]
    public GameObject[] m_listeners;

    private void Awake()
    {
        TapUtilitary.SetListeners(TryFindGameObjectWithType<ITapListener>(ref m_listeners));
        TapUtilitary.m_userHandType = m_handTypeStart;
    }


    public static T[] TryFindGameObjectWithType<T>(ref GameObject[] obj)
    {
        GameObject[] gos = UnityEngine.Object.FindObjectsOfType<GameObject>();
        List<GameObject> findObj = new List<GameObject>();

        List<T> findListener = new List<T>();
        T find;
        for (int i = 0; i < gos.Length; i++)
        {
            find = find = gos[i].GetComponent<T>();
            if ((find) != null)
            {
                findObj.Add(gos[i]);
                findListener.Add(find);
            }
        }
        obj = findObj.ToArray();
        return findListener.ToArray();
    }
}
