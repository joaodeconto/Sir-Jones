using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace BWV
{
    public class InventoryUI : MonoBehaviour
    {
        public GameObject inventoryPanel;
        public GameObject slotPanel;
        public GameObject inventorySlot;
        public GameObject inventoryItem;

        private int slotAmount;
        private List<GameObject> slots = new List<GameObject>();

        public void SetupInventory(Dictionary<InventoryItemSO, int> inventory)
        {
            // Show inventory panel
            inventoryPanel.SetActive(true);

            // Get the slot panel transform
            Transform slotPanelTransform = slotPanel.transform;

            // Get the number of slots required based on the inventory count
            slotAmount = inventory.Count;

            // Create the inventory slots
            for (int i = 0; i < slotAmount; i++)
            {
                // Instantiate a new inventory slot
                GameObject slot = Instantiate(inventorySlot);

                // Set the parent of the slot to the slot panel
                slot.transform.SetParent(slotPanelTransform);

                // Add the slot to the list of slots
                slots.Add(slot);
            }

            // Fill the inventory slots with the items in the inventory
            UpdateInventory(inventory);
        }

        public void UpdateInventory(Dictionary<InventoryItemSO, int> inventory)
        {
            // Loop through each inventory slot and set its item and quantity
            for (int i = 0; i < slots.Count; i++)
            {
                GameObject slot = slots[i];

                // Get the item and quantity for this slot
                InventoryItemSO item = inventory.Keys.ElementAt(i);
                int quantity = inventory.Values.ElementAt(i);

                // Set the item and quantity for this slot
                slot.GetComponent<InventorySlot>().SetItem(item, quantity);
            }
        }
    }
}
