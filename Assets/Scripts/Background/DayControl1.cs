using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayControl1 : MonoBehaviour {
    private int a = 0; //0==night 1==morning
    private int s = 0; // 0=time passs 1== stop
    private float b = 0;
    private int m = 0; //0=no monster 1=monsters
    public GameObject c;
    private int num;
    private GameObject[] monster;
    // Use this for initialization
    void Start () {
        num = Random.Range(1, 100);
        monster = new GameObject[num];
    }
	
	// Update is called once per frame
	void Update () {
        if (a == 0 && m == 0)
        {
            for (int i = 0; i < num; i++)
            {
                monster[i] = Instantiate(c);
                monster[i].transform.tag = "Enemy";
                monster[i].transform.position = new Vector3(Random.Range(-445.0f, 640.0f), -25f, Random.Range(-450.0f, 630.0f));
            }
            m = 1;
            Debug.Log(" Create");
        }
        if (a == 1&&m==1)
        {
            for (int i = 0; i < num; i++)
            {
                Destroy(monster[i]);
            }
            Debug.Log(" DESTROY");
            m = 0;
        }
        if (s==0)
        {
            b = b + 0.1f;
        }
        
        if (b == 360)
        {
            b = 0;
        }
        if (b < 180)
        {
            a = 1;
        }
        else
        {
            a = 0;
        }
        transform.localEulerAngles = new Vector3(transform.localRotation.x + b, transform.localRotation.y, transform.localRotation.z);
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (a == 1)
            {
                
                a = 0;
                b = 180;
            }
            else
            {
                
                a = 1;
                b = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (s == 1)
            {
                s = 0;
            }
            else
            {
                s = 1;
            }
        }
        
	}
}
