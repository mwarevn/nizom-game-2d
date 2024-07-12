using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class handleChar : MonoBehaviour
{
    public float moveSpeed = 5f;
    private GameObject nv;
    public Text player_name, tvEndGame;
    public GameObject HP_Ninja;
    public AudioSource audioSrc;
    public Animator anim;
    private int hardMode;

    public AudioClip deadSrc, hurtSrc;
    // Start is called before the first frame update
    void Start()
    {
        hardMode = PlayerPrefs.GetInt("mode");
        nv = GameObject.Find("Char");
        audioSrc = nv.GetComponent<AudioSource>();
        player_name.text = PlayerPrefs.GetString("player_name");
        Debug.Log("Name is: " + PlayerPrefs.GetString("player_name"));
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Áp dụng lực để di chuyển nhân vật
        nv.transform.position = nv.transform.position + new Vector3(moveX * moveSpeed * Time.deltaTime,
            moveY * moveSpeed * Time.deltaTime, 0);
        
        // name moving with player
        player_name.transform.position = new Vector3(nv.transform.position.x, nv.transform.position.y + 1f, 0);
        
        // Áp dụng animation di chuyển
        
        // Quay sang trái
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.Play("CharRun");
            nv.transform.rotation = new Quaternion(0, 1, 0, 0);
        }

        // quay sang phải
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.Play("CharRun");
            nv.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.Play("CharJump");
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.Play("CharSlide");
        }
        
        // đứng khi dừng chạy
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow)  || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetMouseButtonUp(0))
        {
            anim.Play("CharIdle");
        }

        // Click chuột thì chém
        if (Input.GetMouseButtonDown(0))
        {
            anim.Play("CharAttack");
            nv.GetComponent<BoxCollider2D>().enabled = true;
        }
        
        // Click chuột thì chém
        if (Input.GetMouseButtonUp(0))
        {
            nv.GetComponent<BoxCollider2D>().enabled = false;
        }
        
    }

    


    void playerHurt()
    {
        if (HP_Ninja.transform.localScale.x <= 0)
        {
            playerDead();
            return;
        }
        

        audioSrc.clip = hurtSrc;
        audioSrc.Play();
        
        
        if (hardMode == 1)
        {
            HP_Ninja.transform.localScale -= new Vector3(40 * Time.deltaTime, 0, 0);
            HP_Ninja.transform.position -= new Vector3(4 * Time.deltaTime, 0, 0);
        }
        else
        {
            HP_Ninja.transform.localScale -= new Vector3(20 * Time.deltaTime, 0, 0);
            HP_Ninja.transform.position -= new Vector3(2 * Time.deltaTime, 0, 0);
        }
        
    }
    
    void playerDead()
    {
        audioSrc.clip = deadSrc;
        audioSrc.Play();
        anim.Play("CharDead");
        Destroy(nv.gameObject, 1);
        tvEndGame.text = "game over";
        Debug.Log("Char dead.........................................");
        SceneManager.LoadScene("MenuPauseGame");

    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Lake"))
        {
            Debug.Log("Char Falldown on lake...");
            HP_Ninja.transform.localScale = new Vector3(0, 0, 0);
            playerDead();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
            if (other.gameObject.CompareTag("cay_nam"))
            {
                playerHurt();
            }

            if (other.gameObject.CompareTag("Zombie") && hardMode == 1)
            {
                playerHurt();
            }
        
    }
    
}
