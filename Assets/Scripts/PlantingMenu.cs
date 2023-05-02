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
    private VisualElement m_SeedPicker;
    private VisualElement m_PlantSeed;
    private VisualElement m_Remove_Plant;
    private VisualElement m_Cancel;
    private VisualElement m_SeedList;

    private PlantingSpot CurrentSpot;
    private Inventory inventory;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        m_Root = GetComponent<UIDocument>().rootVisualElement;
        m_SeedPicker = GameObject.Find("SeedPicker").GetComponent<UIDocument>().rootVisualElement;
        m_PlantSeed = m_Root.Q<VisualElement>("Plant");
        m_Remove_Plant = m_Root.Q<VisualElement>("Remove");
        m_Cancel = m_Root.Q<VisualElement>("Cancel");
        m_PlantSeed.RegisterCallback<PointerDownEvent>(OpenSeedPicker);
        m_Remove_Plant.RegisterCallback<PointerDownEvent>(Remove);
        m_Cancel.RegisterCallback<PointerDownEvent>(Cancel);
        m_SeedList = m_SeedPicker.Q<VisualElement>("Seeds");
        m_Root.visible = false;
        m_SeedPicker.visible = false;
    }

    private void PlantSeedMenu(PointerDownEvent evt)
    {
        if (GameManager.GetItem("carrotseed", out Item carrotSeedItem)) {
            CurrentSpot.PlantSomething(carrotSeedItem);
        }

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

    void OpenSeedPicker(PointerDownEvent evt)
    {
        m_Root.visible = false;
        m_SeedPicker.visible = true;
        FillSeedList();
    }

    void FillSeedList()
    {
        m_SeedList.Clear();
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            GameManager.GetItem(inventory.itemIds[i], out Item item);
            if (item.plantable)
            {
                Label listElement = new Label();
                listElement.text = item.name;
                m_SeedList.Add(listElement);
            }
        }
    }
}