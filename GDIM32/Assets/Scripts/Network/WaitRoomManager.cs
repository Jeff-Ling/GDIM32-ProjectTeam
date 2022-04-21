using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class WaitRoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject readyButton;
    private int playerNum;
    private GameObject playerChoosePref;
    private GameObject[] playerChoosePrefs;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void Start()
    {
        RoomOptions options = new RoomOptions { MaxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom(PlayerPrefs.GetString("RoomName"), options, default);       
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
    }
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playerNum = 0;
        }
        else
        {
            playerNum = 1;
        }
        //PhotonNetwork.Instantiate("PlayerChoose",Vector3.zero,Quaternion.identity).transform.SetParent(playerPanel.transform);
        if (playerNum == 0)
        {
            playerChoosePref = PhotonNetwork.Instantiate("P1Choose", Vector3.zero, Quaternion.identity, 0);
        }
        else
        {
            playerChoosePref = PhotonNetwork.Instantiate("P2Choose", Vector3.zero, Quaternion.identity, 0);
        }
        playerChoosePrefs = GameObject.FindGameObjectsWithTag("PlayerChoosePref");
        loadingPanel.SetActive(false);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(playerNum == 1)
        {
            playerNum = 0;
            PhotonNetwork.Destroy(playerChoosePref);
            playerChoosePref = PhotonNetwork.Instantiate("P1Choose", Vector3.zero, Quaternion.identity, 0);
            playerChoosePrefs = GameObject.FindGameObjectsWithTag("PlayerChoosePref");
        }
    }
    public void GunTypeButton()
    {
        for(int i=0; i<playerChoosePrefs.Length; i++)
        {
            if(playerChoosePrefs[i].GetPhotonView().IsMine)
            {
                if (playerChoosePrefs[i].transform.Find("Ready").gameObject.GetComponent<Text>().enabled)
                {
                    return;
                }
                playerChoosePrefs[i].transform.Find("Sheild").gameObject.GetComponent<Image>().enabled = false;
                playerChoosePrefs[i].transform.Find("Gun").gameObject.GetComponent<Image>().enabled = true;
                playerChoosePrefs[i].GetComponent<PlayerChoosePrefManager>().playerType = "Gun";
                PlayerPrefs.SetString("PlayerType", "Gun");
                return;
            }
        }
    }
    public void SheildTypeButton()
    {
        for (int i = 0; i < playerChoosePrefs.Length; i++)
        {
            if (playerChoosePrefs[i].GetPhotonView().IsMine)
            {
                if(playerChoosePrefs[i].transform.Find("Ready").gameObject.GetComponent<Text>().enabled)
                {
                    return;
                }
                playerChoosePrefs[i].transform.Find("Sheild").gameObject.GetComponent<Image>().enabled = true;
                playerChoosePrefs[i].transform.Find("Gun").gameObject.GetComponent<Image>().enabled = false;
                playerChoosePrefs[i].GetComponent<PlayerChoosePrefManager>().playerType = "Sheild";
                PlayerPrefs.SetString("PlayerType", "Sheild");
                return;
            }
        }
    }
    public void ReadyButton()
    {
        for (int i = 0; i < playerChoosePrefs.Length; i++)
        {
            if (playerChoosePrefs[i].GetPhotonView().IsMine)
            {
                playerChoosePrefs[i].transform.Find("Ready").gameObject.GetComponent<Text>().enabled = !playerChoosePrefs[i].transform.Find("Ready").gameObject.GetComponent<Text>().enabled;
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && playerChoosePrefs.Length != 2)
        {
            playerChoosePrefs = GameObject.FindGameObjectsWithTag("PlayerChoosePref");
        }
        if (PlayersHaveSameType())
        {
            for (int i = 0; i < playerChoosePrefs.Length; i++)
            {
                if (playerChoosePrefs[i].GetPhotonView().IsMine && !playerChoosePrefs[i].transform.Find("Ready").gameObject.GetComponent<Text>().enabled)
                {
                    readyButton.SetActive(false);
                    break;
                }
                else
                {
                    readyButton.SetActive(true);
                    break;
                }
            }
        }
        else
        {
            readyButton.SetActive(true);
        }

        if(BothPlayerReady() && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("chapter1-Multi");
        }
    }

    private bool PlayersHaveSameType()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount != 2)
        {
            return false;
        }
        return (playerChoosePrefs[0].GetComponent<PlayerChoosePrefManager>().playerType == playerChoosePrefs[1].GetComponent<PlayerChoosePrefManager>().playerType);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("Disconnect");
    }
    
    private bool BothPlayerReady()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount != 2)
        {
            return false;
        }
        return playerChoosePrefs[0].transform.Find("Ready").gameObject.GetComponent<Text>().enabled && playerChoosePrefs[1].transform.Find("Ready").gameObject.GetComponent<Text>().enabled;
    }
}
