using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FileStorageAccess : MonoBehaviour {


    public enum StorageType { Private, Public }

    [SerializeField]
    string m_publicPath = "";

    [SerializeField]
    string m_privatePath = "";

    public Text m_debugPath;
    public string m_directoryName = "SpeechlessVoice";
    public void Awake()
    {
        
     
        m_publicPath = GetStorageDirectory(StorageType.Public, m_directoryName);
        m_privatePath = GetStorageDirectory(StorageType.Private, m_directoryName);
        if(m_debugPath!=null)
            m_debugPath.text =  m_publicPath + "  ||   " + m_privatePath;

        if (!Directory.Exists(m_privatePath))
        {
            Directory.CreateDirectory(m_privatePath);
            File.CreateText(m_privatePath + "/TestPrivate1.txt");

        }
        if (!Directory.Exists(m_privatePath)) {
            Directory.CreateDirectory(m_publicPath);
            File.CreateText(m_publicPath + "/TestPublic1.txt");

        }
    }

    public string GetDirectoryPath(StorageType storageType)
    {
      return   GetStorageDirectory(storageType, m_directoryName);
    }

    public string GetFilePath(StorageType storageType, string filenamewithoutExtension, string extension="txt")
    {
        if (extension == null || extension.Length < 2)
            throw new Exception("Not valide extension:"+ filenamewithoutExtension+"."+extension);

        if (extension[0] == '.')
            extension = extension.Substring(1);

        return GetFilePath(storageType, filenamewithoutExtension + '.' + extension);
    }

    public string GetFilePath(StorageType storageType, string filenameWithExtention)
    {
        if (filenameWithExtention == null || filenameWithExtention.Length < 3)
            throw new Exception("Not valide extension:"+ filenameWithExtention);
        if (filenameWithExtention[0] == '/')
            filenameWithExtention = filenameWithExtention.Substring(1);

        return GetStorageDirectory(storageType,m_directoryName) + "/" + filenameWithExtention;
    }

        public static string GetStorageDirectory(StorageType storageType, string subDirectory) {

        string path = "";

        #if UNITY_EDITOR
            path = storageType==StorageType.Public?  Application.dataPath+"/../"+ subDirectory : Application.persistentDataPath + "/" + subDirectory;

#else
#if UNITY_ANDROID
        // NEED TO BE CHECKED
        path = storageType == StorageType.Public ? "/storage/emulated/0/" + subDirectory : Application.persistentDataPath+ "/" + subDirectory;

#endif
#if UNITY_STANDALONE
        path = storageType == StorageType.Public ? Application.dataPath+ "/../" + subDirectory : Application.persistentDataPath+ "/" + subDirectory;

#endif

#endif





        return path;
    }
    



    private string GetAndroidContextExternalFilesDir()
    {
        string path = "";

        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                using (AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    using (AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        path = ajo.Call<AndroidJavaObject>("getExternalFilesDir", null).Call<string>("getAbsolutePath");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error fetching native Android external storage dir: " + e.Message);
            }
        }
        return path;
    }

    private String GetAndroidContextInternalFilesDir()
    {
        string path = "";

        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                using (AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    using (AndroidJavaObject ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        path = ajo.Call<AndroidJavaObject>("getFilesDir").Call<string>("getAbsolutePath");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error fetching native Android internal storage dir: " + e.Message);
            }
        }
        return path;
    }


    //private static string GetAndroidExternalFilesDir()
    //{
    //    using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
    //    {
    //        using (AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
    //        {
    //            // Get all available external file directories (emulated and sdCards)
    //            AndroidJavaObject[] externalFilesDirectories = context.Call<AndroidJavaObject[]>("getExternalFilesDirs", null);
    //            AndroidJavaObject emulated = null;
    //            AndroidJavaObject sdCard = null;

    //            for (int i = 0; i < externalFilesDirectories.Length; i++)
    //            {
    //                AndroidJavaObject directory = externalFilesDirectories[i];
    //                using (AndroidJavaClass environment = new AndroidJavaClass("android.os.Environment"))
    //                {
    //                    // Check which one is the emulated and which the sdCard.
    //                    bool isRemovable = environment.CallStatic < bool("isExternalStorageRemovable", directory);
    //                    bool isEmulated = environment.CallStatic<bool>("isExternalStorageEmulated", directory);
    //                    if (isEmulated)
    //                        emulated = directory;
    //                    else if (isRemovable && isEmulated == false)
    //                        sdCard = directory;
    //                }
    //            }
    //            // Return the sdCard if available
    //            if (sdCard != null)
    //                return sdCard.Call<string>("getAbsolutePath");
    //            else
    //                return emulated.Call<string>("getAbsolutePath");
    //        }
    //    }
    //}
}
