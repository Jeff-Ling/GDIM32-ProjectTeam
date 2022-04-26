using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomListLauncher : MonoBehaviourPunCallbacks
{
    [SerializeField] private string gameVersion = "1";
    [SerializeField] private GameObject roomInfo;
    [SerializeField] private GameObject connectingUI;
    [SerializeField] private Text roomName;

    private void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        roomInfo.SetActive(true);
        connectingUI.SetActive(false);
        PhotonNetwork.JoinLobby();
    }
    public void JoinOrCreateButton()
    {
        if(roomName.text.Length < 2)
        {
            return;
        }        
        PlayerPrefs.SetString("RoomName", roomName.text);
        PhotonNetwork.LoadLevel("WaitRoom");
        //SceneManager.LoadScene("WaitRoom");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("Disconnect");
    }
}
