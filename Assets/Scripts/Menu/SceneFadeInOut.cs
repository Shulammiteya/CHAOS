using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFadeInOut : MonoBehaviour {
    
    public bool start = true;
    private RawImage rawImage;

    // Use this for initialization
    void Start () {
        rawImage = GetComponent<RawImage>();
    }
	
	// Update is called once per frame
	void Update () {
        if (start)
        {
            rawImage.color = Color.Lerp(rawImage.color, Color.clear, 0.5f * Time.deltaTime);
            if (rawImage.color.a < 0.4f)
            {
                rawImage.color = Color.clear;
                rawImage.enabled = false;
                start = false;
            }
        }
    }
}
