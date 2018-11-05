using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundAccessorDefault : SoundAccessor
{
    public AudioClip m_noAudio;
    public List<AccessibleSound> m_sounds= new List<AccessibleSound>();

    [System.Serializable]
    public struct AccessibleSound {
        public string id;
        public AudioClip audio;

    }

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public AudioClip m_clipLoaded;
    public override IEnumerator GetSound(string soundName)
    {
        AudioClip result = m_noAudio;
        AudioClip [] results = m_sounds.Where(k => k.id == soundName).Select(k=>k.audio).ToArray();
        if (results.Length > 0)
        {
            result = results[0];
            m_clipLoaded = result;
        }
        else m_clipLoaded = null;
        yield return new WaitForEndOfFrame();

    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override AudioClip GetLoadedsound()
    {
        return m_clipLoaded;
    }
}


public abstract class SoundAccessor : MonoBehaviour
{
    public abstract IEnumerator GetSound(string soundName );
    public abstract AudioClip GetLoadedsound();
}
