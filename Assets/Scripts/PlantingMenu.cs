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

    private PlantingSpot CurrentSpot;
    private Inventory inventory;
    private Animator anim;

    void Start()
    {
        anim = FindObjectOfType<Animator>();
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

    void Remove(PointerDownEvent evt)
    {
        anim.Play("Base Layer.Harvest");
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
                        anim.Play("Base Layer.Plant");
                        CloseMenu();
                    });
                    itemLabel.text = item.name;
                    itemLabel.style.color = Color.white;
                    itemLabel.style.fontSize = 32;
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