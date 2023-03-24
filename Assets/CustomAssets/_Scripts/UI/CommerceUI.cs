using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace BWV
{
    public class CommerceUI : MonoBehaviour
    {
        public ShopStorageSO shopStorage;

        public GameObject itemTemplate;
        public Transform itemContainer;

        private void Start()
        {
            // Update the GUI with the item and price data
            UpdateCommerceGUI();
        }

        private void UpdateCommerceGUI()
        {
            // Remove any existing items from the container
            foreach (Transform child in itemContainer)
            {
                Destroy(child.gameObject);
            }

            // Add new items to the container based on the item list in the shop storage
            foreach (InventoryItemSO item in shopStorage.availableItems)
            {
                GameObject newItem = Instantiate(itemTemplate, itemContainer);
                newItem.SetActive(true);

                // Update the item name, description, icon, and price
                newItem.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = item.itemName;
                newItem.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = item.description;
                newItem.transform.Find("Icon").GetComponent<Image>().sprite = item.itemIcon;
                newItem.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = item.itemPrice.ToString();
            }
        }
    }
}
