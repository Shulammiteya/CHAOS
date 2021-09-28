using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour {
    private int b = 0, c = 0, h = 0;
    [SerializeField]
    Text[] text = new Text[4];
    [SerializeField]
    AudioListener ear;
    [SerializeField]
    GameObject relic, door1, door2, paper, angel, door3, flag, flag_2;
    public Image group;


    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(transform.position);
        if (relic == null)
        {
            if (door1.transform.position.x < -4)
            door1.transform.position = new Vector3(door1.transform.position.x + 1 * Time.deltaTime, door1.transform.position.y, door1.transform.position.z);
        }
        if (paper == null)
        {
            if (door2.transform.position.x < -4)
                door2.transform.position = new Vector3(door2.transform.position.x + 1 * Time.deltaTime, door2.transform.position.y, door2.transform.position.z);
        }
        if(flag == null)
        {
            if (door3.transform.position.x < -4)
                door3.transform.position = new Vector3(door3.transform.position.x + 1 * Time.deltaTime, door3.transform.position.y, door3.transform.position.z);
        }
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D))
        {
            if (b < 100)
            {
                b++;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (ear.enabled == false)
            {
                ear.enabled = true;
            }
            else
            {
                ear.enabled = false;
            }
        }
        if (flag_2 != null && b == 100)
        {
            text[0].text = "";
            text[1].text = "跟著箭頭走";
            text[2].text = "";
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (h == 0)
            {
                h = 1;
            }
            else
            {
                h = 0;
            }
        }
        if (h == 1)
        {
            text[0].text = "按H關閉教學";
            text[1].text = "● ESC : 目錄        ● Space : 跳躍        ● P : 背包        ● Q : 靜音        ● C : 切換第一/第三人稱";
            text[2].text = "● W、A、S、D : 走路 (按下Shift鍵 轉為跑步)			● C : 切換第一/第三人稱";
        }
        Ray pointer = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit pointhit;
        if (Physics.Raycast(pointer, out pointhit))
        {
            if (pointhit.collider.gameObject.name == "Altar")
            {
                text[0].text = "";
                text[1].text = "按滑鼠左鍵，拾起祭壇上的聖物";
                text[2].text = "";
                b = 0;
            }
            if (pointhit.collider.gameObject.transform.tag == "relic")
            {
                text[3].text = "聖物";
            }
            else
            {
                text[3].text = "";
            }
            if (pointhit.collider.gameObject.name == "angelStatue")
            {
                if (c == 0)
                {
                    text[0].text = "";
                    text[1].text = "按滑鼠左鍵，攻擊雕像三下，取得聖經";
                    text[2].text = "";
                }
                if (c == 1)
                {
                    text[0].text = "";
                    text[1].text = "按滑鼠左鍵，攻擊雕像三下，取得聖經";
                    text[2].text = "還有兩下";
                }
                if (c == 2)
                {
                    text[0].text = "";
                    text[1].text = "按滑鼠左鍵，攻擊雕像三下，取得聖經";
                    text[2].text = "最後一下";
                }
                
                text[3].text = "雕像";
                b = 0;
            }
            if (pointhit.collider.gameObject.name == "codepaper" && angel == null)
            {
                if (c == 3)
                {
                    text[0].text = "";
                    text[1].text = "按滑鼠左鍵，拾起聖經";
                    text[2].text = "";
                }
                text[3].text = "聖經";
            }
            if (pointhit.collider.gameObject.name == "MoonStone01")
            {
                if (flag_2 == null)
                {
                    text[0].text = "";
                    text[1].text = "";
                    text[2].text = "";
                    text[3].text = "";
                    group.enabled = false;
                }
                else
                {
                    text[0].text = "";
                    text[1].text = "按滑鼠左鍵，偷取寶石";
                    text[2].text = "";
                    text[3].text = "寶石";
                    b = 0;
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rch;

            if (Physics.Raycast(ray, out rch))
            {
                if (rch.collider.gameObject.transform.tag == "relic")
                {
                    if (Mathf.Abs(rch.point.x - transform.localPosition.x) < 2 && Mathf.Abs(rch.point.y - transform.localPosition.y) < 2 && Mathf.Abs(rch.point.z - transform.localPosition.z) < 2)
                    {
                        Destroy(relic);
                    }
                    else
                    {
                        text[0].text = "";
                        text[1].text = "再靠近一點";
                        text[2].text = "";
                    }
                }
                if (rch.collider.gameObject.transform.name== "angelStatue")
                {
                    if (Mathf.Abs(rch.point.x - transform.localPosition.x) < 2 && Mathf.Abs(rch.point.y - transform.localPosition.y) < 2 && Mathf.Abs(rch.point.z - transform.localPosition.z) < 2)
                    {
                        c++;
                        if (c == 3)
                        {
                            Destroy(rch.collider.gameObject);
                        }
                    }
                }
                if (rch.collider.gameObject.transform.name =="codepaper" && angel == null)
                {
                    if (Mathf.Abs(rch.point.x - transform.localPosition.x) < 2 && Mathf.Abs(rch.point.y - transform.localPosition.y) < 2 && Mathf.Abs(rch.point.z - transform.localPosition.z) < 2)
                    {
                        Destroy(paper);
                    }
                    else
                    {
                        text[0].text = "";
                        text[1].text = "再靠近一點";
                        text[2].text = "";
                    }
                }
                if (rch.collider.gameObject.transform.name == "MoonStone01")
                {
                    if (Mathf.Abs(rch.point.x - transform.localPosition.x) < 2 && Mathf.Abs(rch.point.y - transform.localPosition.y) < 2 && Mathf.Abs(rch.point.z - transform.localPosition.z) < 2)
                    {
                        Destroy(flag_2);
                    }
                    else
                    {
                        text[0].text = "";
                        text[1].text = "再靠近一點";
                        text[2].text = "";
                    }
                }
            }
        }
    }
}
