using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        if (PlayerPrefs.GetString("Room Name") != string.Empty)
        {
            if(!PhotonNetwork.JoinOrCreateRoom(PlayerPrefs.GetString("Room Name"), new Photon.Realtime.RoomOptions() { MaxPlayers = 2 }, default))
            {
                SceneManager.LoadScene("RoomIsFull");
            }
        }
        else
        {
            if (!PhotonNetwork.JoinRandomOrCreateRoom(expectedMaxPlayers: 2))
            {
                SceneManager.LoadScene("RoomIsFull");
            }
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if (PlayerPrefs.GetString("Character Type") == "Sheild")
        {
            InstantiateSheildPlayer();
        }
        if (PlayerPrefs.GetString("Character Type") == "Gun")
        {
            InstantiateGunPlayer();
        }
        if (PlayerPrefs.GetString("Character Type") == "Random")
        {
            if(GameObject.FindGameObjectWithTag("Player") == null)
            {
                if(Random.Range(0,2) < 1)
                {
                    InstantiateSheildPlayer();
                }
                else
                {
                    InstantiateGunPlayer();
                }
            }
            else if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Type == PlayerStats.PlayerType.Gun)
            {
                InstantiateSheildPlayer();
            }
            else
            {
                InstantiateGunPlayer();
            }
        }
    }

    private void InstantiateGunPlayer()
    {
        GameObject gunPlayer = PhotonNetwork.Instantiate("Player_Gun", new Vector3(4f, 0, 0), Quaternion.Euler(0, 0, 90), 0) as GameObject;
        gunPlayer.GetComponent<PlayerManager>().Enable = false;
    }
    private void InstantiateSheildPlayer()
    {
        GameObject gunPlayer = PhotonNetwork.Instantiate("Player_Sheild", new Vector3(-4f, 0, 0), Quaternion.Euler(0, 0, 90), 0) as GameObject;
        gunPlayer.GetComponent<PlayerManager>().Enable = false;
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("Disconnect");
    }
}
