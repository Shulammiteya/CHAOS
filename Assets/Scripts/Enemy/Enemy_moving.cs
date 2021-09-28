using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_moving : MonoBehaviour {
    private Rigidbody rid;
    private bool isatk = false;
    private Animator animator;
    private MeshCollider meshcollider;
    private float MaxSpawnCD;
    [SerializeField]
    GameObject player;
    [SerializeField]
    float speed,viewDis_Far, viewDis_Near;
    [SerializeField]
    GameObject atk_Far,atk_Near;
    [SerializeField]
    float atkCD;
	// Use this for initialization
	void Start () {
        rid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        meshcollider = GetComponent<MeshCollider>();
    }
	
	// Update is called once per frame
	void Update () {
        float dis = Vector3.Distance(transform.position, player.transform.position);
        if (dis < viewDis_Far && dis > viewDis_Near)
        {
            rid.WakeUp();
            //transform.LookAt(player.transform);
            //rid.AddRelativeForce(forward * speed);
            var lookpos = player.transform.position - transform.position;
            if (!isatk)
            {
                transform.rotation = Quaternion.LookRotation(lookpos);
            }
            var rotation = Quaternion.LookRotation(lookpos, Vector3.up);            
            Vector3 forward = transform.TransformDirection(new Vector3(0, meshcollider.bounds.size.y/2, meshcollider.bounds.size.z));
            if (Time.time >= MaxSpawnCD)
            {
                isatk = true;
                animator.SetTrigger("Breath_Gs");
                StartCoroutine(DelayAtk(atk_Far,forward,rotation,1f));              
                MaxSpawnCD = Time.time + atkCD;
            }
            

        }
        else if(dis < viewDis_Near)
        {
            rid.WakeUp();
            //transform.LookAt(player.transform);
            //rid.AddRelativeForce(forward * speed);
            var lookpos = player.transform.position - transform.position;
            if (!isatk)
            {
                transform.rotation = Quaternion.LookRotation(lookpos);
            }
            var rotation = transform.rotation = Quaternion.LookRotation(new Vector3(lookpos.x, 0, lookpos.z), new Vector3(0, 1, 0));
            Vector3 forward = transform.TransformDirection(new Vector3(0, meshcollider.bounds.size.y / 10, meshcollider.bounds.size.z));
            if (Time.time >= MaxSpawnCD)
            {
                isatk = true;
                animator.SetTrigger("Breath_Gs");
                StartCoroutine(DelayAtk(atk_Near,forward, rotation,1.3f));
                MaxSpawnCD = Time.time + atkCD;
            }
            
        }
        else
        {
            rid.Sleep();
        }       
	}
    private IEnumerator DelayAtk(GameObject atk,Vector3 forward, Quaternion rotation,float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(atk, transform.position + forward, rotation);
        if(atk == atk_Near)
        {
            yield return new WaitForSeconds(0.75f);
        }
        isatk = false;
    }
    void OnCollisionStay(Collision c)
    {
        if(c.gameObject.tag == "Player")
        {
            animator.SetTrigger("Atk_Claw_DBL"); 
            Vector3 ExploPos = c.transform.position + new Vector3(0, 0, 3);
            StartCoroutine(DelayClaw( c,  ExploPos));
        }
    }
    private IEnumerator DelayClaw(Collision c,Vector3 ExploPos)
    {
        yield return new WaitForSeconds(1);
        c.rigidbody.AddExplosionForce(500f, ExploPos, 20f);
    }
}
