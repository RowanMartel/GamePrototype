using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUIController : MonoBehaviour
{
    public static InventoryUIController instance;
    public List<InventorySlotUI> InventoryUISlots = new List<InventorySlotUI>();
    private VisualElement m_Root;
    private VisualElement m_SlotContainer;
    public Inventory actualInventory;

    private static VisualElement m_GhostIcon;
    private static bool m_IsDragging;
    private static InventorySlotUI m_OriginalSlot;

    private void Awake()
    {
        instance = this;
        m_Root = GetComponent<UIDocument>().rootVisualElement;
        m_SlotContainer = m_Root.Q<VisualElement>("SlotContainer");
        for (int i = 0; i < 20; i++)
        {
            InventorySlotUI item = new InventorySlotUI();
            item.slotIndex = i;
            InventoryUISlots.Add(item);
            m_SlotContainer.Add(item);
        }
        GameController.OnInventoryChanged += GameController_OnInventoryChanged;
        m_GhostIcon = m_Root.Query<VisualElement>("GhostIcon");
        m_SlotContainer.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        m_GhostIcon.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        m_GhostIcon.RegisterCallback<PointerUpEvent>(OnPointerUp);
        m_SlotContainer.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private void GameController_OnInventoryChanged(string[] itemGuid, InventoryChangeType change) {
        RefreshUI();
    }

    public static void StartDrag(Vector2 position, InventorySlotUI originalSlot)
    {
        InventorySlot slotToDrag = instance.actualInventory.slots[originalSlot.slotIndex];
        if (slotToDrag.itemAmount <= 0) return;
        //Set tracking variables
        m_IsDragging = true;
        m_OriginalSlot = originalSlot;
        //Set the new position
        m_GhostIcon.style.top = position.y - m_GhostIcon.layout.height / 2;
        m_GhostIcon.style.left = position.x - m_GhostIcon.layout.width / 2;
        //Set the image
        if (GameManager.GetItem(slotToDrag.itemId, out Item myItem)) {
            m_GhostIcon.style.backgroundImage = new StyleBackground(myItem.icon);
        }
        else
        {
            m_GhostIcon.style.backgroundImage = new StyleBackground();
        }
        //Flip the visibility on
        m_GhostIcon.style.visibility = Visibility.Visible;
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        //Only take action if the player is dragging an item around the screen
        if (!m_IsDragging)
        {
            return;
        }
        //Set the new position
        m_GhostIcon.style.top = evt.position.y - m_GhostIcon.layout.height / 2;
        m_GhostIcon.style.left = evt.position.x - m_GhostIcon.layout.width / 2;
    }
    private void OnPointerUp(PointerUpEvent evt)
    {
        if (!m_IsDragging)
        {
            return;
        }
        //Check to see if they are dropping the ghost icon over any inventory slots.
        IEnumerable<InventorySlotUI> slots = InventoryUISlots.Where(x => x.worldBound.Overlaps(m_GhostIcon.worldBound));
        //Found at least one
        if (slots.Count() != 0) {
            InventorySlotUI closestSlot = slots.OrderBy(x => Vector2.Distance
               (x.worldBound.position, m_GhostIcon.worldBound.position)).First();

            
            InventorySlot slotToDrag = instance.actualInventory.slots[m_OriginalSlot.slotIndex];
            InventorySlot slotToDropOn = instance.actualInventory.slots[closestSlot.slotIndex];
            slotToDropOn.SwapSlots(slotToDrag);
            RefreshUI();
        }
        //Didn't find any (dragged off the window)
        else {
            m_OriginalSlot.Refresh();
        }
        //Clear dragging related visuals and data
        m_IsDragging = false;
        m_OriginalSlot = null;
        m_GhostIcon.style.visibility = Visibility.Hidden;
    }

    public void RefreshUI() {
        for (int i = 0; i < InventoryUISlots.Count; i++) {
            InventoryUISlots[i].Refresh();
        }
    }
}