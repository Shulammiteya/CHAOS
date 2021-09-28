using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Chaos : MonoBehaviour {

    public RawImage[] UpperHaelthBar = new RawImage[12];
    public RawImage[] LowerHaelthBar = new RawImage[10];

    int barNum;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for (barNum = 0; barNum < Boss.blood; barNum++)
        {
            UpperHaelthBar[barNum].enabled = true;
        }
        for (; barNum < 12; barNum++)
        {
            UpperHaelthBar[barNum].enabled = false;
        }
        if (Boss.blood < 10)
        {
            for (barNum = 0; barNum < Boss.blood; barNum++)
            {
                LowerHaelthBar[barNum].enabled = true;
            }
            for (; barNum < 12; barNum++)
            {
                LowerHaelthBar[barNum].enabled = false;
            }
        }
    }
}
