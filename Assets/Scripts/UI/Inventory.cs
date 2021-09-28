using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public Text[] text = new Text[4];
    public GameObject paper, relic;
    [SerializeField]
    RawImage[] image= new RawImage[4];
    [SerializeField]
    Texture relic1, bible1;
    private string[] item = new string[4];
    private int c = 0;
    // Use this for initialization
    void Start () {
        item[0] = "";
        item[1] = "";
        item[2] = "";
        item[3] = "";
    }
	
	// Update is called once per frame
	void Update () {

        if (relic == null&&c==0)
        {
            for (int i = 0; i < 4; i++)
            {
                if (item[i] == "")
                {
                    item[i] = "relic";
                    image[i].texture = relic1;
                    c = 1;
                    break;
                }
            }

        }
        if (paper == null && c == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                if (item[i] == "")
                {
                    item[i] = "bible";
                    image[i].texture = bible1;
                    c ++;
                    break;
                }
            }

        }
    }
}
