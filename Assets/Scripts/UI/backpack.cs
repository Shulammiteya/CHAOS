using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class backpack : MonoBehaviour {
    public GameObject flag, flag_2, viking, bible, relic, moonStone;
    public RawImage curse, fadeOut, fadeOut_2;
    float count = 0.1f;
    public Light light;
    [SerializeField]
    RawImage a, b;
    private int p = 0;
    // Use this for initialization
    void Start()
    {
        fadeOut.color = Color.clear;
        fadeOut.enabled = false;
        fadeOut_2.enabled = false;
        curse.transform.localPosition = new Vector3(0, 400, 0);
        transform.position = new Vector3(-307, 18, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (flag_2 == null)
        {
            if (count > 45)
            {
                fadeOut_2.enabled = true;
                fadeOut_2.color = Color.Lerp(fadeOut_2.color, Color.black, Time.deltaTime);
                if (fadeOut_2.color.a > 0.99)
                    SceneManager.LoadScene(3);
            }
            else if (count > 15)
            {
                fadeOut.enabled = true;
                fadeOut.color = Color.Lerp(fadeOut.color, Color.black, 1.5f * Time.deltaTime);
                if (count <= 30)
                    fadeOut_2.color = fadeOut.color;
                if (count > 30)
                {
                    curse.transform.localPosition = new Vector3(0, 0, 0);
                    fadeOut_2.enabled = true;
                    fadeOut_2.color = Color.Lerp(fadeOut_2.color, Color.clear, 2 * Time.deltaTime);
                }
            }
            count = count + 0.1f;
            moonStone.transform.localPosition -= new Vector3(0.01f, 0, 0);
            moonStone.transform.localScale += new Vector3(0.005f * count, 0.005f * count, 0.005f * count);
            moonStone.transform.localEulerAngles += new Vector3(0, 0, count);
            light.range += 0.05f;
            light.color = Color.blue;
            light.intensity += 0.05f;
        }
        if (flag != null && viking.transform.position.z > 75)
        {
            viking.transform.position = new Vector3(-10.6f, 25.6f, 75.9f);
            p = 1;
            if (relic.transform.position.y > 28.0f)
            {
                if (bible.transform.position.y > 28.0f)
                {
                    bible.transform.position = new Vector3(bible.transform.position.x, bible.transform.position.y, bible.transform.position.z + 1 * Time.deltaTime);
                    relic.transform.position = new Vector3(relic.transform.position.x, relic.transform.position.y, relic.transform.position.z + 1 * Time.deltaTime);
                    if (relic.transform.position.z > 83)
                    {
                        Destroy(relic);
                        Destroy(bible);
                        Destroy(flag);
                    }
                }
                else
                {
                    b.texture = null;
                    bible.transform.position = new Vector3(bible.transform.position.x, bible.transform.position.y + 1 * Time.deltaTime, bible.transform.position.z);

                }
            }
            else
            {
                a.texture = null;
                relic.transform.position = new Vector3(relic.transform.position.x, relic.transform.position.y + 1 * Time.deltaTime, relic.transform.position.z);
            }
            
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (p == 0)
                {
                    p = 1;
                }
                else
                {
                    p = 0;
                }
            }
        }
        //Debug.Log(transform.position);
        if (p == 1)
        {
            if (transform.position.x <= 140)
                transform.position = new Vector3(transform.position.x + 600 * Time.deltaTime, transform.position.y, transform.position.z);
            if (transform.position.x != 140)
            {
                transform.position = new Vector3(140, 18, 0);
            }
        }
        if (p == 0)
        {
            if (transform.position.x >= -307)
                transform.position = new Vector3(transform.position.x - 500 * Time.deltaTime, transform.position.y + 1 * Time.deltaTime, transform.position.z);

        }
    }
}
