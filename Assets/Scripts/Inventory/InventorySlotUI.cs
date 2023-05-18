using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class InventorySlotUI : VisualElement
{
    public int slotIndex;
    public Image Icon;
    public Label Amount;
    public InventorySlotUI()
    {
        //Create a new Image element and add it to the root
        Icon = new Image();
        Add(Icon);
        Icon.pickingMode = PickingMode.Ignore;
        Amount = new Label("0");
        Amount.pickingMode = PickingMode.Ignore;
        Amount.AddToClassList("SlotAmount");
        Add(Amount);
        //Add USS style properties to the elements
        Icon.AddToClassList("slotIcon");
        AddToClassList("slotContainer2");
        RegisterCallback<PointerDownEvent>(OnPointerDown);
    }

    public void Refresh() {
        Refresh(InventoryUIController.instance.actualInventory.slots[slotIndex]);
    }
    public void Refresh(InventorySlot inventorySlot)
    {
        if (inventorySlot.itemAmount > 0 && GameManager.GetItem(inventorySlot.itemId, out Item itemReference)) {
            Icon.image = itemReference.icon;
            Amount.text = inventorySlot.itemAmount.ToString();
        }
        else {
            Icon.image = null;
            Amount.text = "";
        }
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        //Not the left mouse button
        if (evt.button != 0 || InventoryUIController.instance.actualInventory.slots[slotIndex].itemAmount <= 0) {
            return;
        }
        //Clear the image
        Icon.image = null;
        Amount.text = string.Empty;
        //Start the drag
        InventoryUIController.StartDrag(evt.position, this);
    }

    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<InventorySlotUI, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}