using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveSetting : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
    private string roomName = "Room 1";
    public void MoveToMultiScene(string characterType)
    {
        PlayerPrefs.SetString("Room Name", roomName);
        PlayerPrefs.SetString("Character Type", characterType);
        SceneManager.LoadScene("chapter1-Multi");
    }
    public void OnValueChanged()
    {
        Debug.Log(roomName);
        roomName = dropdown.options[dropdown.value].text;
    }
}
