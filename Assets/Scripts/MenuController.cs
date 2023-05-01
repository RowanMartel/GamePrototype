using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    UIDocument doc;

    void Start()
    {
        doc = GetComponent<UIDocument>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Global.MenuOpen)
            {
                doc.rootVisualElement.visible = false;
                Global.MenuOpen = false;
            }
            else
            {
                doc.rootVisualElement.visible = true;
                Global.MenuOpen = true;
            }
        }
    }
}