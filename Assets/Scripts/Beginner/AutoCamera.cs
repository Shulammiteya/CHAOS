using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoCamera : MonoBehaviour {

    public RawImage story;
    bool end = false;
    public AudioClip walk;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        story.transform.localPosition = new Vector3(0, 300, 0);
        audioSource = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
        if (end)
        {
            story.color = Color.Lerp(story.color, Color.clear, 1f * Time.deltaTime);
            if (story.color.a < 0.1f)
            {
                story.color = Color.clear;
                end = false;
            }
        }
        if (transform.localPosition.z < 10)    //順序5
        {
            return;
        }
        if (transform.localPosition.x < -10)  //順序4
        {
            end = true;
            if (audioSource.clip != walk)
            {
                audioSource.clip = walk;
                audioSource.Play();
            }
            transform.localPosition += new Vector3(-0.05f, 0f, -1f);
            transform.localEulerAngles += new Vector3(-0.16f, 0f, 0f);
            return;
        }
        else if (transform.localPosition.x > 130)  //順序1
        {
            transform.localPosition += new Vector3(-0.5f, 0f, 0f);
            transform.localEulerAngles += new Vector3(-0.16f, 0f, 0f);
        }
        else if (transform.localPosition.x > 0)  //順序2
        {
            story.transform.localPosition = new Vector3(0, -110, 0);
            transform.localPosition += new Vector3(-0.5f, 0f, 0f);
            transform.localEulerAngles += new Vector3(-0.16f, 0f, 0f);
        }
        else    //順序3
        {
            transform.localPosition += new Vector3(-0.05f, 0f, 0f);
            transform.localEulerAngles += new Vector3(-0.13f, 0f, 0f);
        }
    }
}
