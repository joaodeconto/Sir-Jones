using System.Collections.Generic;
using UnityEngine;

namespace BWV
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
    public class ShopStorageSO : ScriptableObject
    {
        public List<InventoryItemSO> availableItems= new List<InventoryItemSO>();
    }
}