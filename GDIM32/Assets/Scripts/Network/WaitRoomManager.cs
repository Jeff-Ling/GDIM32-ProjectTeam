using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class WaitRoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject readyButton;
    [SerializeField] private GameObject[] readyText;
    [SerializeField] private GameObject[] gunImage;
    [SerializeField] private GameObject[] sheildImage;
    private int playerNum;
    public void ReadyButton()
    {
        readyText[playerNum].SetActive(!readyText[playerNum].activeSelf);
        for (int i = 0; i < readyText.Length; i++)
        {
            if (!readyText[i].activeSelf)
            {
                return;
            }       
        }
        PhotonNetwork.LoadLevel(7);
    }
    public void GunTypeButton()
    {
        gunImage[playerNum].SetActive(true);
        sheildImage[playerNum].SetActive(false);
    }
    public void SheildTypeButton()
    {
        gunImage[playerNum].SetActive(false);
        sheildImage[playerNum].SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
      
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.ActorNumber);
    }
    


    // Update is called once per frame
    void Update()
    {
        /*if(PlayersHaveSameType() && !readyText[playerNum].activeSelf)
        {
            readyButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            readyButton.GetComponent<Button>().interactable = true;
        }*/
    }
    private bool PlayersHaveSameType()
    {
        return (gunImage[0].activeSelf && gunImage[1].activeSelf) || (sheildImage[0].activeSelf && sheildImage[1].activeSelf);
    }
}
