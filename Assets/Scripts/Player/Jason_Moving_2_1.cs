using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]

public class Jason_Moving_2_1 : MonoBehaviour
{
    public static int blood = 12;
    private AudioSource audioSource;
    private Animator animator;
    public AudioClip audio_walk, audio_run, audio_jump, audio_attack;

    float count;
    public RawImage LoseWord, fadeOut, fadeOut_2;

    private Rigidbody rid;
    private bool isjump = false;
    private float AttDis = 2;
    float pressWASD = 30;
    public GameObject flag, flag_2;
    Vector3 forward = new Vector3(0, 0, 10);
    Vector3 backward = new Vector3(0, 0, -10);
    Vector3 right_move = new Vector3(10, 0, 0);
    Vector3 left_move = new Vector3(-10, 0, 0);
    Vector3 jumpDir = new Vector3(0, 250, 0);
    [SerializeField]
    float speed;

    public static Jason_Moving_2_1 self;
    public int walk_run_type;

    [SerializeField]
    GameObject enemy;
    [SerializeField]
    GameObject atk;

    // Use this for initialization
    void Start()
    {
        fadeOut.color = Color.clear;
        fadeOut.enabled = false;
        fadeOut_2.enabled = false;
        LoseWord.transform.localPosition = new Vector3(0, 400, 0);
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        rid = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        self = this;
        walk_run_type = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (blood<=0)
        {
            animator.SetBool("damage", false);
            if (count > 45)
            {
                fadeOut_2.enabled = true;
                fadeOut_2.color = Color.Lerp(fadeOut_2.color, Color.black, Time.deltaTime);
                if (fadeOut_2.color.a > 0.99)
                    SceneManager.LoadScene(0);
            }
            else if (count > 15)
            {
                fadeOut.enabled = true;
                fadeOut.color = Color.Lerp(fadeOut.color, Color.black, 1.5f * Time.deltaTime);
                if (count <= 30)
                    fadeOut_2.color = fadeOut.color;
                if (count > 30)
                {
                    LoseWord.transform.localPosition = new Vector3(0, 0, 0);
                    fadeOut_2.enabled = true;
                    fadeOut_2.color = Color.Lerp(fadeOut_2.color, Color.clear, 2 * Time.deltaTime);
                }
            }
            count = count + 0.1f;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            pressWASD = 30;
        if (flag_2 == null)
        {
            return;
        }
        switch (state())
        {
            case 0:
                rid.velocity = new Vector3(0, rid.velocity.y, 0);
                if (isjump == false)
                {
                    if (audioSource.clip != audio_attack || audioSource.isPlaying == false)
                    {
                        animator.SetFloat("speed", 0);
                        animator.SetBool("jump", false);
                        audioSource.Stop();
                    }
                }
                break;
            case 1:
                if (flag != null && transform.position.z > 75)
                {
                    break;
                }
                if (isjump == false)
                {
                    if (audioSource.clip != audio_walk || audioSource.isPlaying == false)
                    {
                        animator.SetFloat("speed", 1);
                        animator.SetBool("jump", false);
                        audioSource.clip = audio_walk;
                        audioSource.Play();
                        audioSource.loop = true;
                    }
                }
                walk(walk_run_type);
                break;
            case 2:
                if (flag != null && transform.position.z > 75)
                {
                    break;
                }
                if (isjump == false)
                {
                    if (audioSource.clip != audio_run || audioSource.isPlaying == false)
                    {
                        animator.SetFloat("speed", 3);
                        animator.SetBool("jump", false);
                        audioSource.clip = audio_run;
                        audioSource.Play();
                        audioSource.loop = true;
                    }
                }
                run(walk_run_type);
                break;
            case 3:
                if (flag != null && transform.position.z > 75)
                {
                    break;
                }
                if (audioSource.clip != audio_attack || audioSource.isPlaying == false)
                {
                    animator.SetTrigger("chop");
                    audioSource.clip = audio_attack;
                    audioSource.Play();
                    audioSource.loop = false;
                }
                attack();
                break;
            case 4:
                if (flag != null && transform.position.z > 75)
                {
                    break;
                }
                if (audioSource.clip != audio_attack || audioSource.isPlaying == false)
                {
                    animator.SetTrigger("chop");
                    audioSource.clip = audio_attack;
                    audioSource.Play();
                    audioSource.loop = false;
                }
                walk(walk_run_type);
                attack();
                break;
            case 5:
                if (flag != null && transform.position.z > 75)
                {
                    break;
                }
                if (audioSource.clip != audio_attack || audioSource.isPlaying == false)
                {
                    animator.SetTrigger("chop");
                    audioSource.clip = audio_attack;
                    audioSource.Play();
                    audioSource.loop = false;
                }
                run(walk_run_type);
                attack();
                break;
        }
        jump();
    }

    public int state()
    {
        // 0 idle , 1 walk , 2 run , 3  hit  //4 4 walk & attack  //5 run & attack
        if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //前進
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                Instantiate(atk, transform.GetChild(0).position + forward, transform.GetChild(0).rotation);
                return 5;
            }
            return 2;
        }
        else if (Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //左移
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                Instantiate(atk, transform.GetChild(0).position + forward, transform.GetChild(0).rotation);
                return 5;
            }
            return 2;
        }
        else if (Input.GetKey(KeyCode.D) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //右移
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                Instantiate(atk, transform.GetChild(0).position + forward, transform.GetChild(0).rotation);
                return 5;
            }
            return 2;
        }
        else if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //後退
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                Instantiate(atk, transform.GetChild(0).position + forward, transform.GetChild(0).rotation);
                return 5;
            }
            return 2;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                Instantiate(atk, transform.GetChild(0).position + forward, transform.GetChild(0).rotation);
                return 4;
            }
            return 1;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Instantiate(atk, transform.GetChild(0).position + forward, transform.GetChild(0).rotation);
            return 3;
        }
        else
        {
            return 0;
        }
    }

    public void walk(int type)
    {
        switch (type)
        {
            case 1:
                //Debug.Log(1);
                //正常
                if (Input.GetKey(KeyCode.W)) //前進
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 9, 0.03f);
                    rid.AddRelativeForce(0, 0, pressWASD);
                }
                if (Input.GetKey(KeyCode.A)) //左移
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 9, 0.03f);
                    rid.AddRelativeForce(-pressWASD, 0, 0);
                }
                if (Input.GetKey(KeyCode.D)) //右移
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 9, 0.03f);
                    rid.AddRelativeForce(pressWASD, 0, 0);
                }
                if (Input.GetKey(KeyCode.S)) //後退
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 9, 0.03f);
                    rid.AddRelativeForce(0, 0, -pressWASD);
                }
                break;
            case 2:
                Debug.Log(2);
                //混亂1
                if (Input.GetKey(KeyCode.S)) //前進
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 9, 0.03f);
                    rid.AddRelativeForce(0, 0, pressWASD);
                }
                if (Input.GetKey(KeyCode.D)) //左移
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 9, 0.03f);
                    rid.AddRelativeForce(-pressWASD, 0, 0);
                }
                if (Input.GetKey(KeyCode.A)) //右移
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 9, 0.03f);
                    rid.AddRelativeForce(pressWASD, 0, 0);
                }
                if (Input.GetKey(KeyCode.W)) //後退
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 9, 0.03f);
                    rid.AddRelativeForce(0, 0, -pressWASD);
                }
                break;
            case 3:
                //混亂2
                if (Input.GetKey(KeyCode.A)) //前進
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 9, 0.03f);
                    rid.AddRelativeForce(0, 0, pressWASD);
                }
                if (Input.GetKey(KeyCode.W)) //左移
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 9, 0.03f);
                    rid.AddRelativeForce(-pressWASD, 0, 0);
                }
                if (Input.GetKey(KeyCode.S)) //右移
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 9, 0.03f);
                    rid.AddRelativeForce(pressWASD, 0, 0);
                }
                if (Input.GetKey(KeyCode.D)) //後退
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 9, 0.03f);
                    rid.AddRelativeForce(0, 0, -pressWASD);
                }
                break;
        }
    }

    public void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isjump)
        {
            rid.AddForce(jumpDir);
            animator.SetBool("jump", true);
            audioSource.clip = audio_jump;
            audioSource.Play();
            audioSource.loop = false;
            isjump = true;
        }

    }

    public void run(int type)
    {
        switch (type)
        {
            case 1:
                //正常
                if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //前進
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 19, 0.03f);
                    rid.AddRelativeForce(0, 0, pressWASD);
                }
                if (Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //左移
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 19, 0.03f);
                    rid.AddRelativeForce(-pressWASD, 0, 0);
                }
                if (Input.GetKey(KeyCode.D) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //右移
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 19, 0.03f);
                    rid.AddRelativeForce(pressWASD, 0, 0);
                }
                if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //後退
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 19, 0.03f);
                    rid.AddRelativeForce(0, 0, -pressWASD);
                }
                break;
            case 2:
                //混亂1
                if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //前進
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 19, 0.03f);
                    rid.AddRelativeForce(0, 0, pressWASD);
                }
                if (Input.GetKey(KeyCode.D) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //左移
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 19, 0.03f);
                    rid.AddRelativeForce(-pressWASD, 0, 0);
                }
                if (Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //右移
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 19, 0.03f);
                    rid.AddRelativeForce(pressWASD, 0, 0);
                }
                if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //後退
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 19, 0.03f);
                    rid.AddRelativeForce(0, 0, -pressWASD);
                }
                break;
            case 3:
                //混亂2
                if (Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //前進
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 19, 0.03f);
                    rid.AddRelativeForce(0, 0, pressWASD);
                }
                if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //左移
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 19, 0.03f);
                    rid.AddRelativeForce(-pressWASD, 0, 0);
                }
                if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //右移
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 19, 0.03f);
                    rid.AddRelativeForce(pressWASD, 0, 0);
                }
                if (Input.GetKey(KeyCode.D) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //後退
                {
                    if (pressWASD > 10)
                        pressWASD = Mathf.Lerp(pressWASD, 19, 0.03f);
                    rid.AddRelativeForce(0, 0, -pressWASD);
                }
                break;
        }
    }

    void OnCollisionEnter(Collision c)
    {
        Debug.Log(c.gameObject.name);
        if (c.gameObject.name == "Meteor(Clone)")
        {
            blood--;
            animator.SetBool("damage", true);
            StartCoroutine(damageTime());
        }
        if (c.gameObject.tag == "Enemy")
        {
            /*rid.AddExplosionForce(1000f, c.transform.position, 200f);
            blood--;*/
            if (animator.GetBool("damage") == false)
                blood--;
            animator.SetBool("damage", true);
            StartCoroutine(damageTime());
        }
        isjump = false;
    }

    private IEnumerator damageTime()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("damage", false);
    }

    void OnCollisionExit(Collision c)
    {
        animator.SetBool("damage", false);
    }

    public void attack()
    {
        float dis = Vector3.Distance(transform.position, enemy.transform.position);
        if (dis < AttDis)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit ray_cast_hit;
            if (Physics.Raycast(ray, out ray_cast_hit))
            {
                if (ray_cast_hit.transform.tag == "Enemy")
                {
                    Vector3 ExploPos = transform.position + new Vector3(0, 0, 3);
                    ray_cast_hit.rigidbody.AddExplosionForce(500f, ExploPos, 20f);
                }
            }
        }
    }

}
