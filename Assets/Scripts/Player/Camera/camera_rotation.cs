using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_rotation : MonoBehaviour {
    float  RotationY;
    private bool firstperson = false;
    [SerializeField]
    float sensitivityY;
    public GameObject flag,viking;
    // Use this for initialization
    void Start () {
        transform.localPosition = new Vector3(-0.3f, 1.35f, 0.6f);
    }
	
	// Update is called once per frame
	void Update () {
        if (flag != null && viking.transform.position.z > 75)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            RotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            RotationY = Mathf.Clamp(RotationY, -90, 90);
            transform.localEulerAngles = new Vector3(-RotationY, 0, 0);
        }
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            firstperson = !firstperson;
        }
        if (firstperson)
        {
            FirstPerson();
        }
        else
        {
            ThirdPerson();
        }
    }
    private void FirstPerson()
    {
        transform.localPosition = new Vector3(0f, 1.5f, 0.6f);
    }
    private void ThirdPerson()
    {
        transform.localPosition = new Vector3(0f, 1.5f, -1.4f);
        //transform.localPosition = new Vector3(-0.5f, 1.15f,-1.5f);
    }
}
