using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject pauseUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseUI.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Reload()
    {
        SceneManager.LoadScene(0);
        Resume();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
