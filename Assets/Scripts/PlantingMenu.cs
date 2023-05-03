using KinematicCharacterController.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.WSA;
using Button = UnityEngine.UIElements.Button;

public class PlantingMenu : MonoBehaviour
{
    private VisualElement m_Root;
    private VisualElement m_SeedPicker;
    private VisualElement m_PlantSeed;
    private VisualElement m_Remove_Plant;
    private VisualElement m_Cancel;
    private VisualElement m_SeedList;
    private TextField m_SeedTextBox;

    private PlantingSpot CurrentSpot;
    private Inventory inventory;

    private List<string> seedNameList;
    private List<string> seedIdList;

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
        m_SeedTextBox = m_SeedPicker.Q<TextField>("SeedBox");
        m_Root.visible = false;
        m_SeedPicker.visible = false;
        m_SeedTextBox.visible = false;
        m_SeedTextBox.RegisterCallback<ChangeEvent<string>>(PlantSeed);
    }

    private void PlantSeed(ChangeEvent<string> evt)
    {
        if (m_SeedTextBox.value.ToLower() == "cancel")
        {
            CloseMenu();
            return;
        }
        if (seedNameList.IndexOf(m_SeedTextBox.value.ToLower()) == -1) return;
        string itemId = seedIdList[seedNameList.IndexOf(m_SeedTextBox.value)];
        if (GameManager.GetItem(itemId, out Item seed)) {
            CurrentSpot.PlantSomething(seed);
            for (int i = 0; i < inventory.slots.Count; i++)
            {
                if (inventory.slots[i].itemId == itemId.ToHashId())
                {
                    inventory.slots[i].RemoveAmount(1);
                    break;
                }
            }
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
        m_SeedPicker.visible = false;
        m_SeedTextBox.visible = false;
    }

    void OpenSeedPicker(PointerDownEvent evt)
    {
        m_Root.visible = false;
        m_SeedPicker.visible = true;
        m_SeedTextBox.visible = true;
        FillSeedList();
    }

    void FillSeedList()
    {
        m_SeedList.Clear();
        m_SeedTextBox.value = "";
        seedNameList = new List<string>();
        seedIdList = new List<string>();
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            GameManager.GetItem(inventory.slots[i].itemId, out Item item);
            if (item != null)
            {
                if (item.plantable)
                {
                    Button itemLabel = new Button(() => {
                        CurrentSpot.PlantSomething(item);
                        for (int i = 0; i < inventory.slots.Count; i++)
                        {
                            if (inventory.slots[i].itemId == item.hashId)
                            {
                                inventory.slots[i].RemoveAmount(1);
                                break;
                            }
                        }
                        CloseMenu();
                    });
                    itemLabel.text = item.name;
                    itemLabel.style.color = Color.white;
                    itemLabel.style.fontSize = 32;
                    seedNameList.Add(item.name.ToLower());
                    seedIdList.Add(item.id);
                    m_SeedList.Add(itemLabel);
                }
            }
        }
        Button cancelLabel = new Button(CloseMenu);
        cancelLabel.text = "Cancel";
        cancelLabel.style.color = Color.white;
        cancelLabel.style.fontSize = 32;
        m_SeedList.Add(cancelLabel);
        
    }
}