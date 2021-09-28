using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.localPosition.z > -120)
        {
            transform.localPosition += new Vector3(-0.05f, 0f, -0.6f);
            transform.localEulerAngles += new Vector3(-0.05f, 0f, 0f);
        }
        else
        {
            transform.localPosition = new Vector3(105f, 36.1f, 275f);
        }
    }
}
