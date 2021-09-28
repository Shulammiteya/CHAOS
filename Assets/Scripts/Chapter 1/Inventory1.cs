using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory1 : MonoBehaviour {
    [SerializeField]
    RawImage[] image = new RawImage[4];
    private string[] item = new string[4];
    // Use this for initialization
    void Start () {
        item[0] = "";
        item[1] = "";
        item[2] = "";
        item[3] = "";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
