using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SoundLibrary : MonoBehaviour {

    public AudioClip m_clip;
    public AudioSource m_audioSource;
    public InputField m_fileName;
    public FileStorageAccess m_storePath;
    public int m_maxTime=45;

    public static string m_directoryPathWindow="";
    public  string DirectoryPath { get {
            if (m_directoryPathWindow==null || m_directoryPathWindow.Length <= 0)
                m_directoryPathWindow = m_storePath.GetDirectoryPath(FileStorageAccess.StorageType.Public);
            return m_directoryPathWindow ; }
    }
  

    public void StartRecord()
    {
        Debug.Log("Start");
        m_clip = Microphone.Start(null, false, m_maxTime, 44100);
    }

    public void StopRecord()
    {
        Debug.Log("stop "+ m_fileName.text);
        StopRecord(RemoveExtention(m_fileName.text));
    }

    private string RemoveExtention(string text)
    {
        text = text.Replace(".wav","");
        text = text.Replace(".mp3","");
        return text;
    }

    public void StopRecord (string fileName) {
        Microphone.End(null);
        if(m_clip!=null)
            SavWav.Save(fileName!=""?fileName: GetTimestamp(DateTime.Now)+".wav", m_clip, DirectoryPath);

    }

    public void PlayLastRecord() {
        m_audioSource.clip = m_clip;
        m_audioSource.Play();
    }
    

    public string[] GetFilesNameInFolder() {

       //string [] pathFiles = Directory.GetFiles(DirectoryPath);
       // if (pathFiles.Length > 0)
       // {
       //     for (int i = 0; i < pathFiles.Length; i++)
       //     {
       //         Debug.Log(">:" + pathFiles[i]);
       //         pathFiles[i] = Path.GetFileName(pathFiles[i]);
       //     }
       // }
       // else return new string[0];

        return   Directory
                .GetFiles(DirectoryPath, "*", SearchOption.AllDirectories)
                .Select(f => Path.GetFileName(f)).ToArray();
     
    }

    public void PlayInFolder(string fileName) {

        StartCoroutine(LaunchAudio(fileName));
      
    }

    private IEnumerator LaunchAudio(string fileName)
    {
        Debug.Log("Hey:Audio: "+ fileName);
        if (!string.IsNullOrEmpty(fileName)) {
            
            Debug.Log(DirectoryPath + "/" + fileName);
            string path =  DirectoryPath + "/" + fileName;
            if (File.Exists(path)) {

                Debug.Log("Hey:dddd: " + fileName);
                WWW www;

                www = new WWW("file:///" +path);
                yield return www;

                AudioClip audio = www.GetAudioClip();
                if (audio != null)
                {
                    m_audioSource.clip = audio;
                    m_audioSource.Play();
                }

            }


        }
    }
    

    public static String GetTimestamp(DateTime value)
    {
        return value.ToString("yyyyMMddHHmmssffff");
    }


}
