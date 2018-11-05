using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour {

    public float m_timeToDestroy=10;
	void Start () {
        Destroy( this.gameObject, m_timeToDestroy);
	}
	
}
