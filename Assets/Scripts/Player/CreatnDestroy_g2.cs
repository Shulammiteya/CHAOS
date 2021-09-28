using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreatnDestroy_g2 : MonoBehaviour {
    public Text[] text = new Text[4];
    public GameObject brick, coarse_dirt, glass, gold_ore, grass, lamp, log, stone;
    [SerializeField]
    Texture Brick, Coarse_dirt, Glass, Gold_ore, Grass, Lamp, Log, Stone;
    [SerializeField]
    RawImage i1, i2, i3, i4;
    private int[] icount = new int[4];
    private int[] item = new int[4]; //0=brick 1=dirt 2=glass 3=gold 4=grass 5=lamp 6=log 7=stone 8=none
    private RawImage[] bag = new RawImage[4];
    //Hsin
    private Rigidbody rigidBody;
    //Hsin
    // Use this for initialization
    void Start () {
        bag[0] = i1;
        bag[1] = i2;
        bag[2] = i3;
        bag[3] = i4;
        for(int i = 0; i < 4; i++)
        {
            item[i] = 8;
            icount[i] = 0;
            text[i].text = "";
        }
        item[0] = 1;
        item[1] = 7;
        item[2] = 3;
        item[3] = 5;
        icount[0] = 1;
        icount[1] = 1;
        icount[2] = 1;
        icount[3] = 1;
        rigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
            item[0] = 8;
            icount[0] = 0;
        }
        
        //refreshing text and empty item
        for (int i = 0; i < 4; i++)
        {
            if (icount[i] == 0)
            {
                item[i] = 8;
                text[i].text = "";
            }
            else 
            {
                text[i].text = icount[i].ToString();
            }
        }
        //scorlling backpack
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            int temp = 0 ,temp2=0;
            temp2 = icount[0];
            icount[0] = icount[1];
            icount[1] = icount[2];
            icount[2] = icount[3];
            icount[3] = temp2;
            temp = item[0];
            item[0] = item[1];
            item[1] = item[2];
            item[2] = item[3];
            item[3] = temp;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            int temp = 0, temp2 = 0;
            temp2 = icount[3];
            icount[3] = icount[2];
            icount[2] = icount[1];
            icount[1] = icount[0];
            icount[0] = temp2;
            temp = item[3];
            item[3] = item[2];
            item[2] = item[1];
            item[1] = item[0];
            item[0] = temp;
        }
        //refreshing texture
        for(int i = 0; i < 4; i++)
        {
            
            if (item[i] == 0)
            {
                bag[i].texture = Brick;
            }
            else if (item[i] == 1)
            {
                bag[i].texture = Coarse_dirt;
            }
            else if (item[i] == 2)
            {
                bag[i].texture = Glass;
            }
            else if (item[i] == 3)
            {
                bag[i].texture = Gold_ore;
            }
            else if (item[i] == 4)
            {
                bag[i].texture = Grass;
            }
            else if (item[i] == 5)
            {
                bag[i].texture = Lamp;
            }
            else if (item[i] == 6)
            {
                bag[i].texture = Log;
            }
            else if (item[i] == 7)
            {
                bag[i].texture = Stone;
            }
            else if (item[i] == 8)
            {
                bag[i].texture = null;
            }

        }
        //sorting v2
        int count = 0;
        for(int i = 0; i < 4; i++)
        {
            if (icount[i] > 0)
            {
                count++;
            }
        }
        for(int i = count; i < 4; i++)
        {
            if (icount[i] > 0&&icount[0]==0)
            {
                icount[0] = icount[i];
                item[0] = item[i];
                item[i] = 0;
                icount[i] = 0;
            }
        }
        //sorting
        if (icount[0] == 0)
        {
            for (int i = 1; i < 4; i++)
            {
                if (icount[i] > 0)
                {
                    icount[0] = icount[i];
                    icount[i] = 0;
                    item[0] = item[i];
                    item[i] = 8;
                    break;
                }
            }
        }
        if (icount[1] == 0)
        {
            for (int i = 2; i < 4; i++)
            {
                if (icount[i] > 0)
                {
                    icount[1] = icount[i];
                    icount[i] = 0;
                    item[1] = item[i];
                    item[i] = 8;
                    break;
                }
            }
        }
        if (icount[2] == 0)
        {
            if (icount[3] > 0)
            {
                icount[2] = icount[3];
                icount[3] = 0;
                item[2] = item[3];
                item[3] = 8;
            }
        }
        //get the cube
        if (Input.GetMouseButtonDown(0))//0=brick 1=dirt 2=glass 3=gold_ore 4=grass 5=lamp 6=log 7=stone 8=none
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {

                Debug.Log(raycastHit.collider.gameObject.transform.tag);
                //Debug.Log(transform.localPosition);
                //Debug.Log(raycastHit.point);
                //Debug.Log("this "+Mathf.Abs(raycastHit.point.x-transform.localPosition.x)+" "+ Mathf.Abs(raycastHit.point.y - transform.localPosition.y)+ " " + Mathf.Abs(raycastHit.point.z - transform.localPosition.z));
                if (Mathf.Abs(raycastHit.point.x - transform.localPosition.x)<2&& Mathf.Abs(raycastHit.point.y - transform.localPosition.y) < 2 && Mathf.Abs(raycastHit.point.z - transform.localPosition.z) < 2 )
                {
                    int create = 0;
                    int temp = 8;
                    if (raycastHit.collider.gameObject.transform.tag == "brick")
                    {
                        temp = 0;
                        create = 1;
                    }
                    else if (raycastHit.collider.gameObject.transform.tag == "coarse_dirt")
                    {
                        temp = 1;
                        create = 1;
                    }
                    else if (raycastHit.collider.gameObject.transform.tag == "glass")
                    {
                        temp = 2;
                        create = 1;
                    }
                    else if (raycastHit.collider.gameObject.transform.tag == "gold_ore")
                    {
                        temp = 3;
                        create = 1;
                    }
                    else if (raycastHit.collider.gameObject.transform.tag == "grass")
                    {
                        temp = 4;
                        create = 1;
                    }
                    else if (raycastHit.collider.gameObject.transform.tag == "lamp")
                    {
                        temp = 5;
                        create = 1;
                    }
                    else if (raycastHit.collider.gameObject.transform.tag == "log")
                    {
                        temp = 6;
                        create = 1;
                    }
                    else if (raycastHit.collider.gameObject.transform.tag == "stone")
                    {
                        temp = 7;
                        create = 1;
                    }
                    //Hsin
                    if (raycastHit.collider.gameObject.transform.tag == "TreeHouse" && rigidBody.position.x < 12 && rigidBody.position.x > 9.5 && rigidBody.position.z > 57 && rigidBody.position.z < 60)
                        SceneManager.LoadScene(1);
                    if (create == 1)
                    {
                        int ran = UnityEngine.Random.Range(0, 8);
                        GameObject c;
                        if (ran == 0)
                        {
                            c = Instantiate(brick);
                            c.transform.tag = "brick";
                        }
                        else if (ran == 1)
                        {
                            c = Instantiate(coarse_dirt);
                            c.transform.tag = "coarse_dirt";
                        }
                        else if (ran == 2)
                        {
                            c = Instantiate(glass);
                            c.transform.tag = "glass";
                        }
                        else if (ran == 3)
                        {
                            c = Instantiate(gold_ore);
                            c.transform.tag = "gold_ore";
                        }
                        else if (ran == 4)
                        {
                            c = Instantiate(grass);
                            c.transform.tag = "grass";
                        }
                        else if (ran == 5)
                        {
                            c = Instantiate(lamp);
                            c.transform.tag = "lamp";
                        }
                        else if (ran == 6)
                        {
                            c = Instantiate(log);
                            c.transform.tag = "log";
                        }
                        else
                        {
                            c = Instantiate(stone);
                            c.transform.tag = "stone";
                        }
                        c.transform.position = new Vector3(raycastHit.collider.transform.position.x, raycastHit.collider.transform.position.y - 1, raycastHit.collider.transform.position.z);
                        c.transform.localScale = new Vector3(9 * c.transform.localScale.x, c.transform.localScale.y, 9 * c.transform.localScale.z);
                        Destroy(raycastHit.collider.gameObject);
                        if (rigidBody.transform.localPosition.y - c.transform.localPosition.y > 1)
                        {
                            for (int a = -1; a < 2; a++)
                            {
                                for (int b = -1; b < 2; b++)
                                {
                                    GameObject d;
                                    d = Instantiate(c);
                                    d.transform.position = new Vector3(c.transform.position.x + a * c.transform.localScale.x, c.transform.position.y, c.transform.position.z + b * c.transform.localScale.z);
                                }
                            }
                        }
                    }
                    //Hsin
                    for (int i = 0; i < 4; i++)
                    {
                        if (item[i] == temp&&temp!=8)
                        {
                            icount[i]++;
                            return;
                        }
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        if (icount[i]==0&&temp!=8)
                        {
                            item[i] = temp;
                            icount[i]++;
                            break;
                        }
                    }
                }
            }
            /*Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit1;
            if (Physics.Raycast(ray1, out raycastHit1))
            {
                if (Mathf.Abs(raycastHit1.point.x - transform.localPosition.x) < 2 && Mathf.Abs(raycastHit1.point.y - transform.localPosition.y) < 2 && Mathf.Abs(raycastHit1.point.z - transform.localPosition.z) < 2)
                {
                    
                }
            }*/
        }
        //stacking the cube
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rch;

            if (Physics.Raycast(ray, out rch))
            {
                if (Mathf.Abs(rch.point.x - transform.localPosition.x) < 2 && Mathf.Abs(rch.point.y - transform.localPosition.y) < 2 && Mathf.Abs(rch.point.z - transform.localPosition.z) < 2)
                {
                    GameObject c;
                    if (item[0] == 0)
                    {
                        c = Instantiate(brick);
                        c.transform.tag = "brick";
                    }
                    else if (item[0] == 1)
                    {
                        c = Instantiate(coarse_dirt);
                        c.transform.tag = "coarse_dirt";
                    }
                    else if (item[0] == 2)
                    {
                        c = Instantiate(glass);
                        c.transform.tag = "glass";
                    }
                    else if (item[0] == 3)
                    {
                        c = Instantiate(gold_ore);
                        c.transform.tag = "gold_ore";
                    }
                    else if (item[0] == 4)
                    {
                        c = Instantiate(grass);
                        c.transform.tag = "grass";
                    }
                    else if (item[0] == 5)
                    {
                        c = Instantiate(lamp);
                        c.transform.tag = "lamp";
                    }
                    else if (item[0] == 6)
                    {
                        c = Instantiate(log);
                        c.transform.tag = "log";
                    }
                    else if (item[0] == 7)
                    {
                        c = Instantiate(stone);
                        c.transform.tag = "stone";
                    }
                    else
                    {
                        return;
                    }
                    if (rch.collider.gameObject.transform.tag == "stone"|| rch.collider.gameObject.transform.tag == "log" || rch.collider.gameObject.transform.tag == "lamp" || rch.collider.gameObject.transform.tag == "grass" || rch.collider.gameObject.transform.tag == "gold_ore" || rch.collider.gameObject.transform.tag == "glass" || rch.collider.gameObject.transform.tag == "coarse_dirt" || rch.collider.gameObject.transform.tag == "brick")
                    {

                        //Debug.Log("n" + rch.normal);
                        c.transform.position = rch.transform.position + rch.normal;
                        
                    }
                    else
                    {
                        float PosX = Mathf.Floor(rch.point.x) + c.transform.localScale.x / 2;
                        float PosY = Mathf.Floor(rch.point.y) + c.transform.localScale.y / 2;
                        float PosZ = Mathf.Floor(rch.point.z) + c.transform.localScale.z / 2;
                        c.transform.position = new Vector3(PosX, PosY, PosZ);
                        
                        //Debug.Log("adsjlkf"+rch.transform.position+" "+rch.normal);
                        //c.transform.position = rch.transform.position + rch.normal;
                    }
                    icount[0] --;
                }
            }

        }
    }

}
