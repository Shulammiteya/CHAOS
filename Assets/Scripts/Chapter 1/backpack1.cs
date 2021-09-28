using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backpack1 : MonoBehaviour {
    private Vector3 pos;
    private int p = 0;
    // Use this for initialization
    void Start () {
        pos = transform.position;
        transform.position = new Vector3(-307, 18, 0);
    }
	
	// Update is called once per frame
	void Update () {
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
        if (p == 1)
        {
            if (transform.position.x <= pos.x)
                transform.position = new Vector3(transform.position.x + 600 * Time.deltaTime, transform.position.y, transform.position.z);
            if (transform.position.x != pos.x)
            {
                transform.position = pos;
            }
        }
        if (p == 0)
        {
            if (transform.position.x >= -307)
                transform.position = new Vector3(transform.position.x - 500 * Time.deltaTime, transform.position.y + 1 * Time.deltaTime, transform.position.z);

        }
    }
}
