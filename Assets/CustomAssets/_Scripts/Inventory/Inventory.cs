using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace BWV
{
    public class Inventory : MonoBehaviour
    {
        public Dictionary<Item, int> items = new Dictionary<Item, int>();

        public void AddItem(Item item, int quantity)
        {
            if (items.ContainsKey(item))
            {
                items[item] += quantity;
            }
            else
            {
                items.Add(item, quantity);
            }
        }

        public void RemoveItem(Item item, int quantity)
        {
            if (items.ContainsKey(item))
            {
                if (items[item] >= quantity)
                {
                    items[item] -= quantity;
                }
                else
                {
                    Debug.LogWarning($"Tried to remove {quantity} {item.name} but only have {items[item]}.");
                }
            }
            else
            {
                Debug.LogWarning($"Tried to remove {quantity} {item.name} but don't have any.");
            }
        }
    }
}