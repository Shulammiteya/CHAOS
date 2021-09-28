using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact1 : MonoBehaviour
{
    [SerializeField]
    GameObject frame, now;
    private GameObject s;
    private int temp = 0; //record the cube now is? orange 1 red 2 blue3
    private int record = 0;//record the direction now left1 up2 right3 down4
    private bool legal = true;
    private int[] check = new int[20];
    private new Vector3 pos;
    [SerializeField]
    GameObject flag;
    [SerializeField]
    GameObject[] cubes = new GameObject[42];
    GameObject a;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //謎題一
        if (flag.transform.position.x == 2 && transform.position.z < -33) 
        { 
            if (Input.GetKeyDown(KeyCode.R))
        {
            for(int i = 28; i <= 42; i++)
            {
                Destroy(cubes[i].gameObject);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rch;
            if (Physics.Raycast(ray, out rch))
            {
                if (rch.collider.gameObject.transform.tag == "orange")
                {

                    pos = rch.collider.gameObject.transform.position;
                    temp = 1;
                        Destroy(a);
                        a = new GameObject();
                    a = Instantiate(rch.collider.gameObject);
                    a.tag = "orange";
                    a.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                else if (rch.collider.gameObject.transform.tag == "red")
                {
                    pos = rch.collider.gameObject.transform.position;
                    temp = 2;
                        Destroy(a);
                        a = new GameObject();
                        a = Instantiate(rch.collider.gameObject);
                    a.tag = "red";
                    a.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                else if (rch.collider.gameObject.transform.tag == "blue")
                {
                    pos = rch.collider.gameObject.transform.position;
                    temp = 3;
                        Destroy(a);
                        a = new GameObject();
                        a = Instantiate(rch.collider.gameObject);
                    a.tag = "blue";
                    a.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                else
                {
                    Debug.Log("F");
                    temp = 0;
                }
            }
        }
        if (temp != 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                legal = true;
                for (int i = 0; i < 42; i++)
                {
                    if (cubes[i] == null)
                    {

                    }
                    else if (cubes[i].transform.position == new Vector3(pos.x + 0.5f, pos.y, pos.z))
                    {
                        legal = false;
                    }
                }
                if (legal == true)
                {
                    s = new GameObject();
                    s = Instantiate(a);
                    s.transform.position = new Vector3(pos.x + 0.5f, pos.y, pos.z);
                    pos = new Vector3(pos.x + 0.5f, pos.y, pos.z);
                    for (int i = 0; i < 42; i++)
                    {
                        if (cubes[i] == null)
                        {
                            cubes[i] = s;
                            break;
                        }
                    }
                    record = 1;
                }   
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                legal = true;
                for (int i = 0; i < 42; i++)
                {
                    if (cubes[i] == null)
                    {

                    }
                    else if (cubes[i].transform.position == new Vector3(pos.x - 0.5f, pos.y, pos.z))
                    {
                        legal = false;
                    }
                }
                if (legal == true)
                {
                    s = new GameObject();
                    s = Instantiate(a);
                    s.transform.position = new Vector3(pos.x - 0.5f, pos.y, pos.z);
                    pos = new Vector3(pos.x - 0.5f, pos.y, pos.z);
                    for (int i = 0; i < 42; i++)
                    {
                        if (cubes[i] == null)
                        {
                            cubes[i] = s;
                            break;
                        }
                    }
                    record = 3;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                legal = true;
                for (int i = 0; i < 42; i++)
                {
                    if (cubes[i] == null)
                    {

                    }
                    else if (cubes[i].transform.position == new Vector3(pos.x , pos.y + 0.5f, pos.z))
                    {
                        legal = false;
                    }
                }
                if (legal == true)
                {
                    s = new GameObject();
                    s = Instantiate(a);
                    s.transform.position = new Vector3(pos.x , pos.y + 0.5f, pos.z);
                    pos = new Vector3(pos.x , pos.y + 0.5f, pos.z);
                    for (int i = 0; i < 42; i++)
                    {
                        if (cubes[i] == null)
                        {
                            cubes[i] = s;
                            break;
                        }
                    }
                    record = 2;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                legal = true;
                for (int i = 0; i < 42; i++)
                {
                    if (cubes[i] == null)
                    {

                    }
                    else if (cubes[i].transform.position == new Vector3(pos.x, pos.y - 0.5f, pos.z))
                    {
                        legal = false;
                    }
                }
                if (legal == true)
                {
                    s = new GameObject();
                    s = Instantiate(a);
                    s.transform.position = new Vector3(pos.x, pos.y - 0.5f, pos.z);
                    pos = new Vector3(pos.x, pos.y - 0.5f, pos.z);
                    for (int i = 0; i < 42; i++)
                    {
                        if (cubes[i] == null)
                        {
                            cubes[i] = s;
                            break;
                        }
                    }
                    record = 4;
                }
            }
        }
        
            for (int i = 0; i < 20; i++)
            {
                check[i] = 0;
            }
            for (int i = 22; i < 42; i++)
            {
                if (cubes[i] == null)
                    break;
                else
                {
                    if (cubes[i].gameObject.transform.tag == "orange")
                    {

                        if (cubes[i].gameObject.transform.position.x == 4.5 && cubes[i].gameObject.transform.position.y == 3)
                        {
                            check[0] = 1;
                        }
                        else if (cubes[i].gameObject.transform.position.x == 4.5 && cubes[i].gameObject.transform.position.y == 2.5)
                        {
                            check[1] = 1;
                        }
                        else if (cubes[i].gameObject.transform.position.x == 4.5 && cubes[i].gameObject.transform.position.y == 2)
                        {
                            check[2] = 1;
                        }
                        else if (cubes[i].gameObject.transform.position.x == 4.5 && cubes[i].gameObject.transform.position.y == 1.5)
                        {
                            check[3] = 1;
                        }
                        else if (cubes[i].gameObject.transform.position.x == 4 && cubes[i].gameObject.transform.position.y == 1.5)
                        {
                            check[4] = 1;
                        }
                    }
                    else if (cubes[i].gameObject.transform.tag == "red")
                    {

                        if (cubes[i].gameObject.transform.position.x == 3.5 && cubes[i].gameObject.transform.position.y == 2.5)
                        {
                            check[5] = 1;
                        }
                        else if (cubes[i].gameObject.transform.position.x == 3.5 && cubes[i].gameObject.transform.position.y == 2)
                        {
                            check[6] = 1;
                        }
                        else if (cubes[i].gameObject.transform.position.x == 3.5 && cubes[i].gameObject.transform.position.y == 1.5)
                        {
                            check[7] = 1;
                        }
                        else if (cubes[i].gameObject.transform.position.x == 3.5 && cubes[i].gameObject.transform.position.y == 1)
                        {
                            check[8] = 1;
                        }
                        else if (cubes[i].gameObject.transform.position.x == 4 && cubes[i].gameObject.transform.position.y == 1)
                        {
                            check[9] = 1;
                        }
                        else if (cubes[i].gameObject.transform.position.x == 4.5 && cubes[i].gameObject.transform.position.y == 1)
                        {
                            check[10] = 1;
                        }
                    }
                    else if (cubes[i].gameObject.transform.tag == "blue")
                    {

                        if (cubes[i].gameObject.transform.position.x == 4 && cubes[i].gameObject.transform.position.y == 2)
                        {
                            check[11] = 1;
                        }
                        else if (cubes[i].gameObject.transform.position.x == 4 && cubes[i].gameObject.transform.position.y == 2.5)
                        {
                            check[12] = 1;
                        }
                        else if (cubes[i].gameObject.transform.position.x == 4 && cubes[i].gameObject.transform.position.y == 3)
                        {
                            check[13] = 1;
                        }
                        else if (cubes[i].gameObject.transform.position.x == 3.5 && cubes[i].gameObject.transform.position.y == 3)
                        {
                            check[14] = 1;
                        }
                        else if (cubes[i].gameObject.transform.position.x == 3 && cubes[i].gameObject.transform.position.y == 3)
                        {
                            check[15] = 1;
                        }
                        else if (cubes[i].gameObject.transform.position.x == 3 && cubes[i].gameObject.transform.position.y == 2.5)
                        {
                            check[16] = 1;
                        }
                        else if (cubes[i].gameObject.transform.position.x == 3 && cubes[i].gameObject.transform.position.y == 2)
                        {
                            check[17] = 1;
                        }
                        else if (cubes[i].gameObject.transform.position.x == 3 && cubes[i].gameObject.transform.position.y == 1.5)
                        {
                            check[18] = 1;
                        }
                        else if (cubes[i].gameObject.transform.position.x == 3 && cubes[i].gameObject.transform.position.y == 1)
                        {
                            check[19] = 1;
                        }
                    }
                }

            }
            for (int i = 0; i < 20; i++)
            {
                if (check[i] == 1)
                {
                    check[0]++;
                }
            }
            if (check[0] == 21)
            {
                flag.transform.position = new Vector3(1.8f, flag.transform.position.y, flag.transform.position.z);
                now.transform.position = new Vector3(now.transform.position.x, -5, -55);
                for (int i = 0; i < 42; i++)
                {
                    Destroy(cubes[i]);
                }
                Destroy(a);
                frame.transform.position = new Vector3(-2, 0.5f, -48);
            }
        }
      
       
        //謎題一
            
    }
}

