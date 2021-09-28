using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Instruction : MonoBehaviour {//flag x==5 PINK 過關
    private Vector3 pos;
    private int h = 0;
    private Color trans = new Color(),temp = new Color();
    private float clock=0, c=0;
    [SerializeField]
    AudioListener ear;
    [SerializeField]
    GameObject flag, door ;
    [SerializeField]
    GameObject jason;
    [SerializeField]
    Text[] text = new Text[4];
	// Use this for initialization
	void Start () {
        flag.transform.position = new Vector3(2,0,0);
        pos = transform.position;
        transform.position = new Vector3(-500, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

        if (jason.transform.position.y< -3)
        {
            jason.transform.position = new Vector3(4, 0.39f,-0.5f);
        }
       //flag x 2=初始 0-2 = 第一題完成 3-5破關提示 6結束狀態
       //flag y 0=初始 -1-0=失敗 0-4 = 第一階段 4-8第二階段 9-11破關提示 12結束
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (ear.enabled == true) 
            {
                ear.enabled = false;
            }
            else
            {
                ear.enabled = true;
            }
        }
        
        if (flag.transform.position.x > 0 && flag.transform.position.x < 2)
        {

            h = 1;
            text[0].text = "";
            text[1].text = "謎題一完成";
            text[2].text = "";
            flag.transform.position = new Vector3((flag.transform.position.x - (0.5f * Time.deltaTime)), flag.transform.position.y, flag.transform.position.z);
        }
        else if (flag.transform.position.x <= 0)
        {
            flag.transform.position = new Vector3(0, flag.transform.position.y, flag.transform.position.z);
            h = 0;
        }
        else if (flag.transform.position.x < 6 && flag.transform.position.x >= 5)
        {
            flag.transform.position = new Vector3(6, flag.transform.position.y, flag.transform.position.z);
            h = 0;
        }
        else if (flag.transform.position.x >= 3 && flag.transform.position.x < 5)
        {
            h = 1;
            text[0].text = "";
            text[1].text = "破關成功";
            text[2].text = "";
            flag.transform.position = new Vector3((flag.transform.position.x + (0.5f * Time.deltaTime)), flag.transform.position.y, flag.transform.position.z);
        }
        //z 0 = initial 1-2= instruct -1--2=failed 3=start 4-5 =win notice 6=finished
        else if (flag.transform.position.z >= 1 && flag.transform.position.z < 2)
        {
            trans = gameObject.GetComponent<Image>().color;
            temp = trans;
            temp.a = 142 / 256f;
            trans.a = 1;
            h = 1;
            gameObject.GetComponent<Image>().color = trans;
            text[0].text = "迷宮";
            text[1].text = "請找出迷宮中的寶藏，按下滑鼠左鍵";
            text[2].text = "按下E鍵可放棄闖關，並將你送出迷宮";
            flag.transform.position = new Vector3(flag.transform.position.x, flag.transform.position.y, flag.transform.position.z + 0.25f * Time.deltaTime);
        }
        else if (flag.transform.position.z < -1 && flag.transform.position.z >= -2)
        {


            gameObject.GetComponent<Image>().color = temp;
            h = 1;
            text[0].text = "";
            text[1].text = "闖關失敗";
            text[2].text = "";
            flag.transform.position = new Vector3(flag.transform.position.x, flag.transform.position.y, flag.transform.position.z + 0.5f * Time.deltaTime);
        }
        else if (flag.transform.position.z < 3 && flag.transform.position.z >= 2)
        {
            flag.transform.position = new Vector3(flag.transform.position.x, flag.transform.position.y, 3);

        }
        else if (flag.transform.position.z < 0 && flag.transform.position.z >= -1)
        {
            flag.transform.position = new Vector3(flag.transform.position.x, flag.transform.position.y, 0);
        }
        else if (flag.transform.position.z < 5 && flag.transform.position.z >= 4)
        {
            gameObject.GetComponent<Image>().color = temp;
            h = 1;
            
            text[0].text = "";
            text[1].text = "闖關成功";
            text[2].text = "";
            flag.transform.position = new Vector3(flag.transform.position.x, flag.transform.position.y, flag.transform.position.z + 0.5f * Time.deltaTime);
        }
        else if (flag.transform.position.z < 6 && flag.transform.position.z >= 5)
        {
            flag.transform.position = new Vector3(flag.transform.position.x, flag.transform.position.y, 6);

        }
        //y
        else if (flag.transform.position.y >= -1 && flag.transform.position.y < 0)
        {
            h = 1;
            text[0].text = "";
            text[1].text = "闖關失敗，請過10秒後再嘗試";
            text[2].text = "";
            if (flag.transform.position.y > -0.002)
            {
                flag.transform.position = new Vector3(flag.transform.position.x, 0, flag.transform.position.z);
            }
            else
            {
                flag.transform.position = new Vector3(flag.transform.position.x, flag.transform.position.y + 0.1f * Time.deltaTime, flag.transform.position.z);

            }
        }
        else if (flag.transform.position.y >= 9 && flag.transform.position.y < 11)
        {
            h = 1;
            text[0].text = "";
            text[1].text = "破關成功";
            text[2].text = "";
            flag.transform.position = new Vector3(flag.transform.position.x, flag.transform.position.y + (0.5f * Time.deltaTime), flag.transform.position.z);
        }
        else if (flag.transform.position.y < 12 && flag.transform.position.y >= 11)
        {
            flag.transform.position = new Vector3(flag.transform.position.x, 12, flag.transform.position.z);
            h = 0;
        }
        //三關都過了
        else if (flag.transform.position.x == 6 && flag.transform.position.y == 12 && flag.transform.position.z == 6)
        {
            h = 1;
            clock = clock + 1f * Time.deltaTime;
            if (c == 0)
            {
                c = clock;
            }
            text[0].text = "恭喜你完成了，三位天神的試煉";
            text[1].text = "接下來你即將面對Chaos的考驗";
            text[2].text = "請活下去!";
            if (clock - c > 4)
            {
                SceneManager.LoadScene(4);
            }
            
        }
        else
        {
            if (jason.transform.position.z < -33)
            {
                h = 1;
                text[0].text = "請解開面前的謎題!";
                text[1].text = "將鼠標對準你想連線的顏色的方塊按下左鍵，並按下上下左右鍵，即可操作。";
                text[2].text = "R鍵可清除當前已作的操作。將同個顏色的方塊連在一起，即成功!";

            }
            else if (jason.transform.position.x > 33 && flag.transform.position.y == 0)
            {
                jason.transform.position = new Vector3(38, 0.5f, 0.1f);
                flag.transform.position = new Vector3(flag.transform.position.x, 1, flag.transform.position.z);
                door.transform.position = new Vector3(door.transform.position.x, 2, door.transform.position.z);
            }
            else if (jason.transform.position.x > 33 && flag.transform.position.y >= 1 && flag.transform.position.y < 4)
            {
                h = 1;
                text[0].text = "活下去!";
                text[1].text = "別踩白塊兒";
                text[2].text = "踩在黑色的地板上";
            }
            else if (flag.transform.position == new Vector3(2, 0, 0))
            {
                h = 1;
                text[0].text = "歡迎來到詛咒的禁地";
                text[1].text = "想離開這裡的話，就要接受諸神的試煉";
                text[2].text = "總共有三個關卡，努力完成吧";
            }
            else
            {
                h = 0;
            }

        }

        if (h == 1)
        {
            transform.position = pos;
        }
        else
        {
            transform.position = new Vector3(-500, transform.position.y, transform.position.z);
        }
    }
}
