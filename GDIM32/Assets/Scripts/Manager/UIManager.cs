using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject m_LoadingUI;
    [SerializeField] private GameObject m_PrologueUI;
    [SerializeField] private Text m_PrologueText;
    [SerializeField] private GameObject m_RetryUI;
    [SerializeField] private GameObject m_WaitForMasterChooseUI;
    public void CloseLoadingUI()
    {
        m_LoadingUI.SetActive(false);
    }
    public void ShowPrologue(string text)
    {
        m_PrologueUI.SetActive(true);
        m_PrologueText.text = text;
    }
    public void ClosePrologue()
    {
        m_PrologueUI?.SetActive(false);
    }
    public void ShowRetryUI()
    {
        m_RetryUI.SetActive(true);
    }
    public void ShowWaitForMasterChooseUI()
    {
        m_WaitForMasterChooseUI.SetActive(true);
    }
}
