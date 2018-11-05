using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SoundAccessorFiles : SoundAccessor {

     public AudioClip m_noAudio;
    public FileStorageAccess m_filesAccessor;

    [Header("Debug")]
    public string[] m_debug;
    public string[] m_valide;

    public AudioClip m_clipLoaded;

    public override AudioClip GetLoadedsound()
    {
        return m_clipLoaded;
    }

    public override IEnumerator GetSound(string soundName)
    {
        m_clipLoaded = null;
        if (soundName == null)
            yield break;
        string directoryPathPublic = GetPublicPath();
        string[] paths = Directory.GetFiles(directoryPathPublic);
        for (int i = 0; i < paths.Length; i++)
        {
            int indexSlash = paths[i].LastIndexOf('/');
            int indexBackslash = paths[i].LastIndexOf('\\');
            if (indexSlash > indexBackslash && indexSlash >= 0)
                paths[i] = paths[i].Substring(indexSlash+1);
            else if (indexSlash < indexBackslash && indexBackslash >= 0)
                paths[i] = paths[i].Substring(indexBackslash+1);
      


        }
        //Debug.Log("Word:" + soundName);

        string[] validePaths = m_valide = paths.Where(k => k.IndexOf(soundName) == 0 && IsAudio(k)).ToArray();
        m_debug = paths;
        if (validePaths.Length > 0)
        {
            string path = GetPublicPath() +"/"+ validePaths[0];
            //Debug.Log("Path: " +path);
            WWW www = new WWW("file:///"+path);
            yield return www;

            m_clipLoaded = www.GetAudioClip();
            
        }

        yield break;
    }

    private string GetPublicPath()
    {
        return m_filesAccessor.GetDirectoryPath(FileStorageAccess.StorageType.Public);
    }

    private bool IsAudio(string k)
    {
        if (k.Length <= 4)
            return false;
        return k.ToLower().IndexOf(".mp3") == k.Length - 4
        || k.ToLower().IndexOf(".wav") == k.Length - 4
        || k.ToLower().IndexOf(".ogg") == k.Length - 4;
    }

    // Use this for initialization
    void Start() {
        string directoryPathPublic = GetPublicPath();
        m_debug = Directory.GetFiles(directoryPathPublic);
    }
}
