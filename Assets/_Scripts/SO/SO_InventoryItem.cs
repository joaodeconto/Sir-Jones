using UnityEngine;

namespace BWV
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
    public class SO_InventoryItem : ScriptableObject
    {
        public string itemName;
        public Sprite itemIcon;
        public string description;
        public int maxStackSize = 1;
    }
}
