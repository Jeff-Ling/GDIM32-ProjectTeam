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
        GameObject.FindGameObjectWithTag("InputField").GetComponent<InputField>().text = roomInfo.Name;
    }
}
