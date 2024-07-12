using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public GameObject HP_Ninja;
    public GameObject[] Zombies;
    public Text tvEndGame;
    public Button btnPauseGame, btnPaused;
    public Sprite pause, continu;
    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        btnPauseGame.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MenuPauseGame");
        });
        
        btnPaused.onClick.AddListener(() =>
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                isPaused = true;
                btnPaused.GetComponent<Image>().sprite = continu;
            }
            else
            {
                Time.timeScale = 1;
                isPaused = false;
                btnPaused.GetComponent<Image>().sprite = pause;

            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        HP_Ninja = GameObject.Find("HP_Ninja");
        Zombies = GameObject.FindGameObjectsWithTag("Zombie");
        
        if (Zombies.Length == 0 && HP_Ninja.transform.localScale.x > 0)
        {
            tvEndGame.text = "Win!";
        }
    }
}
