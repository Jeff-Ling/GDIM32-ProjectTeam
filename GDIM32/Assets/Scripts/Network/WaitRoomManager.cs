using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class WaitRoomManager : MonoBehaviourPunCallbacks
{
    private int playerNum;

    void Start()
    {
        playerNum = PhotonNetwork.CountOfPlayersInRooms;
        if (playerNum == 0)
        {
            if(GameObject.FindGameObjectWithTag("PlayerChoosePref") != null){ return; }
            PhotonNetwork.Instantiate("P1Choose", Vector3.zero, Quaternion.identity, 0);
        }
        else
        {
            if (GameObject.FindGameObjectsWithTag("PlayerChoosePref").Length == 1) { return; }
            if (GameObject.FindGameObjectWithTag("PlayerChoosePref").name == "P1Choose")
            {
                PhotonNetwork.Instantiate("P2Choose", Vector3.zero, Quaternion.identity, 0);
            }
            else
            {
                PhotonNetwork.Instantiate("P1Choose", Vector3.zero, Quaternion.identity, 0);
            }
        }
    }

    public void GunTypeButton()
    {
        GameObject[] playerChoosePref = GameObject.FindGameObjectsWithTag("PlayerChoosePref");
        for(int i=0; i<playerChoosePref.Length; i++)
        {
            if(playerChoosePref[i].GetPhotonView().IsMine)
            {
                if (playerChoosePref[i].transform.Find("Ready").gameObject.activeSelf)
                {
                    return;
                }
                playerChoosePref[i].transform.Find("Sheild").gameObject.SetActive(false);
                playerChoosePref[i].transform.Find("Gun").gameObject.SetActive(true);
                return;
            }
        }
    }
    public void SheildTypeButton()
    {
        GameObject[] playerChoosePref = GameObject.FindGameObjectsWithTag("PlayerChoosePref");
        for (int i = 0; i < playerChoosePref.Length; i++)
        {
            if (playerChoosePref[i].GetPhotonView().IsMine)
            {
                if(playerChoosePref[i].transform.Find("Ready").gameObject.activeSelf)
                {
                    return;
                }
                playerChoosePref[i].transform.Find("Sheild").gameObject.SetActive(true);
                playerChoosePref[i].transform.Find("Gun").gameObject.SetActive(false);
                return;
            }
        }
    }
    public void ReadyButton()
    {
        GameObject[] playerChoosePref = GameObject.FindGameObjectsWithTag("PlayerChoosePref");
        for (int i = 0; i < playerChoosePref.Length; i++)
        {
            if (playerChoosePref[i].GetPhotonView().IsMine)
            {
                playerChoosePref[i].transform.Find("Ready").gameObject.SetActive(!playerChoosePref[i].transform.Find("Ready").gameObject.activeSelf);
                return;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {        
        if (PlayersHaveSameType())
        {
            GameObject[] playerChoosePref = GameObject.FindGameObjectsWithTag("PlayerChoosePref");
            for (int i = 0; i < playerChoosePref.Length; i++)
            {
                if (playerChoosePref[i].GetPhotonView().IsMine && !playerChoosePref[i].transform.Find("Ready").gameObject.activeSelf)
                {
                    GameObject.FindGameObjectWithTag("ReadyButton").GetComponent<Button>().interactable = false;
                }
                else
                {
                    GameObject.FindGameObjectWithTag("ReadyButton").GetComponent<Button>().interactable = true;
                }
            }
        }
        else
        {
            GameObject.FindGameObjectWithTag("ReadyButton").GetComponent<Button>().interactable = true;
        }
    }

    private bool PlayersHaveSameType()
    {
        if(PhotonNetwork.CountOfPlayers != 2)
        {
            return false;
        }
        GameObject[] playerChoosePref = GameObject.FindGameObjectsWithTag("PlayerChoosePref");
        return (playerChoosePref[0].transform.Find("Sheild").gameObject.activeSelf == true && playerChoosePref[1].transform.Find("Sheild").gameObject.activeSelf == true) 
            || (playerChoosePref[0].transform.Find("Gun").gameObject.activeSelf == true && playerChoosePref[1].transform.Find("Gun").gameObject.activeSelf == true);
    }
}
