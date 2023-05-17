using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TitleMenu : MonoBehaviour
{
    VisualElement m_Root;
    VisualElement m_StartBtn;
    VisualElement m_CloseBtn;

    void Start()
    {
        m_Root = GetComponent<UIDocument>().rootVisualElement;
        m_StartBtn = m_Root.Q<VisualElement>("Play");
        m_StartBtn.RegisterCallback<ClickEvent>(StartGame);
        m_CloseBtn = m_Root.Q<VisualElement>("Quit");
        m_CloseBtn.RegisterCallback<ClickEvent>(CloseGame);
        OpenMenu();
    }

    void Update()
    {
        
    }

    void OpenMenu()
    {
        m_Root.visible = true;
        Time.timeScale = 0;
    }

    void StartGame(ClickEvent evt)
    {
        m_Root.visible = false;
        Time.timeScale = 1;
    }
    void CloseGame(ClickEvent evt)
    {
        Application.Quit();
    }
}