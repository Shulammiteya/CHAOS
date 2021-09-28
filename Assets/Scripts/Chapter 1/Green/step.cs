using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class step : MonoBehaviour {
    [SerializeField]
    GameObject door,flag;
    [SerializeField]
    GameObject[] floor = new GameObject[9];
    private float clock, temp;
    private bool start = false;
    private bool save = false;
    private int a1, a2;
    
    [SerializeField]
    Text text;
    // Use this for initialization
    void Start () {
		
	}
    void OnCollisionStay(Collision c)
    {
        if (flag.transform.position.y>4&&flag.transform.position.y<8)
        {
            if ((2 - clock + temp) >= 0)
            {
                if ((c.gameObject == floor[a1] ))
                {
                    save = true;
                }
                else
                {
                    save = false;
                }
            }
        }
        else
        {
            if ((2 - clock + temp) >= 0)
            {
                if ((c.gameObject == floor[a1] || c.gameObject == floor[a2]))
                {
                    save = true;
                }
                else
                {
                    save = false;
                }
            }
        }
            
    }
    // Update is called once per frame
    void Update () {
        Debug.Log(flag.transform.position);
        clock = clock +(1 * Time.deltaTime);
        if (flag.transform.position.y >= 1 && flag.transform.position.y < 8)
        {
            flag.transform.position = new Vector3(flag.transform.position.x, flag.transform.position.y + 0.1f * Time.deltaTime, flag.transform.position.z);
            if (flag.transform.position.y <= 4)
            {
                
                if (start == false)
                {
                    temp = clock;
                    a1 = Random.Range(0, 9);
                    a2 = 0;
                    while (a2==a1)
                    {
                        a2 = Random.Range(0, 9);
                    }
                    
                    floor[a1].gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
                    floor[a2].gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
                    start = true;
                }
                else
                {
                    if ((2 - clock + temp) > 0)
                    {
                        text.text = (2 - clock + temp).ToString();
                    }
                    else
                    {

                        if (save == true)
                        {
                            text.text = "save";
                        }
                        else
                        {

                            temp = clock;
                            text.text = "dead";
                            flag.transform.position = new Vector3(flag.transform.position.x, -1, flag.transform.position.z);
                            transform.position = new Vector3(4, 0.3f, -0.5f);
                            floor[a1].gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                            floor[a2].gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                            start = false;
                        }
                    }
                    if (clock - temp >= 5)
                    {
                        start = false;
                        floor[a1].gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                        floor[a2].gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                    }
                }
            }
            else
            {
                if (a2 >= 0)
                {
                    floor[a2].gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                }
                a2 = -1;
                if (start == false)
                {
                    temp = clock;
                    a1 = Random.Range(0, 9);
                    floor[a1].gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
                    start = true;
                }
                else
                {
                    if ((2 - clock + temp) > 0)
                    {
                        text.text = (2 - clock + temp).ToString();
                    }
                    else
                    {

                        if (save == true)
                        {
                            text.text = "save";
                        }
                        else
                        {

                            temp = clock;
                            text.text = "dead";
                            flag.transform.position = new Vector3(flag.transform.position.x, -1, flag.transform.position.z);
                            transform.position = new Vector3(4, 0.3f, -0.5f);
                            floor[a1].gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                            start = false;
                        }
                    }
                    if (clock - temp >= 5)
                    {
                        start = false;
                        floor[a1].gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                    }
                }
            }
            
        }
        else if (flag.transform.position.y == 0)
        {
            door.transform.position = new Vector3(door.transform.position.x, 5, door.transform.position.z);

        }
        else if(flag.transform.position.y>=8&&flag.transform.position.y<9)
        {
            flag.transform.position = new Vector3(flag.transform.position.x,9, flag.transform.position.z);
            
        }
        else if (flag.transform.position.y >= 9 && flag.transform.position.y < 11)
        {
            if (door.transform.position.y < 5)
            {
                door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + 0.5f * Time.deltaTime, door.transform.position.z);
            }
            else
            {
                door.transform.position = new Vector3(door.transform.position.x, 5, door.transform.position.z);
            }
        }
            if (clock - temp > 5)
        {
            text.text = "";
        }
    }
    
}
