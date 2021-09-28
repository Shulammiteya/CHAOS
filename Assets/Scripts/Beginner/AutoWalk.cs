using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoWalk : MonoBehaviour {

    Rigidbody rid;

	// Use this for initialization
	void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y > 58)
        {
            SceneManager.LoadScene(2);
        }
        else if (transform.localPosition.y > 45)  //進石洞
        {
            transform.localPosition += new Vector3(0.04f, -0.06f, -0.05f);
        }
        else if (transform.localPosition.y > 36)  //階梯向左上走
        {
            transform.localPosition += new Vector3(0.19f, 0.021f, 0f);
        }
        if (transform.localPosition.x < 0)  //階梯向右上走
        {
            transform.localPosition += new Vector3(-0.1f, 0.12f, -0.21f);
            return;
        }
        else if (transform.localPosition.x > 5)  //平地向右走
        {
            transform.localPosition += new Vector3(-0.6f, 0f, 0.075f);
        }
        else
        {
            transform.localPosition = new Vector3(-8, 21, 65);
            transform.localRotation = new Quaternion(0, -180, 0, 0);
        }
    }
}
