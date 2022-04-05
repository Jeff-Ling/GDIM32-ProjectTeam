using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomListLauncher : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject roomInfo;
    [SerializeField] private GameObject connectingUI;
    [SerializeField] private Text roomName;

    private void Start()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
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
        RoomOptions options = new RoomOptions { MaxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom(roomName.text, options, default);
    }

    public override void OnJoinedRoom()
    {       
        PhotonNetwork.LoadLevel(9);
    }
}
