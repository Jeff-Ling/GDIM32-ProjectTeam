using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

////////////////////////////////////
//Created by Y. Song at 2022.02.02//
////////////////////////////////////

public class menuControl : MonoBehaviour
{
    public static bool isPaused = false; 

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == true)
            {
                resumeButton();
            }
            else
            {
                pause();
            }
        }
    }

    public void playButton()
    {
        SceneManager.LoadScene("chapter1");
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void multiButton()
    {
        SceneManager.LoadScene("RoomList");
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void quitButton()
    {
        Debug.Log("Quit Success.");
        Application.Quit();
    }

    public void resumeButton()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void returnButton()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
        SceneManager.LoadScene("Menu");
    }

    public void creditButton()
    {
        SceneManager.LoadScene("credit");
    }

    void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
