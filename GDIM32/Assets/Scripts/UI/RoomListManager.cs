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
    Dictionary<string, RoomInfo> myRoomList = new Dictionary<string, RoomInfo>();
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for(int i = 0; i<gridLayout.childCount;i++)
        {
            Destroy(gridLayout.GetChild(i).gameObject);
        }
        foreach (var r in roomList)
        {
            if (!r.IsOpen || !r.IsVisible || r.RemovedFromList)
            {
                if (myRoomList.ContainsKey(r.Name))
                {
                    myRoomList.Remove(r.Name);
                }
                continue;
            }
            if (myRoomList.ContainsKey(r.Name))
            {
                myRoomList[r.Name] = r;
            }
            else
            {
                myRoomList.Add(r.Name, r);
            }
        }
        foreach (var room in myRoomList.Values)
        {
            GameObject newRoom = Instantiate(roomNamePrefab, gridLayout.position, Quaternion.identity, gridLayout);
            newRoom.GetComponentInChildren<Text>().text = room.Name + "("+ room.PlayerCount +"/"+ room.MaxPlayers + ")";
            newRoom.GetComponent<RoomNameButtom>().roomInfo = room;
        }
    }
}
