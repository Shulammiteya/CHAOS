using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static int blood = 12;
    public static bool BossMode1 = true;

    bool isDark;
    private Rigidbody rid;
    private BoxCollider box_col;
    private bool isatk = false;
    private Animation anim;
    private float MaxSpawnCD;
    [SerializeField]
    Light sun;
    [SerializeField]
    GameObject player;
    [SerializeField]
    float speed;
    [SerializeField]
    GameObject atk_1;
    [SerializeField]
    float atkCD;
    // Use this for initialization
    void Start()
    {
        box_col = GetComponent<BoxCollider>();
        anim = GetComponent<Animation>();
        rid = GetComponent<Rigidbody>();
        isDark = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (blood <= 3)
        {
            int wizardLifeChack = 0;
            if (Wizard_Moving_1.blood > 0)
                wizardLifeChack++;
            if (Wizard_Moving_2.blood > 0)
                wizardLifeChack++;
            if (Wizard_Moving_3.blood > 0)
                wizardLifeChack++;
            blood = wizardLifeChack;
        }
        if (BossMode1)
        {
            var lookpos = player.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(new Vector3(lookpos.x, 0, lookpos.z), new Vector3(0, 1, 0));

            if (isDark == false)
                sun.color = Color.Lerp(sun.color, Color.white, 2.5f * Time.deltaTime);
            else
                sun.color = Color.Lerp(sun.color, Color.black, 2.5f * Time.deltaTime);


            int ATK_ran_type = Random.Range(1, 4);
            /*if (!isatk)
            {
                ATK_ran_type = Random.Range(1, 4);
            }  */
            switch (ATK_ran_type)
            {
                case 1:
                    Atk_type1();
                    break;
                case 2:
                    Atk_type2();
                    break;
                case 3:
                    Atk_type3();
                    break;
            }

            if (!anim.isPlaying)
            {
                anim.Play("Walk");
                rid.AddRelativeForce(0, 0, 50);
            }
        }
    }
    private void Atk_type1()
    {
        //transform.LookAt(player.transform);
        //rid.AddRelativeForce(forward * speed);
        var lookpos = player.transform.position - transform.position;
        if (!isatk)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(lookpos.x, 0, lookpos.z), new Vector3(0, 1, 0));
            if(!anim.IsPlaying("Walk"))
                anim.CrossFade("Walk");
        }
        var rotation = Quaternion.LookRotation(lookpos, Vector3.up);       
        if (Time.time >= MaxSpawnCD)
        {
            isatk = true;
            if (!anim.IsPlaying("ATK_throw"))
                anim.CrossFade("ATK_throw");
            StartCoroutine(DelayAtk(atk_1, player.transform.position, rotation, 0.5f));
            MaxSpawnCD = Time.time + atkCD;
        }
    } //召喚隕石
    private void Atk_type2()
    {      
        var lookpos = player.transform.position - transform.position;
        if (!isatk)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(lookpos.x, 0, lookpos.z), new Vector3(0, 1, 0));
            if (!anim.IsPlaying("Walk"))
                anim.CrossFade("Walk");
        }
        var rotation = Quaternion.LookRotation(lookpos, Vector3.up);       
        if (Time.time >= MaxSpawnCD)
        {
            isatk = true;
            if (!anim.IsPlaying("Walk"))
            {
                anim.CrossFade("ATK_dash");
            }
            Vector3 pos_player = new Vector3(player.transform.position.x,0,player.transform.position.z);           
            rid.AddRelativeForce(new Vector3(0, 0, 1) * speed);
            StartCoroutine(DelayDash(4f));          
            MaxSpawnCD = Time.time + atkCD;           
        }
    } //衝撞
    private void Atk_type3() //致盲 混亂
    {
        var lookpos = player.transform.position - transform.position;
        if (!isatk)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(lookpos.x, 0, lookpos.z), new Vector3(0, 1, 0));
            if (!anim.IsPlaying("Walk"))
                anim.CrossFade("Walk");
            isDark = false;
            Jason_Moving_2_1.self.walk_run_type = 1;
        }
        var rotation = Quaternion.LookRotation(lookpos, Vector3.up);
        if (Time.time >= MaxSpawnCD)
        {
            int type = Random.Range(2,4);
            isatk = true;
            isDark = true;
            if (!anim.IsPlaying("ATK_roar"))
                anim.CrossFade("ATK_roar");
            Jason_Moving_2_1.self.walk_run_type = type;
            StartCoroutine(DelayBlind(1f,3f));
            MaxSpawnCD = Time.time + atkCD;
        }
    }
    private IEnumerator DelayBlind(float delay,float effect)
    {
        yield return new WaitForSeconds(effect);
        isDark = true;
        yield return new WaitForSeconds(delay);
        isatk = false;
    }
    private IEnumerator DelayDash(float delay)
    {        
        yield return new WaitForSeconds(delay);
        isatk = false;
    }
    private IEnumerator DelayAtk(GameObject atk, Vector3 position, Quaternion rotation, float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(atk, position, rotation);
        isatk = false;
    }

    void OnCollisionEnter(Collision c)
    {
        if (BossMode1)
        {
            if (c.gameObject.name == "FireballCollider")
            {
                if (blood > 3)
                    blood--;
                else if (blood == 3)
                {
                    BossMode1 = false;
                }
                GetComponent<Animation>().CrossFade("damage_001", 1f);
                //Vector3 ExploPos = transform.position + new Vector3(0, 0, 3);
            }
            if (c.gameObject.tag == "Player")
            {
                if (!isatk)
                {
                    anim.Play("ATK_hit");
                }
                Vector3 ExploPos = c.transform.position - transform.position;
                StartCoroutine(DelayClaw(c, ExploPos));
            }
        }
    }
    
    private IEnumerator DelayClaw(Collision c, Vector3 ExploPos)
    {
        yield return new WaitForSeconds(0.25f);
        c.rigidbody.AddExplosionForce(500f, ExploPos, 20f);
    }
}
