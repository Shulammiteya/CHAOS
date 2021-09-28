using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Music : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        Debug.developerConsoleVisible = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
