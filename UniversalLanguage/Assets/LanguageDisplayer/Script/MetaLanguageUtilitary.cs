using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MetaLanguageUtilitary : MonoBehaviour {

    public UI_MetaLanguageDisplayer m_displayer;
    [Header("Package")]
    public LanguagePackageMono m_selectedPackage;
    public List<LanguagePackageMono> m_packages = new List<LanguagePackageMono>();
    public LanguageType m_foreignLanguage;


    public void StopAllAudios() {
        m_displayer.StopAllSounds();
    }
    internal string[] GetValideIdenities()
    {
        if (m_selectedPackage == null)
            return new string[0];
        return m_selectedPackage.m_package.m_availibleSentence.Select(k => k.m_identifier).ToArray();
    }

    public void SwitchPackage(LanguagePackageMono package)
    {
        m_selectedPackage = package;
    }
    public void SwitchPackage(string packageName)
    {
        List<LanguagePackageMono> packageResult = m_packages.Where(k=>k.m_package.m_packageName==packageName).ToList();
        if (packageResult.Count > 0)
            m_selectedPackage = packageResult[0];
    }
    public void DisplayMetaLanguage(string metaId)
    {
        MetaLanguage meta  = m_selectedPackage.m_package.FindBasedOnId(metaId);
        m_displayer.SetWith(meta, m_foreignLanguage);

    }

    public void SwitchTargetLanguage(LanguageType targetLanguage)
    {
        m_foreignLanguage = targetLanguage;
    }
    public void SwitchTargetLanguage(string language)
    {
        try
        {

            LanguageType choosed = ParseEnum<LanguageType>(language);
            SwitchTargetLanguage(choosed);

        }
        catch (Exception e) { }
    }

    public static T ParseEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }


}
