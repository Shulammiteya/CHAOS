using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Button_Start : MonoBehaviour, IPointerClickHandler
{
    AudioSource audioSourse;
    public RawImage rawImage;
    bool fade = false;

    // Use this for initialization
    void Start()
    {
        audioSourse = GetComponent<AudioSource>();
        rawImage.color = Color.clear;
        rawImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(fade)
        {
            rawImage.enabled = true;
            rawImage.color = Color.Lerp(rawImage.color, Color.black, 1.5f * Time.deltaTime);
            if (rawImage.color.a > 0.9f)
            {
                rawImage.color = Color.black;
                SceneManager.LoadScene(1);
            }
        }
    }

    public void OnPointerClick(PointerEventData e)
    {
        audioSourse.Play();
        fade = true;
    }
}
