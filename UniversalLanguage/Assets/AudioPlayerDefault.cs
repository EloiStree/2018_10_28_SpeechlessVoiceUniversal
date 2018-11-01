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
}

public abstract class AudioPlayer : MonoBehaviour
{
    public abstract void Play(AudioClip audio);
}