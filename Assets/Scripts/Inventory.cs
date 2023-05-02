using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();
    private void Start() {
        slots.Clear();
        for (int i = 0; i < 20; i++)
        {
            slots.Add(new InventorySlot());
        }
    }

    public bool AddItem(string id, int amount = 1) => AddItem(id.ToHashId(),amount);
    public bool AddItem(int id, int amount = 1)
    {
        InventorySlot desiredSlot = default;
        bool hasDesiredSlot = false;
        //See if a slot already has it.
        for (int i = 0; i < slots.Count; i++) {
            if (slots[i].itemId == id) {
                //found a slot the type we want! yay!
                desiredSlot = slots[i];
                hasDesiredSlot = true;
                break;
            }
        }

        //None of our slots are the type we want.
        if (!hasDesiredSlot)
        {
            for (int i = 0; i < slots.Count; i++) {
                if (slots[i].itemAmount == 0) {
                    //found a slot the type we want! yay!
                    desiredSlot = slots[i];
                    hasDesiredSlot = true;
                    break;
                }
            }
        }
        
        //None of our slots are the type we want.
        if (!hasDesiredSlot)
        {
            //All the slots are full, nothing we can do.
            return false;
        }

        desiredSlot.itemId = id;
        desiredSlot.itemAmount += amount;
        InventoryUIController.instance.RefreshUI();
        return true;
    }
}

[Serializable]
public class InventorySlot
{
    public int itemId = 0;
    public int itemAmount = 0;

    public void Clear() {
        itemId = 0;
        itemAmount = 0;
    }

    public void RemoveAmount(int amount) {
        itemAmount -= amount;
        if(itemAmount <=0) Clear();
    }

    public void SwapSlots(InventorySlot otherSlot) {
        var tempId = itemId;
        var tempAmount = itemAmount;
        itemId = otherSlot.itemId;
        itemAmount = otherSlot.itemAmount;
        otherSlot.itemId = tempId;
        otherSlot.itemAmount = tempAmount;
    }
}

public static class ExtensionMethods
{
    public static int ToHashId(this string str) {
        return Animator.StringToHash(str.Trim().ToUpper());
    }
}