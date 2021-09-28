using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour {

	// Use this for initialization
	void Start () {
		for(int i = 0; i< transform.childCount; i++)
        {
            transform.GetChild(i).tag = "Background";
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
