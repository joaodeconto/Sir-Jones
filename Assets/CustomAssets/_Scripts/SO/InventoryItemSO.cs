using UnityEngine;

namespace BWV
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
    public class InventoryItemSO : ScriptableObject
    {
        public string itemName;
        public Sprite itemIcon;
        public string description;
        public float itemPrice;
        public int maxStackSize = 1;
    }
}
