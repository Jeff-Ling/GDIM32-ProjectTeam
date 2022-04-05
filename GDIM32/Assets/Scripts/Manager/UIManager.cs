using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject m_LoadingUI;
    public void CloseLoadingUI()
    {
        m_LoadingUI.SetActive(false);
    }
}
