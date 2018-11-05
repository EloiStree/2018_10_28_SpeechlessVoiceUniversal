using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour {

    public string m_url;

    public void OpenBrowserWithURL()
    {
        OpenBrowserWithURL(m_url);
    }
    public void OpenBrowserWithURL(string url)
    {
        Application.OpenURL(url);
    }
}
