using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LanguageDisplayer : MonoBehaviour {

    public Text m_englishText;
    public Text m_mothertongueLanguageText;
    public Text m_foreignLanguageText;
    public Image m_attachImage;
    public AudioSource m_audioSource;

    public void SetWith(MetaLanguageToDisplay display) {

    }
}


[System.Serializable]
public class LanguagePackage {

    public string m_packageName;

    internal void Add(string identity, string english, string native, LanguageType originalLanguage, string foreign, LanguageType foreignLanguage, string foreignAudio)
    {
      //  MetaLanguage meta = 
    }

    public List<MetaLanguage> m_availibleSentence;

    public MetaLanguage FindBasedOnId(string id, out MetaLanguage[] foundResult) {
        
        if (string.IsNullOrEmpty(id)) {
            foundResult = new MetaLanguage[0];
            return null;

        }
        foundResult = m_availibleSentence.Where(k => k.m_identifier == id).ToArray();
        if (foundResult.Length > 0)
            return foundResult[0];
        else return null;
    }

    internal MetaLanguage FindBasedOnId(string id)
    {
        MetaLanguage[] result;
        return FindBasedOnId(id, out result);
    }
}

[System.Serializable]
public class MetaLanguage
{
    public string m_identifier; //"Unamed_"+ UnityEngine.Random.Range(0,int.MaxValue);
    public MetaLanguageToDisplay m_defaultDisplayer = new MetaLanguageToDisplay();
    public List<MetaLanguageToDisplay> m_byLanguageDisplayInfo = new List<MetaLanguageToDisplay>();

    public MetaLanguageToDisplay GetByLanguage(LanguageType language) {
        MetaLanguageToDisplay meta = m_defaultDisplayer;
        MetaLanguageToDisplay metaLanguage = new MetaLanguageToDisplay();
        if (m_byLanguageDisplayInfo.Count <= 0) {
            return meta;
        }

        if (language == LanguageType.Unkown)
        {
            metaLanguage = m_byLanguageDisplayInfo[UnityEngine.Random.Range(0, m_byLanguageDisplayInfo.Count)];
        }
        else {
            List<MetaLanguageToDisplay> possibility = m_byLanguageDisplayInfo.Where(k => k.foreignLanguage.m_language == language).ToList() ;
            if (possibility.Count > 0)
                metaLanguage = possibility[0];


        }
        meta = Override(meta, metaLanguage);
        return meta;
    }

    private static MetaLanguageToDisplay Override(MetaLanguageToDisplay toOverride, MetaLanguageToDisplay with)
    {
        return MetaLanguageToDisplay.Override(toOverride,with);
    }

    internal MetaLanguageToDisplay FirstDisplayer()
    {
        if (m_byLanguageDisplayInfo == null || m_byLanguageDisplayInfo.Count == 0)
            return new MetaLanguageToDisplay();
        else return m_byLanguageDisplayInfo[0];
    }
    
}

[System.Serializable]
public struct MetaLanguageToDisplay
{
    public Sentence english ;
    public Sentence originalLanguage ;
    public Sentence foreignLanguage ;
    
    internal static MetaLanguageToDisplay Override(MetaLanguageToDisplay toOverride, MetaLanguageToDisplay with)
    {
        MetaLanguageToDisplay newMeta = new MetaLanguageToDisplay();
        newMeta.english = toOverride.foreignLanguage.Override(toOverride.english,with.english);
        newMeta.originalLanguage = toOverride.foreignLanguage.Override(toOverride.originalLanguage,with.originalLanguage);
        newMeta.foreignLanguage = toOverride.foreignLanguage.Override(toOverride.foreignLanguage,with.foreignLanguage);
        return newMeta;
    }

    internal string GetAnyImageName()
    {
        if (!string.IsNullOrEmpty(foreignLanguage.m_media.m_pictureName))
            return foreignLanguage.m_media.m_pictureName;
        if (!string.IsNullOrEmpty(originalLanguage.m_media.m_pictureName))
            return originalLanguage.m_media.m_pictureName;
        if (!string.IsNullOrEmpty(english.m_media.m_pictureName))
            return english.m_media.m_pictureName;
        return "";
    }

    internal string GetForeignAudio()
    {
       return foreignLanguage.m_media.m_audioName;
    }
}

[System.Serializable]
public struct Sentence {
    public string m_text;
    public string m_country;
    public LanguageType m_language ;
    public MediaData m_media;

    internal Sentence Override(Sentence original, Sentence newVersion)
    {
        if (!string.IsNullOrEmpty(newVersion.m_text))
            original.m_text = newVersion.m_text;
        if (!string.IsNullOrEmpty(newVersion.m_text))
            original.m_country = newVersion.m_country;
        if (!string.IsNullOrEmpty(newVersion.m_text))
            original.m_language = newVersion.m_language;
            original.m_media = MediaData.Override(original.m_media, newVersion.m_media);

        return original;
    }
}

[System.Serializable]
public struct MediaData {
    public string m_audioName;
    public bool m_focusOnCall;
    public string m_pictureName;

    public static MediaData Override(MediaData original, MediaData newVersion)
    {
        if (string.IsNullOrEmpty(original.m_audioName))
            original.m_audioName = newVersion.m_audioName;
        if (string.IsNullOrEmpty(original.m_audioName))
            original.m_pictureName = newVersion.m_pictureName;
        return original;
    }
    //public string m_videoName = "";
}

public abstract class MediaDataAccessor : MonoBehaviour {
    private static MediaDataAccessor m_accessorInScene;
    public static MediaDataAccessor GetMediaAccessInScene()
    {
        if (m_accessorInScene)
            return m_accessorInScene;

        return m_accessorInScene = GameObject.FindObjectOfType<MediaDataAccessor>();
    }

    public abstract AudioClip GetAudio(string mediaName);
    public abstract Texture2D GetPicture(string mediaName);
   // public abstract UnityEngine.Object GetVideo(string mediaName);
}

public enum LanguageType {
    Unkown,
    English,
    French,
    Arabic,
    Spanish,
    Portuguese,
    Russian,
    Japanese,
    Punjabi,
    German,
    Javanese,
    Bengali,
    Hindi,
    Wu,
    Malay,
    Telugu,
    Vietnamese,
    Korean,
    Marathi


}