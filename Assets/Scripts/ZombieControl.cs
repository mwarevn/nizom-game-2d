using System;
using UnityEngine;
using UnityEngine.UI;

public class ZombieControl : MonoBehaviour
{
    private Animator animChar, animZombie;
    private GameObject zom;
    public GameObject HP_line;
    public AudioSource audioSource;
    private int point;
    public Text tvKilled;
    public int isHardMode;

    void Start()
    {
        isHardMode = PlayerPrefs.GetInt("mode");
        point = PlayerPrefs.GetInt("point");
        tvKilled.text = point + "";
        zom = gameObject;
        animZombie = zom.GetComponent<Animator>();
        animChar = GameObject.Find("Char").GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isHardMode == 1)
        {
            if (other.gameObject.CompareTag("Char"))
            {
                animZombie.Play("ZombieAttack");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Char"))
        {
            animZombie.Play("ZombieIdle");
        }
    }

    void zombieDead()
    {
        point++;
        PlayerPrefs.SetInt("point", point);
        tvKilled.text = point + "";
        audioSource.Play();
        animZombie.Play("ZombieDead");
        Destroy(zom, 1f);
        Debug.Log("Zombie dead..............");
        
    }

    void zombieHurt(Collider2D other)
    {
        // neu ninja chem
        if (other.gameObject.CompareTag("Char") && animChar.GetCurrentAnimatorStateInfo(0).IsName("CharAttack"))
        {
            if (HP_line.transform.localScale.x <= 0)
            {
                zombieDead();
            }
            else
            {
                Debug.Log("Hurt zombie....");
                HP_line.transform.localScale -= new Vector3(4f * Time.deltaTime, 0, 0);
                HP_line.transform.position -= new Vector3(0.60f * Time.deltaTime, 0, 0);
            }
        }
        
        // neu roi xuong ho
        if (other.gameObject.CompareTag("Lake"))
        {
            HP_line.transform.localScale = new Vector3(0, 0, 0);
            Debug.Log("Fall down lake...");
            zombieDead();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        zombieHurt(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        zombieHurt(other);
    }
}