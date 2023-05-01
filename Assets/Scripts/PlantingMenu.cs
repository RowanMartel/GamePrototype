using KinematicCharacterController.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class PlantingMenu : MonoBehaviour
{
    private VisualElement m_Root;
    private VisualElement m_PlantSeed;
    private VisualElement m_Remove_Plant;
    private VisualElement m_Cancel;

    private PlantingSpot CurrentSpot;

    void Start()
    {
        m_Root = GetComponent<UIDocument>().rootVisualElement;
        m_PlantSeed = m_Root.Q<VisualElement>("Plant");
        m_Remove_Plant = m_Root.Q<VisualElement>("Remove");
        m_Cancel = m_Root.Q<VisualElement>("Cancel");
        m_PlantSeed.RegisterCallback<PointerDownEvent>(PlantSeedMenu);
        m_Remove_Plant.RegisterCallback<PointerDownEvent>(Remove);
        m_Cancel.RegisterCallback<PointerDownEvent>(Cancel);
        m_Root.visible = false;
    }

    private void PlantSeedMenu(PointerDownEvent evt)
    {
        CurrentSpot.PlantSomething(PlantTypeHandler.Plant.GrassBlade);
        CloseMenu();
    }

    void Remove(PointerDownEvent evt)
    {
        CurrentSpot.RemovePlant();
        CloseMenu();
    }
    void Cancel(PointerDownEvent evt)
    {
        CloseMenu();
    }

    public void RegisterPlantingSpot(PlantingSpot Spot)
    {
        CurrentSpot = Spot;
    }

    public void OpenMenu()
    {
        Global.MenuOpen = true;
        m_Root.visible = true;
    }
    void CloseMenu()
    {
        Global.MenuOpen = false;
        m_Root.visible = false;
    }
}