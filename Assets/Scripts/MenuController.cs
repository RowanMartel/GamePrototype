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
        doc.rootVisualElement.visible = false;
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
                InventoryUIController.instance.RefreshUI();
                doc.rootVisualElement.visible = true;
                Global.MenuOpen = true;
            }
        }
    }
}