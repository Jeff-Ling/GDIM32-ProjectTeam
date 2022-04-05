using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class RoomNameButtom : MonoBehaviour
{
    public RoomInfo roomInfo;
    public void ClickButton()
    {
        Debug.Log(roomInfo.Name);
        GameObject.FindGameObjectWithTag("InputField").GetComponent<InputField>().text = roomInfo.Name;
    }
}
