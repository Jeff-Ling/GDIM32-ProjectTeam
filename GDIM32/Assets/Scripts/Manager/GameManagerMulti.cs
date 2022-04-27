using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManagerMulti : MonoBehaviourPunCallbacks
{
    [TextArea]
    [SerializeField] private string[] prologue;
    [SerializeField] private float prologuePlayInterval = 1f;
    [SerializeField] private string nextLevel;
    private UIManager uiManager;
    private GameObject[] players = new GameObject[2];

    void Start()
    {
        uiManager = GetComponent<UIManager>();
        StartCoroutine(GameFlow());
    }

    protected virtual IEnumerator GameFlow()
    {
        yield return StartCoroutine(InstantiatePlayer());
        yield return StartCoroutine(ShowScene());
        yield return StartCoroutine(Prologue());
        yield return StartCoroutine(GamePlaying());
        yield return StartCoroutine(GameEnd());
    }

    private IEnumerator InstantiatePlayer()
    {
        InstantiatePlayer(PlayerPrefs.GetString("PlayerType"));       
        while (!GameObject.FindGameObjectWithTag("Player1"))
        {
            yield return null;
        }
        players[0] = GameObject.FindGameObjectWithTag("Player1");
        Debug.Log(1);
        while (!GameObject.FindGameObjectWithTag("Player2"))
        {
            yield return null;
        }
        players[1] = GameObject.FindGameObjectWithTag("Player2");
        Debug.Log(2);
        players[0].GetComponent<PlayerManager>().Enable = false;
        players[1].GetComponent<PlayerManager>().Enable = false;
    }
    private IEnumerator ShowScene()
    {
        Debug.Log(3);
        UIManager.Instance.CloseLoadingUI();
        yield return null;
    }
    private IEnumerator Prologue()
    {
        int i = 0;
        while (i < prologue.Length)
        {
            uiManager.ShowPrologue(prologue[i]);
            yield return new WaitForSeconds(prologuePlayInterval);
            i++;
        }
        uiManager.ClosePrologue();
    }
    private IEnumerator GamePlaying()
    {
        players[0].GetComponent<PlayerManager>().Enable = true;
        players[1].GetComponent<PlayerManager>().Enable = true;
        while (!WinCondition() && !LoseCondition())
        {
            yield return null;
        }
    }
    private IEnumerator GameEnd()
    {
        if(WinCondition())
        {
            PhotonNetwork.LoadLevel(nextLevel);
            yield return null;
        }
        if(LoseCondition())
        {
            if(PhotonNetwork.IsMasterClient)
            {
                uiManager.ShowRetryUI();
            }
            else
            {
                uiManager.ShowWaitForMasterChooseUI();
            }
        }
    }
    private void InstantiatePlayer(string type)
    {
        if (type == "Gun")
        {
            PhotonNetwork.Instantiate("PlayerFOV_Gun", new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), 0);
            PhotonNetwork.Instantiate("Player_Gun", new Vector3(4f, 0, 0), Quaternion.Euler(0, 0, 90), 0);
        }
        if(type == "Sheild")
        {
            PhotonNetwork.Instantiate("PlayerFOV_Sheild", new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), 0);
            PhotonNetwork.Instantiate("Player_Sheild", new Vector3(-4f, 0, 0), Quaternion.Euler(0, 0, 90), 0);
        }
    }
    protected virtual bool WinCondition()
    {
        return false;
    }
    protected virtual bool LoseCondition()
    {
        return players[0].GetComponent<PlayerStats>().IsDead || players[1].GetComponent<PlayerStats>().IsDead;
    }
    public void RetryButton()
    {
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().ToString());
    }
    public void QuitButton()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Menu");        
    }
}
