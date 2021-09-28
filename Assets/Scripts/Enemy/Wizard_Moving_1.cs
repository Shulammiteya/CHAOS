using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_Moving_1 : MonoBehaviour
{
    private float move = 20;
    private bool stop = false, MoveStart = false, MoveEnd = false, attack = false, rotateRight = false, rotateLeft = false, alive = true, trick = false, cloudATK = false, fly = false;
    private float blend;
    private float delay = 0;
    private GameObject Rune, Cloud;

    public static int blood = 12;
    public float AddRunSpeed = 1;
    public float AddWalkSpeed = 1;
    public static bool WizardATK = false;
    public GameObject player, magicRune, magicCloud;

    // Use this for initialization
    void Start()
    {

    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(4f);
        alive = false;
    }

    IEnumerator AttackBeforeDie()
    {
        yield return new WaitForSeconds(4f);
        cloudATK = true;
        yield return new WaitForSeconds(3f);
        cloudATK = trick = fly = false;
        Jason_Moving_2_1.blood--;
        Destroy(Rune);
        Destroy(Cloud);
    }

    // Update is called once per frame
    void Update()
    {
        if (fly && transform.localPosition.y < 5)
        {
            transform.Translate(0, 3f * Time.deltaTime, 0);
        }
        else if (transform.localPosition.y >= 0.321f)
        {
            transform.Translate(0, -4f * Time.deltaTime, 0);
        }
        if (trick)
        {
            transform.Rotate(0, Time.deltaTime * -800, 0);
            if (cloudATK)
            {
                Vector3 way = player.transform.localPosition - Cloud.transform.localPosition;
                Cloud.transform.localPosition += 0.2f * way;
            }
        }
        if(blood==4)
        {
            blood -= 2;
            trick = true;
            fly = true;
            Rune = Instantiate(magicRune);
            Cloud = Instantiate(magicCloud);
            Rune.transform.localRotation = transform.localRotation;
            Cloud.transform.localRotation = transform.localRotation;
            Rune.transform.localPosition = transform.localPosition + new Vector3(0, 7, 0);
            Cloud.transform.localPosition = transform.localPosition + new Vector3(0, 7, 0);
            StartCoroutine(AttackBeforeDie());
            return;
        }
        else if (blood <= 0 && !trick)
        {
            if (GetComponent<Animation>().isPlaying)
            {
                GetComponent<Animation>().Play("dead");
            }
        }
        else if(!Boss.BossMode1 && !trick)
        {
            Move();
            Rotate();


            if (attack == false)
            {
                if (WizardATK == true)
                {
                    attack = true;
                    StartCoroutine(Mode_Attack());
                }
                else
                {
                    StartCoroutine(Mode_Normal());
                }
            }
        }
    }

    IEnumerator Mode_Attack()
    {
        var lookpos = player.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(lookpos.x, 0, lookpos.z), new Vector3(0, 1, 0));
        MoveStart = true;
        yield return new WaitForSeconds(Random.Range(1f, 1.8f));
        MoveStart = false;
        MoveEnd = true;
        yield return new WaitForSeconds(Random.Range(0.2f, 1f));
        MoveEnd = false;
        lookpos = player.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(lookpos.x, 0, lookpos.z), new Vector3(0, 1, 0));
        if (!GetComponent<Animation>().IsPlaying("damade_001"))
        {
            GetComponent<Animation>().CrossFade("attack_short_001", 0.0f);
            GetComponent<Animation>().CrossFadeQueued("idle_combat");
        }
        yield return new WaitForSeconds(Random.Range(0.2f, 1f));
        if (Vector3.Distance(transform.localPosition, player.transform.localPosition) < 3)
            Jason_Moving_2_1.blood--;
        attack = false;
    }

    IEnumerator Mode_Normal()
    {
        MoveStart = true;
        rotateRight = (Random.Range(-1, 1) == 0) ? true : false;
        rotateLeft = (rotateRight == false) ? true : false;
        StartCoroutine(Mode_Normal_2());
        yield return new WaitForSeconds(Random.Range(0.8f, 1.5f));
        MoveStart = false;
        MoveEnd = true;
        yield return new WaitForSeconds(Random.Range(0.2f, 1f));
        MoveEnd = false;
    }

    IEnumerator Mode_Normal_2()
    {
        yield return new WaitForSeconds(Random.Range(0.8f, 1.5f));
        rotateRight = false;
        rotateLeft = false;
    }

    void Move()
    {

        float speed = 0.0f;
        float add = 0.0f;



        if (MoveStart == true)
        {
            move *= 1.015F;

            if (move > 250)
            {
                if (!GetComponent<Animation>().IsPlaying("damade_001"))
                    GetComponent<Animation>().CrossFade("move_forward_fast");
                add = 20 * AddRunSpeed;
            }
            else
            {
                if (!GetComponent<Animation>().IsPlaying("damade_001"))
                    GetComponent<Animation>().Play("move_forward");
                add = 5 * AddWalkSpeed;
            }

            speed = Time.deltaTime * add;

            transform.Translate(0, 0, speed);
        }


        if (MoveEnd == true)
        {
            if (GetComponent<Animation>().IsPlaying("move_forward"))
            {
                if (!GetComponent<Animation>().IsPlaying("damade_001"))
                    GetComponent<Animation>().CrossFade("idle_normal", 0.3f);
            }
            if (GetComponent<Animation>().IsPlaying("move_forward_fast"))
            {
                if (!GetComponent<Animation>().IsPlaying("damade_001"))
                    GetComponent<Animation>().CrossFade("idle_combat", 0.5f);
                stop = true;
            }
            move = 20;
        }

        if (stop == true)
        {
            float max = Time.deltaTime * 20 * AddRunSpeed;
            blend = Mathf.Lerp(max, 0, delay);

            if (blend > 0)
            {
                transform.Translate(0, 0, blend);
                delay += 0.025f;
            }
            else
            {
                stop = false;
                delay = 0.0f;
            }
        }
    }


    void Rotate()
    {

        if (rotateRight)
        {
            transform.Rotate(0, Time.deltaTime * -100, 0);
        }

        if (rotateLeft)
        {
            transform.Rotate(0, Time.deltaTime * 100, 0);
        }

    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name == "FireballCollider")
        {
            if (!trick && blood > 0)
                blood-=2;
            GetComponent<Animation>().CrossFade("damage_001", 1f);
            Vector3 ExploPos = transform.position + new Vector3(0, 0, 3);
        }
    }

    void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            Vector3 ExploPos = c.transform.position - transform.position;
            StartCoroutine(DelayClaw(c, ExploPos));
        }
    }

    private IEnumerator DelayClaw(Collision c, Vector3 ExploPos)
    {
        yield return new WaitForSeconds(0.25f);
        c.rigidbody.AddExplosionForce(500f, ExploPos, 20f);
    }

}

