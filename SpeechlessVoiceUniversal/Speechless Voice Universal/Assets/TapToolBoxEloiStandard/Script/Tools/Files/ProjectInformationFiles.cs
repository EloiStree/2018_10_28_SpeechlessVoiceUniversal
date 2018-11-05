using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProjectInformationFiles : MonoBehaviour {

    public FileStorageAccess m_fileStorage;
    public FileToCreate[] m_assetsAttachToProject = new FileToCreate[0];

    [System.Serializable]
    public struct FileToCreate {
        public string m_filename;
        public TextAsset m_textAsset;
    }

	// Use this for initialization
	void Awake () {
        for (int i = 0; i < m_assetsAttachToProject.Length; i++)
        {
            if (m_assetsAttachToProject[i].m_textAsset != null)
            {
                CreateFile(i, FileStorageAccess.StorageType.Public);
                CreateFile(i, FileStorageAccess.StorageType.Private);

            }
        }

	}

    private void CreateFile(int index, FileStorageAccess.StorageType type)
    {
        string path = m_fileStorage.GetFilePath(type, m_assetsAttachToProject[index].m_filename);
        File.WriteAllText(path, m_assetsAttachToProject[index].m_textAsset.text);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
