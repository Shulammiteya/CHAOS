using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class view : MonoBehaviour {
    float RotationX;
    public GameObject flag;
    [SerializeField]
    float sensitivityX;
    // Use this for initialization
    void Start () {
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        //第一人稱視角控制 平視旋轉
        if(flag != null && transform.position.z > 75)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            RotationX += Input.GetAxis("Mouse X") * sensitivityX;
            transform.localEulerAngles = new Vector3(0, RotationX, 0);
        }
        
	}
}
