using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Created by Y. Song at 2022/02/02

public class mainMenu : MonoBehaviour
{
    public void playButton ()
    {
        SceneManager.LoadScene("MAP");
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitButton()
    {
        Debug.Log("Quit Success.");
        Application.Quit();
    }
}

