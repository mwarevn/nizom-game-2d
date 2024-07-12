using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControll : MonoBehaviour
{
    public Button btnContinue, btnReplay, btnNewGame;
    // Start is called before the first frame update
    void Start()
    {
        btnContinue.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("ChooseMode");
        });
        
        btnReplay.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("LevelEasy");
        });
        
        btnNewGame.onClick.AddListener(() =>
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("NewGame");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
