using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleMenu : MonoBehaviour
{
    VisualElement m_Root;
    VisualElement m_StartBtn;
    VisualElement m_CloseBtn;

    [SerializeField]
    UIDocument n_Document;
    VisualElement n_Root;
    VisualElement n_ContinueBtn;
    Label n_Score;
    string scoreTextBase;

    void Start()
    {
        m_Root = GetComponent<UIDocument>().rootVisualElement;
        m_StartBtn = m_Root.Q<VisualElement>("Play");
        m_StartBtn.RegisterCallback<ClickEvent>(StartGame);
        m_CloseBtn = m_Root.Q<VisualElement>("Quit");
        m_CloseBtn.RegisterCallback<ClickEvent>(CloseGame);

        n_Root = n_Document.rootVisualElement;
        n_ContinueBtn = n_Root.Q<VisualElement>("Continue");
        n_ContinueBtn.RegisterCallback<ClickEvent>(Continue);
        n_Score = n_Root.Q<Label>("Score");
        scoreTextBase = n_Score.text;
        n_Root.visible = false;

        OpenMenu();
    }

    void Continue(ClickEvent evt)
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    void OpenMenu()
    {
        m_Root.visible = true;
        Time.timeScale = 0;
        Global.MenuOpen = true;
    }

    void StartGame(ClickEvent evt)
    {
        BGM.StartBGM();
        m_Root.visible = false;
        Time.timeScale = 1;
        Global.MenuOpen = false;
    }
    void CloseGame(ClickEvent evt)
    {
        Application.Quit();
    }

    public void EndScreen()
    {
        BGM.StopBGM();
        Time.timeScale = 0;
        Global.MenuOpen = true;
        n_Root.visible = true;
        n_Score.text = scoreTextBase + Score.score;
    }
}