using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class AudioMove : MonoBehaviour {

    private AudioSource audioSource;
    private Animator animator;
    public AudioClip walk, run, jump, attack;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (audioSource.clip == jump && audioSource.isPlaying == true)
            return;
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            audioSource.Stop();
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if (audioSource.clip != run)
                {
                    audioSource.clip = run;
                    audioSource.Play();
                    audioSource.loop = true;
                }
                if (audioSource.isPlaying == false)
                {
                    audioSource.Play();
                    audioSource.loop = true;
                }
            }
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            audioSource.loop = true;
            if (audioSource.clip != walk)
            {
                audioSource.clip = walk;
                audioSource.Play();
                audioSource.loop = true;
            }
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
                audioSource.loop = true;
            }
        }

        if (animator.GetBool("jump") == true)
        {
            audioSource.loop = false;
            if (audioSource.clip != jump)
            {
                audioSource.clip = jump;
                audioSource.Play();
            }
            if (audioSource.isPlaying == false)
                audioSource.Play();
        }

        if(Input.GetMouseButtonDown(0))
        {
            audioSource.loop = false;
            if (audioSource.clip != attack)
            {
                audioSource.clip = attack;
                audioSource.Play();
            }
            if (audioSource.isPlaying == false)
                audioSource.Play();
        }
    }
}
