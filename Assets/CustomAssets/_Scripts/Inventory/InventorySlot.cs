using UnityEngine;
using UnityEngine.UI;

namespace BWV
{
    public class InventorySlot : MonoBehaviour
    {
        public Image icon;
        public Text quantityText;

        private InventoryItemSO _item;
        private int _quantity;

        public void SetItem(InventoryItemSO item, int quantity)
        {
            _item = item;
            _quantity = quantity;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
            quantityText.text = quantity.ToString();
        }

        public void ClearSlot()
        {
            _item = null;
            _quantity = 0;
            icon.sprite = null;
            icon.enabled = false;
            quantityText.text = "";
        }

        public void UseItem()
        {
            if (_item != null)
            {
                // Use item functionality here
                _quantity--;
                quantityText.text = _quantity.ToString();

                if (_quantity <= 0)
                {
                    ClearSlot();
                }
            }
        }
    }
}
