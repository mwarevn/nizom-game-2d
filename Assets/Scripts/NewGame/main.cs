using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class main : MonoBehaviour
{
    public Text label;
    public InputField edName;
    public Button btnStartNameGame;
    void Start()
    {

        if (PlayerPrefs.GetString("player_name") != "")
        {
            edName.text = PlayerPrefs.GetString("player_name");
        }
        
        btnStartNameGame.onClick.AddListener(() =>
        {
            if (edName.text != "")
            {
                PlayerPrefs.SetString("player_name", edName.text);
                SceneManager.LoadScene("ChooseMode");
            }
            else
                label.text = "* Please enter name";
        });
    }
}
