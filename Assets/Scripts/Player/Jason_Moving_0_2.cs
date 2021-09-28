using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]

public class Jason_Moving_0_2 : MonoBehaviour
{

    private AudioSource audioSource;
    private Animator animator;
    public AudioClip audio_walk, audio_run, audio_jump, audio_attack;
    public GameObject magicRune3;
    GameObject magicRune;
    float count;

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
    int StepCount = 0;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        rid = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag_2 == null)
        {
            if (count > 5 && magicRune == null)
            {
                magicRune = Instantiate(magicRune3);
                magicRune.transform.localPosition = transform.localPosition + new Vector3(0, 1.5f, 3);
            }
            count = count + 0.1f;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            pressWASD = 30;
        if (StepCount < 60)
        {
            //先走幾步
            if (audioSource.clip != audio_walk || audioSource.isPlaying == false)
            {
                animator.SetFloat("speed", 1);
                animator.SetBool("jump", false);
                audioSource.clip = audio_walk;
                audioSource.Play();
                audioSource.loop = true;
            }
            rid.AddRelativeForce(0, 0, 45 - StepCount);
            StepCount++;
            return;
        }
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
                walk();
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
                run();
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
                walk();
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
                run();
                break;
        }
        if (flag == null || transform.position.z <= 75)
        {
            jump();
        }
    }

    public int state()
    {
        // 0 idle , 1 walk , 2 run , 3  hit  //4 4 walk & attack  //5 run & attack
        if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //前進
        {
            if (Input.GetMouseButtonDown(0))
            {
                return 5;
            }
            return 2;
        }
        else if (Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //左移
        {
            if (Input.GetMouseButtonDown(0))
            {
                return 5;
            }
            return 2;
        }
        else if (Input.GetKey(KeyCode.D) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //右移
        {
            if (Input.GetMouseButtonDown(0))
            {
                return 5;
            }
            return 2;
        }
        else if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) //後退
        {
            if (Input.GetMouseButtonDown(0))
            {
                return 5;
            }
            return 2;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetMouseButtonDown(0))
            {
                return 4;
            }
            return 1;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            return 3;
        }
        else
        {
            return 0;
        }
    }

    public void walk()
    {
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

    public void run()
    {
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

    }

    void OnCollisionEnter(Collision c)
    {
        isjump = false;
    }

}
