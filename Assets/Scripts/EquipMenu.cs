using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EquipMenu : MonoBehaviour
{
    VisualElement m_root;
    VisualElement m_WeaponList;
    Inventory inventory;
    WeaponManager weaponManager;

    void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
        inventory = FindObjectOfType<Inventory>();
        m_root = GetComponent<UIDocument>().rootVisualElement;
        m_WeaponList = m_root.Q<VisualElement>("Weapons");
        m_root.visible = false;
    }

    public void OpenMenu()
    {
        m_root.visible = true;
        FillWeaponList();
    }
    void CloseMenu()
    {
        m_root.visible = false;
        Global.MenuOpen = false;
    }

    void FillWeaponList()
    {
        m_WeaponList.Clear();
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            GameManager.GetItem(inventory.slots[i].itemId, out Item item);
            if (item != null)
            {
                if (item.equippable)
                {
                    Button itemLabel = new Button(() => {
                        weaponManager.Equip(item);
                        CloseMenu();
                    });
                    itemLabel.text = item.name;
                    itemLabel.style.color = Color.white;
                    itemLabel.style.fontSize = 32;
                    m_WeaponList.Add(itemLabel);
                }
            }
        }
        Button cancelLabel = new Button(CloseMenu);
        cancelLabel.text = "Cancel";
        cancelLabel.style.color = Color.white;
        cancelLabel.style.fontSize = 32;
        m_WeaponList.Add(cancelLabel);
    }
}