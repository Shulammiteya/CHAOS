using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour {
    private float MaxSpawnCD;
    [SerializeField]
    GameObject atk_1;
    [SerializeField]
    float atk_1_speed_down, atk_1_speed_big;
    [SerializeField]
    float atkCD;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if(Input.GetKey(KeyCode.L))
        //{
            ATK_type1();
        //}
        

    }

    public void ATK_type1() //fail
    {
        if (Time.time >= MaxSpawnCD)
        {
            Instantiate(atk_1, transform);
            //while(atk_1.transform.position.y > 1.5f)
            //{
            atk_1.transform.position -= new Vector3(0, 1, 0) * atk_1_speed_down * Time.deltaTime;
            Debug.Log("down");
            //}
            //while(atk_1.transform.localScale.x < 20)
            // {
            atk_1.transform.localScale += new Vector3(1, 0, 1) * atk_1_speed_big * Time.deltaTime;
            Debug.Log("bigger");
            //}
                Destroy(atk_1,5f);
            Debug.Log("destroy");
            MaxSpawnCD = Time.time + atkCD;
        }

            
        
     
    }
}
