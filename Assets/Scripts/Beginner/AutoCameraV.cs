using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AutoCameraV : MonoBehaviour {

    public RawImage story;
    bool end = false, disappear = false, disappear_stoty = false;
    public AudioClip walk;
    AudioSource audioSource;
    public RawImage rawImage;
    public Text te1, te2;

    // Use this for initialization
    void Start()
    {
        story.transform.localPosition = new Vector3(0, 300, 0);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (disappear)
        {
            SceneManager.LoadScene(2);
        }
        if (disappear_stoty)
        {
            story.color = Color.Lerp(story.color, Color.clear, 1f * Time.deltaTime);
            if (story.color.a < 0.5f)
            {
                te1.transform.localPosition += new Vector3(0, 400, 0);
                te2.transform.localPosition += new Vector3(0, 400, 0);
            }
        }
        if (end)
        {
            rawImage.enabled = true;
            rawImage.color = Color.Lerp(rawImage.color, Color.black, 1f * Time.deltaTime);
            if (audioSource.clip != walk)
            {
                audioSource.clip = walk;
                audioSource.Play();
            }
        }
        StartCoroutine(Story());
    }

    private IEnumerator Story()
    {
        yield return new WaitForSeconds(3f);
        story.transform.localPosition = new Vector3(0, -110, 0);
        yield return new WaitForSeconds(10f);
        disappear_stoty = true;
        yield return new WaitForSeconds(2f);
        end = true;
        rawImage.color = Color.black;
        rawImage.enabled = false;
        yield return new WaitForSeconds(1.5f);
        disappear = true;
    }
}
