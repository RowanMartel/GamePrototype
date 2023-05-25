using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UseMenu : MonoBehaviour
{
    VisualElement m_root;
    VisualElement m_ConsumableList;
    Inventory inventory;
    PlayerHandler playerHandler;

    void Start()
    {
        playerHandler = FindObjectOfType<PlayerHandler>();
        inventory = FindObjectOfType<Inventory>();
        m_root = GetComponent<UIDocument>().rootVisualElement;
        m_ConsumableList = m_root.Q<VisualElement>("Consumables");
        m_root.visible = false;
    }

    public void OpenMenu()
    {
        m_root.visible = true;
        FillConsumableList();
    }
    void CloseMenu()
    {
        m_root.visible = false;
        Global.MenuOpen = false;
    }

    void FillConsumableList()
    {
        m_ConsumableList.Clear();
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            GameManager.GetItem(inventory.slots[i].itemId, out Item item);
            if (item != null)
            {
                if (item.useable)
                {
                    Button itemLabel = new Button(() => {
                        playerHandler.Heal(item.strength);
                        inventory.RemoveItem(item.id, 1);
                        CloseMenu();
                    });
                    itemLabel.text = item.name;
                    itemLabel.style.color = Color.white;
                    itemLabel.style.fontSize = 32;
                    m_ConsumableList.Add(itemLabel);
                }
            }
        }
        Button cancelLabel = new Button(CloseMenu);
        cancelLabel.text = "Cancel";
        cancelLabel.style.color = Color.white;
        cancelLabel.style.fontSize = 32;
        m_ConsumableList.Add(cancelLabel);
    }
}