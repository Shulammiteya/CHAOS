using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blind : MonoBehaviour {
    [SerializeField]
    GameObject mask;
    [SerializeField]
    GameObject night_vision;
    [SerializeField]
    GameObject flag;
    [SerializeField]
    GameObject door;
    private bool inblack = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rch;
            if (Physics.Raycast(ray, out rch))
            {
                Debug.Log(rch.collider.gameObject.name);
                Debug.Log(rch.collider.gameObject.transform.tag);
                if (rch.collider.gameObject.transform.tag == "jewel")
                {
                    flag.transform.position = new Vector3(flag.transform.position.x, flag.transform.position.y, 4);
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.E) && transform.position.x < -36) 
        {
            transform.position = new Vector3(-4.4f,0.39f,0.71f);
            door.transform.position = new Vector3(-34.5f,5,1.62f);
            flag.transform.position = new Vector3(flag.transform.position.x, flag.transform.position.y, -2);
            inblack = false;
        }
        
        if (transform.position.x<-36&&flag.transform.position.z ==0)
        {
            inblack = true;
            flag.transform.position = new Vector3(flag.transform.position.x,flag.transform.position.y,1);
        }
        /*else
        {
            inblack = false;
        }*/

        if (inblack==true&&flag.transform.position.z!=4)
        {
            mask.transform.localPosition = new Vector3(0, -102f, 0);
            if (door.transform.position.y > 2)
            {
                door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y - 0.5f * Time.deltaTime, door.transform.position.z);
            }
            else
            {
                door.transform.position = new Vector3(door.transform.position.x, 2, door.transform.position.z);
            }
        }
         if (flag.transform.position.z ==3)
        {
            mask.transform.localPosition = new Vector3(0, -1.017719f, 0);
        }
        else if (flag.transform.position.z == 4)
        {//破關成功
            transform.position = new Vector3(-4.4f, 0.39f, 0.71f);
            inblack = false;
        }
		if(!inblack) 
        {
            mask.SetActive(false);
            night_vision.SetActive(false);
        }
        else //如果在黑色關中就增加遮罩並且讓玩家僅能看到周圍
        {
            mask.SetActive(true);
            night_vision.SetActive(true);
        }
	}
    /*void OnTriggersStay(Collider c)
    {
        if(c.gameObject.name == "black") //判斷是否進入黑色關卡
        {
            inblack = true;
        }
        else
        {
            inblack = false;
        }
    }*/
}
