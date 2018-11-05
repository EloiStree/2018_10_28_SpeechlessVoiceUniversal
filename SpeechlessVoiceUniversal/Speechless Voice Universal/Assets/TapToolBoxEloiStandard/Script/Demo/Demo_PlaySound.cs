using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Demo_PlaySound : MonoBehaviour {

    public GameObject m_prefab;
    public StringToAudio[] m_keyToAudio;
    public TapToAudio[] m_tapToAudio;

    public void PlayAudio(string actionKey, HandsTapValue handsTap) {

        AudioClip audio = null;
        audio = LookFor(handsTap);

        Play(audio);

    }
    public void PlayAudio(string actionKey)
    {

        AudioClip audio = null;
        audio = LookFor(actionKey);

        Play(audio);

    }

    private AudioClip LookFor(string key)
    {
        AudioClip [] audios = m_keyToAudio.Where(k => k.m_key == key).Select(k => k.m_audio).ToArray();
        if (audios.Length > 0)
            return audios[0];
        return null;
    }
    private AudioClip LookFor(HandsTapValue tap)
    {
        AudioClip[] audios = m_tapToAudio.Where(k => HandsTapValue.AreEquals( k.m_key, tap) ).Select(k => k.m_audio).ToArray();
        if (audios.Length > 0)
            return audios[0];
        return null;
    }

    private void Play(AudioClip audio)
    {
        if (audio == null)
            return;

        GameObject obj = GameObject.Instantiate(m_prefab);
        if (obj == null)
            return;

        obj.SetActive(true);
        AudioSource source = obj.GetComponent<AudioSource>();
        if (source != null) {
            source.clip = audio;
            source.Play();
        }
    }

    [System.Serializable]
    public struct StringToAudio
    {
        public string m_key;
        public AudioClip m_audio;

    }
    [System.Serializable]
    public struct TapToAudio
    {
        public HandsTapValue m_key;
        public AudioClip m_audio;

    }
}
