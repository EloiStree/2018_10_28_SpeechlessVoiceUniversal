using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRecorder : MonoBehaviour {

    public string m_audioName="Default";
    public int m_maxTime = 30;
    public FileStorageAccess m_directory;
    public AudioPlayer m_player;
    public SoundAccessor m_soundAccessor;

    [Header("Debug")]
    public AudioClip m_clip;

    public void SetAudioName(string name)
    {
        m_audioName = name;

    }

    public void StartRecord()
    {
        Debug.Log("Start");
        m_clip = Microphone.Start(null, false, m_maxTime, 44100);
    }

    public void StopRecord()
    {
        Debug.Log("stop " + m_audioName);
        m_audioName = (RemoveExtention(m_audioName));
        Microphone.End(null);
        if (m_clip != null && !string.IsNullOrEmpty(m_audioName))
            SavWav.Save(m_audioName + ".wav", m_clip, m_directory.GetDirectoryPath(FileStorageAccess.StorageType.Public)+"/");
    }

    public void ReplayRecord() {

        StartCoroutine(PlaySound());
    }
   
    private IEnumerator PlaySound()
    {


        yield return  m_soundAccessor.GetSound(m_audioName);
        m_clip = m_soundAccessor.GetLoadedsound();
        m_player.Play(m_clip);
    }

    private string RemoveExtention(string text)
    {
        text = text.Replace(".wav", "");
        text = text.Replace(".mp3", "");
        return text;
    }

    public void SaveRecording() {

        SavWav.Save(m_audioName, m_clip, m_directory.GetDirectoryPath(FileStorageAccess.StorageType.Public));
    }
   
}
