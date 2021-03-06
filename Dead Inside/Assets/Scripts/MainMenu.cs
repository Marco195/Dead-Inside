﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


    private void Start()
    {
        AudioManager.instance.StopSound("Background");
    }

    //carrega a primeira fase
    public void PlayGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //sai do jogo
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
