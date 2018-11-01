using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class UI_MetaLanguageDisplayer : MonoBehaviour {

    [Header("Accessor")]

    public AudioPlayer m_audioPlayer;
    public ImageAccessor m_imageAccessor;
    public SoundAccessor m_soundAccessor;
    [Header("Text")]

    public Text m_identifier;
    public UI_Sentence m_english;
    public UI_Sentence m_original;
    public UI_Sentence m_foreign;


    [Header("Image")]
    public RawImage m_preview;
    public RawImage m_fullDisplay;
    public AspectRatioFitter m_ratioFitter;

    [Header("Debug")]
    public MetaLanguageToDisplay m_currently;

    public void Awake()
    {
        m_english.m_onSoundRequested.AddListener(CallSound);
        m_english.m_onImageRequested.AddListener(CallImage);
        m_original.m_onSoundRequested.AddListener(CallSound);
        m_original.m_onImageRequested.AddListener(CallImage);
        m_foreign.m_onSoundRequested.AddListener(CallSound);
        m_foreign.m_onImageRequested.AddListener(CallImage);
        
    }

    public void SetWith(MetaLanguage meta, LanguageType language) {
        if (meta == null)
            return;
        m_identifier.text = meta.m_identifier;
        MetaLanguageToDisplay display = m_currently = meta.GetByLanguage(language);
        m_english.SetWithSentence(display.english);
        m_original.SetWithSentence(display.originalLanguage);
        m_foreign.SetWithSentence(display.foreignLanguage);

        CallImage(display.GetAnyImageName());
        CallSound(display.GetForeignAudio());
        
    }

    public void CallSound(string name)
    {
        Debug.Log("Call Sound:" + name);
        AudioClip clip = m_soundAccessor.GetSound(name);
        if (clip == null)
            return;
        m_audioPlayer.Play(clip);
    }
    public void CallImage(string name)
    {
      Texture2D text=  m_imageAccessor.GetTexture(name);
        if (text == null)
            return;
        m_preview.texture = text;
        m_fullDisplay.texture = text;
        m_ratioFitter.aspectRatio = text.width / (float)text.height;

    }
}



[System.Serializable]
public class OnMediaRequested : UnityEvent<string>
{

}