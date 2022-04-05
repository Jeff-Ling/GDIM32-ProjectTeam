using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerMulti : Singleton<GameManagerMulti>
{
    private UIManager uiManager;
    private bool gameStart = false;

    public int CheckPlayerNumbers()
    {
        return GameObject.FindGameObjectsWithTag("Player").Length;
    }
    void Start()
    {
        uiManager = GetComponent<UIManager>();
        StartCoroutine(WaitForPlayers());
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameStart)
        {
            if(CheckPlayerNumbers() == 2)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                players[0].GetComponent<PlayerManager>().Enable = true;
                players[1].GetComponent<PlayerManager>().Enable = true;
                UIManager.Instance.CloseLoadingUI();
                gameStart = true;
            }
        }
    }

    private IEnumerator WaitForPlayers()
    {
        while(CheckPlayerNumbers() != 2)
        {
            yield return null;
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        players[0].GetComponent<PlayerManager>().Enable = true;
        players[1].GetComponent<PlayerManager>().Enable = true;
        Transform[] playersTransform = new Transform[2];
        playersTransform[0] = players[0].transform;
        playersTransform[1] = players[1].transform;
        CameraControl.Instance.m_Targets = playersTransform;
        UIManager.Instance.CloseLoadingUI();
    }
}
