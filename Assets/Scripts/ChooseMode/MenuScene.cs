using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour
{
    public Button btnEasy, btnHard;
    // Start is called before the first frame update
    void Start()
    {

        btnEasy.onClick.AddListener(runLvEasy);
        
        btnHard.onClick.AddListener(runLvHard);


    }

    void runLvEasy()
    {
        PlayerPrefs.SetInt("mode", 0);
        SceneManager.LoadScene("LevelEasy");
    }

    void runLvHard()
    {
        PlayerPrefs.SetInt("mode", 1);
        SceneManager.LoadScene("LevelEasy");
    }
}
