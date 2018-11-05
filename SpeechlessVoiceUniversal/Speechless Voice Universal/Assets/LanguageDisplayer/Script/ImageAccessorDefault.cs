using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ImageAccessorDefault : ImageAccessor
{
    public List<AccessibleImage> m_images = new List<AccessibleImage>();

    [System.Serializable]
    public struct AccessibleImage
    {
        public string id;
        public Texture2D image;

    }
    public override Texture2D GetTexture(string imageName)
    {
        Texture2D result = new Texture2D(64, 64);
        for (int i = 0; i < 64 ; i++)
            for (int j = 0; j < 64; j++)
                result.SetPixel(i, j, new Color(GetRandom(), GetRandom(), GetRandom(), 1));
        result.Apply();


        Texture2D[] results = m_images.Where(k => k.id == imageName).Select(k => k.image).ToArray();
        if (results.Length > 0)
        {
            result = results[0];
        }
        return result;
    }

    public float GetRandom() {
        return UnityEngine.Random.Range(0f, 1f);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public abstract class ImageAccessor : MonoBehaviour {

    public abstract Texture2D GetTexture(string imageName);
}