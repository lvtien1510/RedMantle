using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private bool isNewGame;
    private void Awake()
    {
        isNewGame = bool.Parse(PlayerPrefs.GetString("IsNewGame", "true"));
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Lv1");
        isNewGame = false;
        PlayerPrefs.SetString("IsNewGame", isNewGame.ToString());
    }
    public void Resume()
    {
        if (isNewGame)
        {
            NewGame();
        }
        else
        {
            SceneManager.LoadScene("Lv2");
        }
        
    }
}
