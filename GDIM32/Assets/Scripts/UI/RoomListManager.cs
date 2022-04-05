using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomListManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject roomNamePrefab;
    [SerializeField] private Transform gridLayout;
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for(int i = 0; i<gridLayout.childCount;i++)
        {
            if(gridLayout.GetChild(i).gameObject.GetComponentInChildren<Text>().text == roomList[i].Name)
            {
                Destroy(gridLayout.GetChild(i).gameObject);
            }
        }
        foreach(var room in roomList)
        {
            GameObject newRoom = Instantiate(roomNamePrefab, gridLayout.position, Quaternion.identity,gridLayout);
            newRoom.GetComponentInChildren<Text>().text = room.Name + "("+ room.PlayerCount +"/"+ room.MaxPlayers + ")";
            newRoom.GetComponent<RoomNameButtom>().roomInfo = room;
        }
    }
}
