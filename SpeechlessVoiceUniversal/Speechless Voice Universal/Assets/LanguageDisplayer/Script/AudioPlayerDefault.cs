using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerDefault : AudioPlayer
{
   
    public AudioSource[] m_audioSources;
    public int index;


    public override void Play(AudioClip audio)
    {
        m_audioSources[index].clip = audio;
        m_audioSources[index].Play();
        index++;
        if (index >= m_audioSources.Length)
            index = 0;
    }

    public override void StopAllSounds()
    {
        for (int i = 0; i < m_audioSources.Length; i++)
        {
            m_audioSources[i].Stop();

        }
    }
}

public abstract class AudioPlayer : MonoBehaviour
{
    public abstract void Play(AudioClip audio);

    public abstract void StopAllSounds();
    
}