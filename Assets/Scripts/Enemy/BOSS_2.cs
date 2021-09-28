using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BOSS_2 : MonoBehaviour {

    public GameObject Wizard_1, Wizard_2, Wizard_3, bar1, bar2, bar3;
    public GameObject player, magicRune3, atk;
    public Light sun;

    public AudioSource audioSource;
    public AudioListener ear;

    float count;
    public RawImage WinWord, fadeOut, fadeOut_2;

    private Animation anim;
    private Rigidbody rid;
    private GameObject magicRune;
    private bool start = true, isATK = false, scaleForDie = false;

    // Use this for initialization
    void Start()
    {
        audioSource.GetComponent<AudioSource>();

        fadeOut.color = Color.clear;
        fadeOut.enabled = false;
        fadeOut_2.enabled = false;
        WinWord.transform.localPosition = new Vector3(0, 400, 0);

        bar1.transform.localPosition += new Vector3(0, 400, 0);
        bar2.transform.localPosition += new Vector3(0, 400, 0);
        bar3.transform.localPosition += new Vector3(0, 400, 0);
        anim = GetComponent<Animation>();
        rid = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update () {
        if (anim.IsPlaying("ATK_roar"))
            audioSource.Play();
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (ear.enabled == true)
                ear.enabled = false;
            else
                ear.enabled = true;
        }

        if (scaleForDie && Boss.blood <= 0 && !anim.isPlaying)
        {
            if (count > 30)
            {
                fadeOut_2.enabled = true;
                fadeOut_2.color = Color.Lerp(fadeOut_2.color, Color.black, Time.deltaTime);
                if (fadeOut_2.color.a > 0.99)
                    SceneManager.LoadScene(0);
            }
            else if (count > 5)
            {
                fadeOut.enabled = true;
                fadeOut.color = Color.Lerp(fadeOut.color, Color.black, 1.5f * Time.deltaTime);
                if (count <= 15)
                    fadeOut_2.color = fadeOut.color;
                if (count > 15)
                {
                    WinWord.transform.localPosition = new Vector3(0, 0, 0);
                    fadeOut_2.enabled = true;
                    fadeOut_2.color = Color.Lerp(fadeOut_2.color, Color.clear, 2 * Time.deltaTime);
                }
            }
            count = count + 0.1f;
        }
        if (scaleForDie)
        {
            magicRune.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        }
        else if (Jason_Moving_2_1.blood > 0 && Boss.blood<=0)
        {
            anim.CrossFade("ATK_roar");
            anim.CrossFadeQueued("Die");
            scaleForDie = true;
        }
        if (!scaleForDie && !Boss.BossMode1)
        {
            StartCoroutine(WizardAttack());
            if(!isATK)
                StartCoroutine(FrequentlyAttack());
            var lookpos = player.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(new Vector3(lookpos.x, 0, lookpos.z), new Vector3(0, 1, 0));
            if (start)
            {
                sun.color = Color.Lerp(sun.color, Color.black, 2.5f * Time.deltaTime);
                anim.CrossFade("ATK_roar");
                anim.CrossFadeQueued("ATK_hit");
                anim.CrossFadeQueued("ATK_roar");
                rid.velocity += new Vector3(0, 2, 0);
                if(transform.localPosition.y>10.9999)
                    StartCoroutine(createWizard());
            }
            if (transform.localPosition.y > 11)
            {
                start = false;
                rid.velocity = new Vector3(0, 0, 0);
                rid.useGravity = false;
                magicRune.transform.localPosition = transform.localPosition;
            }
            if (!anim.isPlaying)
            {
                anim.Play("Walk");
                rid.AddRelativeForce(0, 0, 50);
            }
        }
    }

    private IEnumerator FrequentlyAttack()
    {
        isATK = true;
        int ATK_ran_type = Random.Range(1, 3);
        var lookpos = player.transform.position - transform.position;
        switch (ATK_ran_type)
        {
            case 1:
                var rotation = Quaternion.LookRotation(lookpos, Vector3.up);
                anim.CrossFade("ATK_throw");
                StartCoroutine(DelayAtk(atk, player.transform.position, rotation, 0.5f));
                break;
            case 2:
                anim.CrossFadeQueued("ATK_roar");
                anim.CrossFadeQueued("ATK_hit");
                anim.CrossFadeQueued("ATK_roar");
                break;
        }
        yield return new WaitForSeconds(5f);
        isATK = false;
    }

    private IEnumerator createWizard()
    {
        yield return new WaitForSeconds(0.5f);
        magicRune = Instantiate(magicRune3);
        magicRune.transform.localPosition = transform.localPosition;
        yield return new WaitForSeconds(2f);
        var lookpos = player.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(lookpos.x, 0, lookpos.z), new Vector3(0, 1, 0));
        Wizard_1.transform.localPosition = transform.localPosition + new Vector3(0, -11.5f, -7);
        Wizard_1.transform.localRotation = new Quaternion(player.transform.localRotation.x, player.transform.localRotation.y + 180f, player.transform.localRotation.z, player.transform.localRotation.w);
        Wizard_2.transform.localPosition = transform.localPosition + new Vector3(10, -11.5f, 0);
        Wizard_2.transform.localRotation = new Quaternion(player.transform.localRotation.x, player.transform.localRotation.y + 180f, player.transform.localRotation.z, player.transform.localRotation.w);
        Wizard_3.transform.localPosition = transform.localPosition + new Vector3(0, -11.5f, 7);
        Wizard_3.transform.localRotation = new Quaternion(player.transform.localRotation.x, player.transform.localRotation.y + 180f, player.transform.localRotation.z, player.transform.localRotation.w);

        bar1.transform.localPosition -= new Vector3(0, 400, 0);
        bar2.transform.localPosition -= new Vector3(0, 400, 0);
        bar3.transform.localPosition -= new Vector3(0, 400, 0);
    }

    private IEnumerator WizardAttack()
    {
        yield return new WaitForSeconds(2f);
        Wizard_Moving_1.WizardATK = (Random.Range(-1, 1) == 0) ? true : false;
        Wizard_Moving_2.WizardATK = (Random.Range(-1, 1) == 0) ? true : false;
        Wizard_Moving_3.WizardATK = (Random.Range(-1, 1) == 0) ? true : false;
    }
    
    private IEnumerator DelayAtk(GameObject atk, Vector3 position, Quaternion rotation, float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(atk, position, rotation);
    }
}
